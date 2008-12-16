using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Components.Validator;
using System.Web.Mvc;

namespace mvc.Models
{
    public class IncompleteWorkValueReportCell
    {
        private double _value = 0;
        private double _income = 0;

        public double Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }
        public double Income
        {
            get
            {
                return _income;
            }
            set
            {
                _income = value;
            }
        }
        public double Difference
        {
            get
            {
                return Value - Income;
            }
        }
        public string Percent
        {
            get
            {
                if (Value > 0)
                {
                    return (((double)(Difference) / ((double)Value)) * 100.00).ToString("0.00");
                }
                else
                {
                    return "-";

                }
            }
        }
    }
}
