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

        public static string LineChart(this HtmlHelper helper, string legend, System.Collections.Generic.List<ChartPoint> chartData, string titleX, string titleY)
        {            
            WebChart.ChartControl chartControl = new ChartControl();
            ChartPointCollection chartPoints = new ChartPointCollection();
            chartControl.XTitle.Text = titleX;
            chartControl.YTitle.Text = titleY;
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

        public static string LineChart<T>(this HtmlHelper helper, string[] legends, List<T> data, string XAxeName, string[] YAxes, System.Drawing.Color[] colors, string titleX, string titleY)
        {
            return helper.LineChart<T>(legends, data, XAxeName, YAxes, colors, System.Drawing.Color.White, "", titleX, titleY);
        }

        public static string LineChart<T>(this HtmlHelper helper, string[] legends, List<T> data, string XAxeName, string[] YAxes, System.Drawing.Color[] colors, System.Drawing.Color background, string caption, string titleX, string titleY)
        {
            WebChart.ChartControl chartControl = new ChartControl();
            chartControl.Width = new System.Web.UI.WebControls.Unit("600px");
            chartControl.Height = new System.Web.UI.WebControls.Unit("400px");
            ChartPointCollection chartPoints = new ChartPointCollection();
            List<LineChart> lineCharts = new List<LineChart>();
            chartControl.ChartTitle.Text = caption;
            chartControl.XTitle.Text = titleX;
            chartControl.XTitle.StringFormat.LineAlignment = System.Drawing.StringAlignment.Far;
            chartControl.XTitle.StringFormat.Alignment = System.Drawing.StringAlignment.Far;
            chartControl.XTitle.StringFormat.FormatFlags = System.Drawing.StringFormatFlags.FitBlackBox;
            chartControl.YTitle.Text = titleY;
            chartControl.YTitle.StringFormat.LineAlignment = System.Drawing.StringAlignment.Near;            
            chartControl.YTitle.StringFormat.Alignment = System.Drawing.StringAlignment.Near;
            chartControl.YTitle.StringFormat.FormatFlags = System.Drawing.StringFormatFlags.NoWrap;
            chartControl.ShowTitlesOnBackground = false;
            //chartControl.
            chartControl.YValuesInterval = 20;
            chartControl.BottomChartPadding = 0;

            chartControl.TopChartPadding = 0;
            float maxValue = 0;
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
                    int yValue = int.Parse(yAxe);
                    if (yValue > maxValue)
                    {
                        maxValue = yValue;
                    }
                    lineCharts[i].Data.Add(new WebChart.ChartPoint(xAxe, yValue));
                }
            }
            chartControl.YCustomEnd = ((maxValue / 20) + ((maxValue % 20 > 0) ? 1 : 0)) * 20 + 20;
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

        public static string PieChart<T>(this HtmlHelper helper, string[] legends, T data, string XAxeName, string[] YAxes, System.Drawing.Color[] colors, System.Drawing.Color background, string caption, string prefix, int alpha)
        {
            WebChart.ChartControl chartControl = new ChartControl();
            chartControl.Background.Color = background;
            chartControl.GridLines = GridLines.None;
            chartControl.ChartTitle.Text = caption;

            ChartPointCollection chartPoints = new ChartPointCollection();
            PieChart chart = new PieChart();            
            chart.Colors = new System.Drawing.Color[colors.Length];
            chart.DataLabels.ShowValue = true;
            
            chart.DataLabels.Visible = true;

            int j = 0;
            foreach (System.Drawing.Color color in colors)
            {
                chart.Colors[j] = System.Drawing.Color.FromArgb(alpha, colors[j]);
                j++;
            }
            int total = 0;
            for (int i = 0; i < YAxes.Length; i++)
            {
                string value = GetMemberValue(data, YAxes[i]);
                total += int.Parse(value);
            }
            if (total > 0)
            {
                chart.DataLabels.NumberFormat = "0.00%";
            }
            for (int i = 0; i < YAxes.Length; i++)
            {
                string value = GetMemberValue(data, YAxes[i]);
                float realValue = (total != 0) ? float.Parse(value) / (float)total : 0;
                WebChart.ChartPoint point = new WebChart.ChartPoint(prefix + legends[i], realValue);
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


        public static string PieChart<T>(this HtmlHelper helper, string[] legends, List<T> data, string XAxe, string YAxe, System.Drawing.Color[] colors, System.Drawing.Color background, string caption, string prefix, int alpha)
        {
            WebChart.ChartControl chartControl = new ChartControl();
            chartControl.Background.Color = background;
            chartControl.ChartTitle.Text = caption;

            chartControl.GridLines = GridLines.None;
            ChartPointCollection chartPoints = new ChartPointCollection();
            PieChart chart = new PieChart();
            int i = 0;
            
            //chart.DataLabels.NumberFormat = "0%";
            chart.Colors = new System.Drawing.Color[colors.Length];
            int j = 0;
            foreach (System.Drawing.Color color in colors)
            {
                chart.Colors[j] = System.Drawing.Color.FromArgb(alpha, colors[j]);
                j++;
            }

            int total = 0;
            foreach (T line in data)
            {
                string value = GetMemberValue(line, YAxe);
                total += int.Parse(value);
            }
            if (total > 0)
            {
                chart.DataLabels.ShowValue = true;
                chart.DataLabels.Visible = true;
                chart.DataLabels.NumberFormat = "0.00%";
            }
            foreach (T line in data)
            {
                string value = GetMemberValue(line, YAxe);
                string title = GetMemberValue(line, XAxe);
                WebChart.ChartPoint point = new WebChart.ChartPoint();
                if (total > 0)
                {
                    float realValue = (total != 0) ? float.Parse(value) / (float)total : 0;
                    point = new WebChart.ChartPoint(prefix + title, realValue);
                }
                else
                {
                    point = new WebChart.ChartPoint(prefix + title, int.Parse(value));
                }
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

        public static string BarChart<T>(this HtmlHelper helper, string[] legends, List<T> data, string XAxe, string[] YAxes, System.Drawing.Color[] colors, System.Drawing.Color background, string caption, int alpha, int maxWidth, bool useShadow, int width, int height, string prefix, bool vertical, string titleX, string titleY)
        {
            WebChart.ChartControl chartControl = new ChartControl();
            chartControl.Background.Color = background;
            chartControl.Width = width;
            chartControl.Height = height;
            chartControl.ChartTitle.Text = caption;
            chartControl.XTitle.Text = titleX;
            chartControl.YTitle.Text = titleY;
            
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
            int max = 0;
            foreach (T line in data)
            {
                
                string title = prefix + GetMemberValue(line, XAxe);
                int j = 0;
                foreach (string YAxe in YAxes)
                {
                    string value = GetMemberValue(line, YAxe);
                    int intValue = int.Parse(value);
                    if (max < intValue)
                    {
                        max = intValue;
                    }
                    WebChart.ColumnChart chart = new ColumnChart();
                    WebChart.ChartPoint point = new WebChart.ChartPoint(title, intValue);
                    chartControl.Charts[j].Data.Add(point);
                    j++;
                }
                i++;
            }

            float interval = 25;
            int val = (max / 10) + ((max % 10 > 0)?1:0);
            if (val <= 25)
            {
                interval = 25;
            }
            else if (val <= 50)
            {
                interval = 50;
            }
            else if (val <= 100)
            {
                interval = 100;
            }
            else if (val <= 200)
            {
                interval = 200;
            }
            else if (val <= 250)
            {
                interval = 250;
            }
            else if (val <= 400)
            {
                interval = 400;
            }
            else if (val <= 500)
            {
                interval = 100;
            }
            else if (val <= 1000)
            {
                interval = 1000;
            }
            else if (val <= 2000)
            {
                interval = 2000;
            }
            else if (val <= 2500)
            {
                interval = 2500;
            }
            else if (val <= 5000)
            {
                interval = 5000;
            }
            else if (val <= 10000)
            {
                interval = 10000;
            }
            else if (val <= 20000)
            {
                interval = 20000;
            }
            else if (val <= 25000)
            {
                interval = 25000;
            }
            else if (val <= 50000)
            {
                interval = 50000;
            }
            else if (val <= 100000)
            {
                interval = 100000;
            }
            chartControl.YValuesInterval = interval;
            chartControl.YCustomEnd = interval * 10;
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
