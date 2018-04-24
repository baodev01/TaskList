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

        private void btnReport_Click(object sender, EventArgs e)
        {

            if (rdoYear.Checked)
            {
                reportByYear(dateTimePicker1.Value);
            }
            else
            {
                reportByMonth(dateTimePicker2.Value);
            }
        }

        private void reportByYear(DateTime time)
        {
            this.chart1.Series.Clear();
            this.chart1.Titles.Clear();
            this.chart1.Titles.Add("Report Year " + time.Year);

            List<tblReport> reports = new List<tblReport>();
            string monthYear = "";
            for (int i = 1; i < 12; i++)
            {
                monthYear = i.ToString().PadLeft(2, '0') + "-" + time.Year;
                reports.AddRange(Report.reportTaskByYear(monthYear, 0));
            }
            Series series = this.chart1.Series.Add("ALL");
            series.ChartType = SeriesChartType.Column;
            series.Color = Color.Gold;
            foreach (var r in reports)
            {
                series.Points.AddXY(r.dateTime, r.countTask);
            }

            reports = new List<tblReport>();
            for (int i = 1; i < 12; i++)
            {
                monthYear = i.ToString().PadLeft(2, '0') + "-" + time.Year;
                reports.AddRange(Report.reportTaskByYear(monthYear, 1));
            }
            Series series2 = this.chart1.Series.Add("MEP");
            series2.ChartType = SeriesChartType.Column;
            series2.Color = Color.Blue;
            foreach (var r in reports)
            {
                series2.Points.AddXY(r.dateTime, r.countTask);
                //this.chart1.Series["MEP"].Points.AddXY(r.dateTime, r.countTask);
            }

            reports = new List<tblReport>();
            for (int i = 1; i < 12; i++)
            {
                monthYear = i.ToString().PadLeft(2, '0') + "-" + time.Year;
                reports.AddRange(Report.reportTaskByYear(monthYear, 2));
            }
            Series series3 = this.chart1.Series.Add("ID");
            series3.ChartType = SeriesChartType.Column;
            series3.Color = Color.Green;
            foreach (var r in reports)
            {
                series3.Points.AddXY(r.dateTime, r.countTask);
            }

            reports = new List<tblReport>();
            for (int i = 1; i < 12; i++)
            {
                monthYear = i.ToString().PadLeft(2, '0') + "-" + time.Year;
                reports.AddRange(Report.reportTaskByYear(monthYear, 3));
            }
            Series series4 = this.chart1.Series.Add("KC");
            series4.ChartType = SeriesChartType.Column;
            series4.Color = Color.Red;
            foreach (var r in reports)
            {
                series4.Points.AddXY(r.dateTime, r.countTask);
            }

            chart1.Invalidate();
        }

        private void reportByMonth(DateTime time)
        {
            this.chart1.Series.Clear();
            this.chart1.Titles.Clear();
            this.chart1.Titles.Add("Report Month " + time.Month);

            List<tblReport> reports = new List<tblReport>();
            string dayMonthYear = "";

            for (int i = 1; i < DateTime.DaysInMonth(time.Year, time.Month); i++)
            {
                dayMonthYear = i.ToString().PadLeft(2, '0') + "-" + time.Month.ToString().PadLeft(2, '0') + "-" + time.Year;
                reports.AddRange(Report.reportTaskByMonth(dayMonthYear, 0));
            }
            Series series = this.chart1.Series.Add("ALL");
            series.ChartType = SeriesChartType.Column;
            series.Color = Color.Gold;
            foreach (var r in reports)
            {
                series.Points.AddXY(r.dateTime, r.countTask);
            }

            reports = new List<tblReport>();
            for (int i = 1; i < DateTime.DaysInMonth(time.Year, time.Month); i++)
            {
                dayMonthYear = i.ToString().PadLeft(2, '0') + "-" + time.Month.ToString().PadLeft(2, '0') + "-" + time.Year;
                reports.AddRange(Report.reportTaskByMonth(dayMonthYear, 1));
            }
            Series series2 = this.chart1.Series.Add("MEP");
            series2.ChartType = SeriesChartType.Column;
            series2.Color = Color.Blue;
            foreach (var r in reports)
            {
                series2.Points.AddXY(r.dateTime, r.countTask);
                //this.chart1.Series["MEP"].Points.AddXY(r.dateTime, r.countTask);
            }

            reports = new List<tblReport>();
            for (int i = 1; i < DateTime.DaysInMonth(time.Year, time.Month); i++)
            {
                dayMonthYear = i.ToString().PadLeft(2, '0') + "-" + time.Month.ToString().PadLeft(2, '0') + "-" + time.Year;
                reports.AddRange(Report.reportTaskByMonth(dayMonthYear, 2));
            }
            Series series3 = this.chart1.Series.Add("ID");
            series3.ChartType = SeriesChartType.Column;
            series3.Color = Color.Green;
            foreach (var r in reports)
            {
                series3.Points.AddXY(r.dateTime, r.countTask);
            }

            reports = new List<tblReport>();
            for (int i = 1; i < DateTime.DaysInMonth(time.Year, time.Month); i++)
            {
                dayMonthYear = i.ToString().PadLeft(2, '0') + "-" + time.Month.ToString().PadLeft(2, '0') + "-" + time.Year;
                reports.AddRange(Report.reportTaskByMonth(dayMonthYear, 3));
            }
            Series series4 = this.chart1.Series.Add("KC");
            series4.ChartType = SeriesChartType.Column;
            series4.Color = Color.Red;
            foreach (var r in reports)
            {
                series4.Points.AddXY(r.dateTime, r.countTask);
            }

            this.chart1.Invalidate();

        }

        private void reportForm_Load(object sender, EventArgs e)
        {
            btnReport_Click(null, null);
        }

    }
}
