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
            List<tblTasks> result = new List<tblTasks>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = Program.con;
            cmd.CommandText = "select tbl_tasks.id,task_name,task_type_name"
                            + ",DATE_FORMAT(plan_date_start,'%d-%m-%Y') as plan_date_start"
                            + ",DATE_FORMAT(plan_date_end,'%d-%m-%Y') as plan_date_end"
                            + ",plan_person,note"
                            + ",if(copy_f=1,'yes','no') as copy_f"
                            + " from tbl_tasks,tbl_task_type"
                            + " where tbl_tasks.task_type = tbl_task_type.id"
                            + " and del_f = 0"
                            //+ " and plan_date_start >=" + fromDate
                            //+ " and plan_date_start <=" + toDate;
                            + " and plan_date_start between date("+ fromDate + ")"
                            + " and DATE_ADD(date(" + toDate + "), INTERVAL 1 DAY)";
            cmd.Prepare();
            // int result = command.ExecuteNonQuery();
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

            cmd.Parameters.AddWithValue("@Name", "Trygve Gulbranssen");
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
    }
}
