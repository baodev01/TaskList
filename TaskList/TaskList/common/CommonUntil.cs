using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TaskList.common
{
    class CommonUntil
    {
        public static DateTime mustFinish(DateTime dateStart, DateTime dateEnd, object plan_person, object person)
        {
            TimeSpan diff = dateEnd.AddDays(1) - dateStart;
            int day = diff.Days / Int32.Parse(plan_person.ToString());
            double mustDay = (double)day / (double)Int32.Parse(person.ToString());
            double mustDayFinal = Math.Ceiling(mustDay) - 1;

            DateTime result = dateStart.AddDays(mustDayFinal);
            if(DateTime.Compare(result, dateStart) < 0)
            {
                return dateStart;
            }
            return result;
        }

        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        internal static object convertStartDateOfYear(object plan_date_start, int toYear)
        {
            DateTime dateStart = (DateTime)plan_date_start;
            DateTime result;

            try
            {
                result = new DateTime(toYear, dateStart.Month, dateStart.Day);
            } catch
            {
                result = new DateTime(toYear, dateStart.Month, dateStart.Day - 1);
            }

            return result;
        }

        internal static object convertEndDateOfYear(object plan_date_start, object plan_date_end, int toYear)
        {
            DateTime dateStart = (DateTime)plan_date_start;
            DateTime dateEnd = (DateTime)plan_date_end;

            TimeSpan diff = dateEnd - dateStart;
            int day = diff.Days;

            DateTime dateStartNew = (DateTime)convertStartDateOfYear(plan_date_start, toYear);
            DateTime dateEndNew = dateStartNew.AddDays(day);
            
            return dateEndNew;
        }
    }
}
