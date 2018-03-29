using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static List<tblTasks> selectTaskListPlan(DateTime fromDate, DateTime toDate)
        {
            string from = fromDate.ToString("yyyy-MM-dd");
            string to = toDate.ToString("yyyy-MM-dd");
            List<tblTasks> result = new List<tblTasks>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = Program.con;
            cmd.CommandText = "select tbl_tasks.id as id,task_name,task_type_name"
                            + ",DATE_FORMAT(plan_date_start,'%d-%m-%Y') as plan_date_start"
                            + ",DATE_FORMAT(plan_date_end,'%d-%m-%Y') as plan_date_end"
                            + ",plan_person,note"
                            + ",if(copy_f=1,'yes','no') as copy_f"
                            + " from tbl_tasks,tbl_task_type"
                            + " where tbl_tasks.task_type = tbl_task_type.id"
                            + " and del_f = 0"
                            + " and (date_format(plan_date_start,'%Y-%m-%d') between '" + from + "' "
                            + " and '" + to + "')"
                            + " order by plan_date_start asc";
            cmd.Prepare();
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    tblTasks tblRecord = new tblTasks();
                    tblRecord.id = reader["id"];
                    tblRecord.task_name = reader["task_name"];
                    tblRecord.task_type_name = reader["task_type_name"];
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
                            + " task_type = @task_type,"
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
            cmd.Parameters.AddWithValue("@task_type", task.task_type);
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

        public static void insert(tblTasks task)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = Program.con;
            cmd.CommandText = "INSERT INTO tbl_tasks ("
                            + "task_name, "
                            + "task_type, "
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
                            + "@task_type, "
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
            cmd.Parameters.AddWithValue("@task_type", task.task_type);
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
            cmd.CommandText = "select id, task_name,task_type"
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
                    result.task_type = reader["task_type"];
                    result.plan_date_start = reader["plan_date_start"];
                    result.plan_date_end = reader["plan_date_end"];
                    result.plan_person = reader["plan_person"];
                    result.note = reader["note"];
                    result.copy_f = reader["copy_f"];
                }
            }

            return result;
        }

    }
}
