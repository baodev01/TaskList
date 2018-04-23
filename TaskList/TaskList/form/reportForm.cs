using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using TaskList.common;

namespace TaskList.form
{
    public partial class reportForm : Form
    {
        public reportForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if(rdoYear.Checked)
            {
                reportByYear(dateTimePicker1.Value);
            } else
            {
                reportByMonth(dateTimePicker2.Value);
            }
        }

        private void reportByYear(DateTime time)
        {
            List<tblReport> reports = new List<tblReport>();
            string monthYear = "";

            for (int i=1; i < 12; i++)
            {
                monthYear = i.ToString().PadLeft(2,'0') + "-" + time.Year;
                reports.AddRange(Report.reportTaskByYear(monthYear));
            }

            this.chart1.Series.Clear();
            this.chart1.Titles.Clear();
            this.chart1.Titles.Add("Report Year " + time.Year);
            Series series = this.chart1.Series.Add("ALL");
            series.ChartType = SeriesChartType.Spline;

            foreach (var r in reports)
            {
                series.Points.AddXY(r.dateTime, r.countTask);
            }
            chart1.Invalidate();

        }

        private void reportByMonth(DateTime time)
        {
            List<tblReport> reports = new List<tblReport>();
            string dayMonthYear = "";

            for (int i = 1; i < DateTime.DaysInMonth(time.Year,time.Month); i++)
            {
                dayMonthYear = i.ToString().PadLeft(2, '0') + "-" + time.Month.ToString().PadLeft(2, '0') + "-" + time.Year;
                reports.AddRange(Report.reportTaskByMonth(dayMonthYear));
            }

            this.chart1.Series.Clear();
            this.chart1.Titles.Clear();
            this.chart1.Titles.Add("Report Month " + time.Month);
            Series series = this.chart1.Series.Add("ALL");
            series.ChartType = SeriesChartType.Spline;
            series.BorderColor = Color.Gray;
            series.Color = Color.Blue;

            foreach (var r in reports)
            {
                series.Points.AddXY(r.dateTime, r.countTask);
            }


            for (int i = 1; i < DateTime.DaysInMonth(time.Year, time.Month); i++)
            {
                dayMonthYear = i.ToString().PadLeft(2, '0') + "-" + time.Month.ToString().PadLeft(2, '0') + "-" + time.Year;
                reports.AddRange(Report.reportTaskByMonth(dayMonthYear));
            }
            this.chart1.Titles.Add("Report Month " + time.Month);
            Series series2 = this.chart1.Series.Add("ID");
            series2.ChartType = SeriesChartType.Spline;
            series2.Color = Color.Red;
            series2.BorderColor = Color.Gray;
            foreach (var r in reports)
            {
                this.chart1.Series["ID"].Points.AddXY(r.dateTime, r.countTask);
            }

            this.chart1.Invalidate();

        }
    }
}
