using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskList.common
{
    class CommonUntil
    {
        public static double mustFinish(DateTime dateStart, DateTime dateEnd, object plan_person, object person)
        {
            TimeSpan diff = dateEnd.AddDays(1) - dateStart;
            int day = diff.Days / Int32.Parse(plan_person.ToString());
            double mustDay = (double)day / (double)Int32.Parse(person.ToString());
            double mustDayFinal = Math.Ceiling(mustDay) - 1;

            return mustDayFinal;
        }
    }
}
