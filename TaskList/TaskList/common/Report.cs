using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskList.common
{
    class Report
    {

        internal static List<tblReport> reportTaskByYear(string monthYear)
        {
            List<tblReport> result = new List<tblReport>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = Program.con;
            cmd.CommandText = "select count(*) as count "
                            + " from tbl_tasks "
                            + " where del_f = 0 "
                            + " and DATE_FORMAT(plan_date_start, '%m-%Y') <= @monthYear "
                            + " and DATE_FORMAT(plan_date_end, '%m-%Y') >= @monthYear ";
            cmd.Parameters.AddWithValue("@monthYear", monthYear);
            cmd.Prepare();
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    tblReport report = new tblReport();
                    report.countTask = reader["count"];
                    report.typeTask = "ALL";
                    report.dateTime = monthYear;
                    result.Add(report);
                }
            }
            return result;
        }

        internal static IEnumerable<tblReport> reportTaskByMonth(string dayMonthYear)
        {
            List<tblReport> result = new List<tblReport>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = Program.con;
            cmd.CommandText = "select count(*) as count "
                            + " from tbl_tasks "
                            + " where del_f = 0 "
                            + " and DATE_FORMAT(plan_date_start, '%d-%m-%Y') <= @monthYear "
                            + " and DATE_FORMAT(plan_date_end, '%d-%m-%Y') >= @monthYear ";
            cmd.Parameters.AddWithValue("@monthYear", dayMonthYear);
            cmd.Prepare();
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    tblReport report = new tblReport();
                    report.countTask = reader["count"];
                    report.typeTask = "ALL";
                    report.dateTime = dayMonthYear;
                    result.Add(report);
                }
            }
            return result;
        }
    }
}
