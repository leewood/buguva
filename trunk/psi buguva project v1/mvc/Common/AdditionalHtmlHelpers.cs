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
            string returnActionLink = helper.ActionLink("insert_place_for_img", action, values);
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
            WebChart.ChartControl chartControl = new ChartControl();
            ChartPointCollection chartPoints = new ChartPointCollection();
            List<LineChart> lineCharts = new List<LineChart>();
            for (int i = 0; i < legends.Length; i++)
            {
                LineChart chart = new LineChart();
                chart.Legend = legends[i];
                chart.Line.Color = colors[i];
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
