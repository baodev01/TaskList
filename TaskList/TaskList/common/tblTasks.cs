using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskList.common
{
    class tblTasks
    {
        public object id { set; get; }
        public object task_name { set; get; }
        public object task_type_name { set; get; }
        public object task_type { set; get; }
        public object plan_date_start { set; get; }
        public object plan_date_end { set; get; }
        public object plan_person { set; get; }
        public object re_plan_date_start { set; get; }
        public object re_plan_date_end { set; get; }
        public object person { set; get; }
        public object must_date_finish { set; get; }
        public object status { set; get; }
        public object note { set; get; }
        public object date_finish { set; get; }
        public object copy_f { set; get; }
        public object del_f { set; get; }
        
    }
}
