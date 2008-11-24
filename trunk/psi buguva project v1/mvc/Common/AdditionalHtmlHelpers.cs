using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using WebChart;

namespace mvc.Common
{   
    public class ChartPoint
    {
        public string x;
        public float y;
        public ChartPoint(string x, float y)
        {
            this.x = x;
            this.y = y;
        }

    }

    public static class AdditionalHtmlHelpers
    {
        public static string Label(this HtmlHelper helper, string target, string text)
        { 
            return String.Format("<label for='{0}'>{1}</label>", target, text); 
        }

        public static string Path(this HtmlHelper helper)
        {
            return "";
        }

        public static string ActionImageLink(this HtmlHelper helper, string imagePath, string alt, string action, object values)
        {
            return helper.ActionImageLink(imagePath, alt, action, values, false, "");
        }

        public static string ActionImageLink(this HtmlHelper helper, string imagePath, string alt, string action, object values, bool useConfirm, string confirmationText)
        {
            string returnActionLink = "";
            if (useConfirm)
            {
                returnActionLink = helper.ActionLink("insert_place_for_img", action, values, new { onclick = "javascript:return confirm('" + confirmationText + "')" });
            }
            else
            {
                returnActionLink = helper.ActionLink("insert_place_for_img", action, values);
            }
            string replaceWith = String.Format("<img src=\"{0}\" alt = \"{1}\"/>", imagePath, HttpUtility.HtmlEncode(alt));
            return returnActionLink.Replace("insert_place_for_img", replaceWith);
        }

        public static string LineChart(this HtmlHelper helper, string legend, System.Collections.Generic.List<ChartPoint> chartData)
        {            
            WebChart.ChartControl chartControl = new ChartControl();
            ChartPointCollection chartPoints = new ChartPointCollection();
            foreach (ChartPoint point in chartData)
            {
                chartPoints.Add(new WebChart.ChartPoint(point.x, point.y));
            }
            LineChart lineChart = new LineChart(chartPoints);
            lineChart.Legend = legend;
            chartControl.Charts.Add(lineChart);
            chartControl.RedrawChart();
            System.IO.StringWriter stringWriter = new System.IO.StringWriter();
            System.IO.TextWriter textWiter = stringWriter;
            System.Web.UI.HtmlTextWriter writer = new System.Web.UI.HtmlTextWriter(textWiter);
            chartControl.RenderControl(writer);
            string result = stringWriter.ToString();
            return result;
        }



        public static string GetMemberValue<T>(T data, string name)
        {
            if (data.GetType().GetField(name) != null)
            {
                return data.GetType().GetField(name).GetValue(data).ToString();
            }
            else if (data.GetType().GetProperty(name) != null)
            {
                return data.GetType().GetProperty(name).GetValue(data, null).ToString();
            }
            else
            {
                return "";
            }
        }

        public static string LineChart<T>(this HtmlHelper helper, string[] legends, List<T> data, string XAxeName, string[] YAxes, System.Drawing.Color[] colors)
        {
            return helper.LineChart<T>(legends, data, XAxeName, YAxes, colors, System.Drawing.Color.White, "");
        }

        public static string LineChart<T>(this HtmlHelper helper, string[] legends, List<T> data, string XAxeName, string[] YAxes, System.Drawing.Color[] colors, System.Drawing.Color background, string caption)
        {
            WebChart.ChartControl chartControl = new ChartControl();
            chartControl.Width = new System.Web.UI.WebControls.Unit("600px");
            chartControl.Height = new System.Web.UI.WebControls.Unit("400px");
            ChartPointCollection chartPoints = new ChartPointCollection();
            List<LineChart> lineCharts = new List<LineChart>();
            chartControl.ChartTitle.Text = caption;
            for (int i = 0; i < legends.Length; i++)
            {
                LineChart chart = new LineChart();
                chart.Legend = legends[i];                
                chart.Line.Color = colors[i];
                chart.Fill.Color = colors[i];
                chart.Fill.ForeColor = System.Drawing.Color.Black;
                lineCharts.Add(chart);
                
            }

            foreach (T line in data)
            {
                string xAxe = GetMemberValue(line, XAxeName);
                for (int i = 0; i < legends.Length; i++)
                {
                    string yAxe = GetMemberValue(line, YAxes[i]);
                    lineCharts[i].Data.Add(new WebChart.ChartPoint(xAxe, int.Parse(yAxe)));
                }
            }
            foreach (LineChart chart in lineCharts)
            {
                chartControl.Charts.Add(chart);
            }
            chartControl.Background.Color = background;
            chartControl.RedrawChart();
            System.IO.StringWriter stringWriter = new System.IO.StringWriter();
            System.IO.TextWriter textWiter = stringWriter;
            System.Web.UI.HtmlTextWriter writer = new System.Web.UI.HtmlTextWriter(textWiter);
            chartControl.RenderControl(writer);
            string result = stringWriter.ToString();
            return result;
        }

        public static string PieChart<T>(this HtmlHelper helper, string[] legends, T data, string XAxeName, string[] YAxes, System.Drawing.Color[] colors, System.Drawing.Color background, string caption, string prefix)
        {
            WebChart.ChartControl chartControl = new ChartControl();
            chartControl.Background.Color = background;
            chartControl.ChartTitle.Text = caption;
            ChartPointCollection chartPoints = new ChartPointCollection();
            PieChart chart = new PieChart();
            chart.Colors = colors;
            for (int i = 0; i < YAxes.Length; i++)
            {
                string value = GetMemberValue(data, YAxes[i]);
                WebChart.ChartPoint point = new WebChart.ChartPoint(prefix + legends[i], int.Parse(value));
                chart.Data.Add(point);
                
            }

            chartControl.Charts.Add(chart);
            chartControl.RedrawChart();
            System.IO.StringWriter stringWriter = new System.IO.StringWriter();
            System.IO.TextWriter textWiter = stringWriter;
            System.Web.UI.HtmlTextWriter writer = new System.Web.UI.HtmlTextWriter(textWiter);
            chartControl.RenderControl(writer);
            string result = stringWriter.ToString();
            return result;
        }


        public static string PieChart<T>(this HtmlHelper helper, string[] legends, List<T> data, string XAxe, string YAxe, System.Drawing.Color[] colors, System.Drawing.Color background, string caption, string prefix)
        {
            WebChart.ChartControl chartControl = new ChartControl();
            chartControl.Background.Color = background;
            chartControl.ChartTitle.Text = caption;
            ChartPointCollection chartPoints = new ChartPointCollection();
            PieChart chart = new PieChart();
            int i = 0;
            chart.Colors = colors;
            foreach (T line in data)
            {
                string value = GetMemberValue(line, YAxe);
                string title = GetMemberValue(line, XAxe);
                WebChart.ChartPoint point = new WebChart.ChartPoint(prefix + title, int.Parse(value));
                chart.Data.Add(point);
                
                i++;
            }

            chartControl.Charts.Add(chart);
            chartControl.RedrawChart();
            System.IO.StringWriter stringWriter = new System.IO.StringWriter();
            System.IO.TextWriter textWiter = stringWriter;
            System.Web.UI.HtmlTextWriter writer = new System.Web.UI.HtmlTextWriter(textWiter);
            chartControl.RenderControl(writer);
            string result = stringWriter.ToString();
            return result;
        }

        public static string BarChart<T>(this HtmlHelper helper, string[] legends, List<T> data, string XAxe, string[] YAxes, System.Drawing.Color[] colors, System.Drawing.Color background, string caption, int alpha, int maxWidth, bool useShadow, int width, int height, string prefix, bool vertical)
        {
            WebChart.ChartControl chartControl = new ChartControl();
            chartControl.Background.Color = background;
            chartControl.Width = width;
            chartControl.Height = height;
            chartControl.ChartTitle.Text = caption;
            if (vertical)
            {
                chartControl.XAxisFont.StringFormat.FormatFlags = System.Drawing.StringFormatFlags.DirectionVertical;
                chartControl.XAxisFont.StringFormat.Alignment = System.Drawing.StringAlignment.Center;
                chartControl.XAxisFont.StringFormat.LineAlignment = System.Drawing.StringAlignment.Center;
                chartControl.BottomChartPadding = 50;
            }
            ChartPointCollection chartPoints = new ChartPointCollection();
            //WebChart.ColumnChart chart = new ColumnChart();            
            int i = 0;
            for (int j = 0; j < YAxes.Length; j++)
            {
                chartControl.Charts.Add(new WebChart.StackedColumnChart());
                chartControl.Charts[j].Fill.Color = System.Drawing.Color.FromArgb(alpha, colors[j]);
                chartControl.Charts[j].Legend = legends[j];
                chartControl.Charts[j].Shadow.Visible = useShadow;
                
                ((WebChart.StackedColumnChart)(chartControl.Charts[j])).MaxColumnWidth = maxWidth;
            }

            foreach (T line in data)
            {
                
                string title = prefix + GetMemberValue(line, XAxe);
                int j = 0;
                foreach (string YAxe in YAxes)
                {
                    string value = GetMemberValue(line, YAxe);
                    WebChart.ColumnChart chart = new ColumnChart();
                    WebChart.ChartPoint point = new WebChart.ChartPoint(title, int.Parse(value));
                    chartControl.Charts[j].Data.Add(point);
                    j++;
                }
                i++;
            }

            
            chartControl.RedrawChart();
            System.IO.StringWriter stringWriter = new System.IO.StringWriter();
            System.IO.TextWriter textWiter = stringWriter;
            System.Web.UI.HtmlTextWriter writer = new System.Web.UI.HtmlTextWriter(textWiter);
            chartControl.RenderControl(writer);
            string result = stringWriter.ToString();
            return result;
        }


    }

    
}
