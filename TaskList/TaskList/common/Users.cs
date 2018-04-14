using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskList.common
{
    class Users
    {
        internal static bool login(string username, string password)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = Program.con;
            String sql = " SELECT count(id) "
                        + " from tbl_users "
                        + " where username = @username "
                        + " and password = @password";
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);
            Int32 count = Convert.ToInt32(cmd.ExecuteScalar());

            return count == 1;
        }

        internal static void changePass(string username, string password)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = Program.con;
            String sql = " UPDATE tbl_users SET "
                        + " password = @password "
                        + " where username = @username ";
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.ExecuteNonQuery();
        }
    }
}
