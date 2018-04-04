using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskList.common
{
    class Areas
    {
        public static List<tblAreas> selectAll()
        {
            List<tblAreas> result = new List<tblAreas>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = Program.con;
            cmd.CommandText = "select id, areas from tbl_areas";
            cmd.Prepare();
            // int result = command.ExecuteNonQuery();
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    tblAreas tblRecord = new tblAreas();
                    tblRecord.id = reader["id"];
                    tblRecord.areas = reader["areas"];

                    result.Add(tblRecord);
                    //Console.WriteLine(String.Format("{0}-{1}", reader["id"], reader["task_type_name"]));
                }
            }

            return result;
        }
    }
}
