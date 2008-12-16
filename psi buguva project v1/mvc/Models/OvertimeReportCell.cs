using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Components.Validator;
using System.Web.Mvc;

namespace mvc.Models
{
    public class OvertimeReportCell
    {
        private int _timeSum = 0;
        private int _timeNormal = 0;

        public int TimeSum
        {
            get
            {
                return _timeSum;
            }
            set
            {
                _timeSum = value;
            }
        }
        public int TimeNormal
        {
            get
            {
                return _timeNormal;
            }
            set
            {
                _timeNormal = value;
            }
        }
        public int TimeOvertime
        {
            get
            {
                if (TimeNormal < TimeSum)
                {
                    return TimeSum - TimeNormal;
                }
                else
                {
                    return 0;
                }
            }
        }
        public string PercentUndertime
        {
            get
            {
                if (TimeSum < TimeNormal)
                {
                    return ((((double)TimeNormal - (double)TimeSum) / (double)TimeNormal) * 100.00).ToString("0.00");
                }
                else
                {
                    return "-";
                }
            }
        }
        public string PercentOvertime
        {
            get
            {
                if (TimeOvertime > 0)
                {
                    return (((double)TimeOvertime / (double)TimeNormal) * 100.00).ToString("0.00");
                }
                else
                {
                    return "-";
                }
            }
        }
    }
}
