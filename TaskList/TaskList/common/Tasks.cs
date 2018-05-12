using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskList.common
{
    class Tasks
    {
        public static List<tblTasks> selectAllSample()
        {
            List<tblTasks> result = new List<tblTasks>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = Program.con;
            cmd.CommandText = "select * from tbl_tasks";
            cmd.Prepare();
            // int result = command.ExecuteNonQuery();
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    tblTasks tblRecord = new tblTasks();
                    tblRecord.id = reader["id"];
                    tblRecord.task_name = reader["task_name"];
                    tblRecord.task_type = reader["task_type"];
                    tblRecord.plan_date_start = reader["plan_date_start"];
                    tblRecord.plan_date_end = reader["plan_date_end"];
                    tblRecord.plan_person = reader["plan_person"];
                    tblRecord.re_plan_date_start = reader["re_plan_date_start"];
                    tblRecord.re_plan_date_end = reader["re_plan_date_end"];
                    tblRecord.person = reader["person"];
                    tblRecord.must_date_finish = reader["must_date_finish"];
                    tblRecord.status = reader["status"];
                    tblRecord.note = reader["note"];
                    tblRecord.date_finish = reader["date_finish"];
                    tblRecord.copy_f = reader["copy_f"];
                    tblRecord.del_f = reader["del_f"];

                    result.Add(tblRecord);
                    //Console.WriteLine(String.Format("{0}-{1}", reader["id"], reader["task_type_name"]));
                }
            }

            return result;
        }

        internal static void copyPlan(int fromYear, int toYear)
        {
            List<tblTasks> tasks = Tasks.selectTaskListByYear(fromYear);

            foreach (tblTasks task in tasks)
            {
                task.plan_date_end = CommonUntil.convertEndDateOfYear(task.plan_date_start, task.plan_date_end, toYear);
                task.plan_date_start = CommonUntil.convertStartDateOfYear(task.plan_date_start, toYear);
                task.re_plan_date_start = task.plan_date_start;
                task.re_plan_date_end = task.plan_date_end;
                //task.person = task.plan_person;
                task.must_date_finish = CommonUntil.mustFinish((DateTime)task.re_plan_date_start, (DateTime)task.re_plan_date_end, task.plan_person, task.person);
                task.re_plan_date_start = task.plan_date_start;
                task.re_plan_date_end = task.plan_date_end;
                task.status = 0;
                task.note = null;
                task.date_finish = null;
                task.copy_f = 1;
                task.del_f = 0;

                insert(task);
            }
        }

        //internal static void addPlan(List<tblTasks> tasks)
        //{
        //    foreach (tblTasks task in tasks)
        //    {
        //        insert(task);
        //    }
        //}

        internal static void deletePlan(int toYear)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = Program.con;
            cmd.CommandText = "DELETE FROM tbl_tasks WHERE DATE_FORMAT(plan_date_start, '%Y') = @year";
            cmd.Prepare();
            cmd.Parameters.AddWithValue("@year", toYear);
            cmd.ExecuteNonQuery();
        }

        internal static List<tblTasks> selectTaskListByYear(int fromYear)
        {
            List<tblTasks> result = new List<tblTasks>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = Program.con;
            cmd.CommandText = "select * "
                            + " from tbl_tasks "
                            + " where del_f = 0 and copy_f = 1 "
                            + " and DATE_FORMAT(plan_date_start, '%Y') = @year";
            cmd.Parameters.AddWithValue("@year", fromYear);
            cmd.Prepare();
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    tblTasks tblRecord = new tblTasks();
                    tblRecord.id = reader["id"];
                    tblRecord.task_name = reader["task_name"];
                    tblRecord.task_name_en = reader["task_name_en"];
                    tblRecord.task_type = reader["task_type"];
                    tblRecord.areas = reader["areas"];
                    tblRecord.location = reader["location"];
                    tblRecord.plan_date_start = reader["plan_date_start"];
                    tblRecord.plan_date_end = reader["plan_date_end"];
                    tblRecord.plan_person = reader["plan_person"];
                    tblRecord.person = reader["person"];
                    //tblRecord.note = reader["note"];
                    //tblRecord.copy_f = reader["copy_f"];

                    result.Add(tblRecord);
                }
            }
            return result;
        }

        internal static Int32 countTaskOfYear(int toYear)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = Program.con;
            String sql =  " SELECT count(id) "
                        + " from tbl_tasks "
                        + " where del_f = 0 "
                        + " and DATE_FORMAT(plan_date_start, '%Y') = @year";
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@year", toYear);
            Int32 count = Convert.ToInt32(cmd.ExecuteScalar());
            return count;
        }

        internal static void deleteTaskPlanById(string id)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = Program.con;
            cmd.CommandText = "DELETE FROM tbl_tasks WHERE id = @id";
            cmd.Prepare();
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        internal static List<tblTasks> selectTaskList(DateTime toDate, bool finish_f)
        {
            string to = toDate.ToString("yyyy-MM-dd");
            string fromFinish = toDate.ToString("yyyy") + "-01-01";
            List<tblTasks> result = new List<tblTasks>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = Program.con;

            String sql = " SELECT  "
                    + "     id, "
                    + "     task_name, "
                    + "     task_name_en, "
                    + "     task_type_name, "
                    + "     areas, "
                    + "     location, "
                    + "     plan_person, "
                    + "     re_plan_date_start, "
                    + "     re_plan_date_end, "
                    + "     person, "
                    + "     must_date_finish, "
                    + "     status, "
                    + "     delay, "
                    + "     note "
                    + " FROM "
                    + " ( "
                    + " SELECT  "
                    + "     tbl_tasks.id AS id, "
                    + "     task_name, "
                    + "     task_name_en, "
                    + "     task_type_name, "
                    + "     tbl_areas.areas as areas, "
                    + "     location, "
                    + "     plan_person, "
                    + "     DATE_FORMAT(re_plan_date_start, '%d-%m-%Y') AS re_plan_date_start, "
                    + "     DATE_FORMAT(re_plan_date_end, '%d-%m-%Y') AS re_plan_date_end, "
                    + "     person, "
                    + "     DATE_FORMAT(must_date_finish, '%d-%m-%Y') AS must_date_finish, "
                    + "     (CASE "
                    + "         WHEN status = 1 THEN 'doing' "
                    + "         WHEN status = 9 THEN 'done' "
                    + "         ELSE '' "
                    + "     END) AS status, "
                    + "     if(DATE_FORMAT(must_date_finish, '%Y-%m-%d') < DATE_FORMAT(sysdate(), '%Y-%m-%d') AND status <> 9 ,'delay','') as delay, "
                    + "     note "
                    + " FROM "
                    + "     tbl_tasks "
                    + " LEFT JOIN tbl_task_type ON  tbl_tasks.task_type = tbl_task_type.id"
                    + " LEFT JOIN tbl_areas ON  tbl_tasks.areas = tbl_areas.id "
                    + " WHERE ( del_f = 0 "
                    + "         AND DATE_FORMAT(re_plan_date_start, '%Y-%m-%d') <= '" + to + "' AND status <> 9 ";
                   // + " 			OR ( DATE_FORMAT(must_date_finish, '%Y-%m-%d') < DATE_FORMAT(sysdate(), '%Y-%m-%d') AND status <> 9 ) ) ";
            if(finish_f)
            {
                sql = sql + " ) OR ( del_f = 0 AND status = 9 AND DATE_FORMAT(re_plan_date_start, '%Y-%m-%d') >= '" + fromFinish + "' ";
            }

            sql = sql   + ") ) AS TMP "
                        + " ORDER BY delay DESC, status ASC, re_plan_date_start ASC, id ASC";
            cmd.CommandText = sql;
            cmd.Prepare();
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    tblTasks tblRecord = new tblTasks();
                    tblRecord.id = reader["id"];
                    tblRecord.task_name = reader["task_name"];
                    tblRecord.task_name_en = reader["task_name_en"];
                    tblRecord.task_type_name = reader["task_type_name"];
                    tblRecord.areas = reader["areas"];
                    tblRecord.location = reader["location"];
                    tblRecord.plan_person = reader["plan_person"];
                    tblRecord.re_plan_date_start = reader["re_plan_date_start"];
                    tblRecord.re_plan_date_end = reader["re_plan_date_end"];
                    tblRecord.person = reader["person"];
                    tblRecord.must_date_finish = reader["must_date_finish"];
                    tblRecord.status = reader["status"];
                    tblRecord.delay = reader["delay"];
                    tblRecord.note = reader["note"];

                    result.Add(tblRecord);
                }
            }

            return result;
        }

        internal static object convertAreasWithName(object areas_name)
        {
            Int32 id = 0;
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = Program.con;
            String sql = " SELECT id "
                        + " from tbl_areas "
                        + " where areas = @areas";
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@areas", areas_name);
            cmd.Prepare();
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    id = Convert.ToInt32(reader["id"]);
                }
            }
            return id;
        }

        internal static Int32 convertTaskTypeWithName(object task_type_name)
        {
            Int32 id = 0;
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = Program.con;
            String sql = " SELECT id "
                        + " from tbl_task_type "
                        + " where task_type_name = @task_type_name";
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@task_type_name", task_type_name);
            cmd.Prepare();
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    id =  Convert.ToInt32(reader["id"]);
                }
            }
            return id;
        }

        public static List<tblTasks> selectTaskListPlan(DateTime fromDate, DateTime toDate)
        {
            string from = fromDate.ToString("yyyy-MM-dd");
            string to = toDate.ToString("yyyy-MM-dd");
            List<tblTasks> result = new List<tblTasks>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = Program.con;
            cmd.CommandText = "select tbl_tasks.id as id,task_name,task_type_name"
                            + ",task_name_en, tbl_areas.areas as areas, location"
                            + ",DATE_FORMAT(plan_date_start,'%d-%m-%Y') as plan_date_start"
                            + ",DATE_FORMAT(plan_date_end,'%d-%m-%Y') as plan_date_end"
                            + ",plan_person,note"
                            + ",if(copy_f=1,'yes','no') as copy_f"
                            + " from tbl_tasks"
                            + " LEFT JOIN tbl_task_type ON  tbl_tasks.task_type = tbl_task_type.id"
                            + " LEFT JOIN tbl_areas ON  tbl_tasks.areas = tbl_areas.id"
                            + " where del_f = 0"
                            + " and (date_format(plan_date_start,'%Y-%m-%d') between '" + from + "' "
                            + " and '" + to + "')"
                            + " order by plan_date_start asc, id asc";
            cmd.Prepare();
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    tblTasks tblRecord = new tblTasks();
                    tblRecord.id = reader["id"];
                    tblRecord.task_name = reader["task_name"];
                    tblRecord.task_name_en = reader["task_name_en"];
                    tblRecord.task_type_name = reader["task_type_name"];
                    tblRecord.areas = reader["areas"];
                    tblRecord.location = reader["location"];
                    tblRecord.plan_date_start = reader["plan_date_start"];
                    tblRecord.plan_date_end = reader["plan_date_end"];
                    tblRecord.plan_person = reader["plan_person"];
                    tblRecord.note = reader["note"];
                    tblRecord.copy_f = reader["copy_f"];

                    result.Add(tblRecord);
                }
            }

            return result;
        }

        internal static void updateById(tblTasks task)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = Program.con;
            cmd.CommandText = "UPDATE tbl_tasks SET"
                            + " task_name = @task_name,"
                            + " task_name_en = @task_name_en,"
                            + " task_type = @task_type,"
                            + " areas = @areas,"
                            + " location = @location,"
                            + " plan_date_start = @plan_date_start,"
                            + " plan_date_end = @plan_date_end,"
                            + " plan_person = @plan_person,"
                            + " re_plan_date_start = @re_plan_date_start,"
                            + " re_plan_date_end = @re_plan_date_end,"
                            + " person = @person,"
                            + " must_date_finish = @must_date_finish,"
                            + " status = @status,"
                            + " note = @note,"
                            + " date_finish = @date_finish,"
                            + " copy_f = @copy_f,"
                            + " del_f = @del_f "
                            + " WHERE id = " + task.id;
            cmd.Prepare();
            cmd.Parameters.AddWithValue("@task_name", task.task_name);
            cmd.Parameters.AddWithValue("@task_name_en", task.task_name_en);
            cmd.Parameters.AddWithValue("@task_type", task.task_type);
            cmd.Parameters.AddWithValue("@areas", task.areas);
            cmd.Parameters.AddWithValue("@location", task.location);
            cmd.Parameters.AddWithValue("@plan_date_start", task.plan_date_start);
            cmd.Parameters.AddWithValue("@plan_date_end", task.plan_date_end);
            cmd.Parameters.AddWithValue("@plan_person", task.plan_person);
            cmd.Parameters.AddWithValue("@re_plan_date_start", task.re_plan_date_start);
            cmd.Parameters.AddWithValue("@re_plan_date_end", task.re_plan_date_end);
            cmd.Parameters.AddWithValue("@person", task.person);
            cmd.Parameters.AddWithValue("@must_date_finish", task.must_date_finish);
            cmd.Parameters.AddWithValue("@status", task.status);
            cmd.Parameters.AddWithValue("@note", task.note);
            cmd.Parameters.AddWithValue("@date_finish", task.date_finish);
            cmd.Parameters.AddWithValue("@copy_f", task.copy_f);
            cmd.Parameters.AddWithValue("@del_f", task.del_f);

            cmd.ExecuteNonQuery();
        }

        internal static void updateRePlanById(tblTasks task)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = Program.con;
            cmd.CommandText = "UPDATE tbl_tasks SET"
                            + " re_plan_date_start = @re_plan_date_start,"
                            + " re_plan_date_end = @re_plan_date_end,"
                            + " person = @person,"
                            + " must_date_finish = @must_date_finish,"
                            + " status = @status,"
                            + " note = @note"
                            + " WHERE id = " + task.id;
            cmd.Prepare();
            cmd.Parameters.AddWithValue("@re_plan_date_start", task.re_plan_date_start);
            cmd.Parameters.AddWithValue("@re_plan_date_end", task.re_plan_date_end);
            cmd.Parameters.AddWithValue("@person", task.person);
            cmd.Parameters.AddWithValue("@must_date_finish", task.must_date_finish);
            cmd.Parameters.AddWithValue("@status", task.status);
            cmd.Parameters.AddWithValue("@note", task.note);

            cmd.ExecuteNonQuery();
        }
        
        public static void insert(tblTasks task)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = Program.con;
            cmd.CommandText = "INSERT INTO tbl_tasks ("
                            + "task_name, "
                            + "task_name_en, "
                            + "task_type, "
                            + "areas, "
                            + "location, "
                            + "plan_date_start, "
                            + "plan_date_end, "
                            + "plan_person, "
                            + "re_plan_date_start, "
                            + "re_plan_date_end, "
                            + "person, "
                            + "must_date_finish, "
                            + "status, "
                            + "note, "
                            + "date_finish, "
                            + "copy_f, "
                            + "del_f "
                            + ") VALUES("
                            + "@task_name, "
                            + "@task_name_en, "
                            + "@task_type, "
                            + "@areas, "
                            + "@location, "
                            + "@plan_date_start, "
                            + "@plan_date_end, "
                            + "@plan_person, "
                            + "@re_plan_date_start, "
                            + "@re_plan_date_end, "
                            + "@person, "
                            + "@must_date_finish, "
                            + "@status, "
                            + "@note, "
                            + "@date_finish, "
                            + "@copy_f, "
                            + "@del_f "
                            + ")";
            cmd.Prepare();
            cmd.Parameters.AddWithValue("@task_name", task.task_name);
            cmd.Parameters.AddWithValue("@task_name_en", task.task_name_en);
            cmd.Parameters.AddWithValue("@task_type", task.task_type);
            cmd.Parameters.AddWithValue("@areas", task.areas);
            cmd.Parameters.AddWithValue("@location", task.location);
            cmd.Parameters.AddWithValue("@plan_date_start", task.plan_date_start);
            cmd.Parameters.AddWithValue("@plan_date_end", task.plan_date_end);
            cmd.Parameters.AddWithValue("@plan_person", task.plan_person);
            cmd.Parameters.AddWithValue("@re_plan_date_start", task.re_plan_date_start);
            cmd.Parameters.AddWithValue("@re_plan_date_end", task.re_plan_date_end);
            cmd.Parameters.AddWithValue("@person", task.person);
            cmd.Parameters.AddWithValue("@must_date_finish", task.must_date_finish);
            cmd.Parameters.AddWithValue("@status", task.status);
            cmd.Parameters.AddWithValue("@note", task.note);
            cmd.Parameters.AddWithValue("@date_finish", task.date_finish);
            cmd.Parameters.AddWithValue("@copy_f", task.copy_f);
            cmd.Parameters.AddWithValue("@del_f", task.del_f);

            cmd.ExecuteNonQuery();
        }

        public static tblTasks selectTaskById(string id)
        {
            tblTasks result = new tblTasks();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = Program.con;
            cmd.CommandText = "select id, task_name,task_name_en, task_type, areas, location "
                            + ",plan_date_start"
                            + ",plan_date_end"
                            + ",plan_person,note"
                            + ",if(copy_f=1,'true','false') as copy_f"
                            + " from tbl_tasks"
                            + " where del_f = 0"
                            + " and id = " + id;
            cmd.Prepare();
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    result.id = reader["id"];
                    result.task_name = reader["task_name"];
                    result.task_name_en = reader["task_name_en"];
                    result.task_type = reader["task_type"];
                    result.areas = reader["areas"];
                    result.location = reader["location"];
                    result.plan_date_start = reader["plan_date_start"];
                    result.plan_date_end = reader["plan_date_end"];
                    result.plan_person = reader["plan_person"];
                    result.note = reader["note"];
                    result.copy_f = reader["copy_f"];
                }
            }

            return result;
        }

        public static tblTasks selectTaskStatusById(string id)
        {
            tblTasks result = new tblTasks();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = Program.con;
            cmd.CommandText = "select id, task_name,task_name_en, task_type, areas, location "
                            + ",re_plan_date_start"
                            + ",re_plan_date_end"
                            + ",plan_person,person, must_date_finish, note"
                            + ",status as status"
                            + " from tbl_tasks"
                            + " where del_f = 0"
                            + " and id = " + id;
            cmd.Prepare();
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    result.id = reader["id"];
                    result.task_name = reader["task_name"];
                    result.task_name_en = reader["task_name_en"];
                    result.task_type = reader["task_type"];
                    result.areas = reader["areas"];
                    result.location = reader["location"];
                    result.re_plan_date_start = reader["re_plan_date_start"];
                    result.re_plan_date_end = reader["re_plan_date_end"];
                    result.plan_person = reader["plan_person"];
                    result.person = reader["person"];
                    result.must_date_finish = reader["must_date_finish"];
                    result.note = reader["note"];
                    result.status = reader["status"];
                }
            }

            return result;
        }

        internal static void updateStatus(List<tblTasks> tasks)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = Program.con;

            // Start a local transaction
            MySqlTransaction myTrans = Program.con.BeginTransaction();
            cmd.Transaction = myTrans;
            object idError = "";

            try
            {
                foreach (tblTasks task in tasks)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "UPDATE tbl_tasks SET status=@status, note=@note, date_finish=@date_finish WHERE del_f = 0 AND id = @id";
                    cmd.Parameters.AddWithValue("@status", task.status);
                    cmd.Parameters.AddWithValue("@note", task.note);
                    cmd.Parameters.AddWithValue("@date_finish", task.date_finish);
                    cmd.Parameters.AddWithValue("@id", task.id);
                    idError = task.id;
                    cmd.ExecuteNonQuery();
                }
                myTrans.Commit();
            }
            catch (Exception e)
            {
                MessageBox.Show("Task Id: " + idError.ToString() + " _ Please contact with Admin!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Console.WriteLine("Neither record was written to database.");
                try
                {
                    myTrans.Rollback();
                    throw e;
                }
                catch
                {
                    if (myTrans.Connection != null)
                    {
                        //Console.WriteLine("An exception of type " + ex.GetType() +
                        //" was encountered while attempting to roll back the transaction.");
                    }
                }
            }
            finally
            {
            }
        }

    }
}
