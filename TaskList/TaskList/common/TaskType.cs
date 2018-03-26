using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskList.common
{
    class TaskType
    {
        public static List<tblTaskType> selectAll()
        {
            List<tblTaskType> result = new List<tblTaskType>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = Program.con;
            cmd.CommandText = "select id, task_type_name from tbl_task_type";
            cmd.Prepare();
            // int result = command.ExecuteNonQuery();
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    tblTaskType tblRecord = new tblTaskType();
                    tblRecord.id = reader["id"];
                    tblRecord.task_type_name = reader["task_type_name"];

                    result.Add(tblRecord);
                    //Console.WriteLine(String.Format("{0}-{1}", reader["id"], reader["task_type_name"]));
                }
            }

            return result;
        }
    }
}
