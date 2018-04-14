using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskList
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Program.openConnection();

            Application.Run(new login());

            Program.closeConnection();
            
        }

        static public MySqlConnection con = null;
        static public void openConnection()
        {
            string host = "localhost";
            int port = 3306;
            string database = "tasklistdb";
            string username = "root";
            string password = "123456a-";
            con = DBMySQLUtils.GetDBConnection(host, port, database, username, password);
            Console.WriteLine("Openning Connection ...");

            try
            {
                con.Open();
                Console.WriteLine("Connection successful!");
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        static public void closeConnection()
        {
            con.Close();
            Console.WriteLine("Connection Close successful!");
        }

    }
}
