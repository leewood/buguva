﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

using mvc.Models;
using mvc.Common;

namespace mvc.Views.Projects
{
    public partial class ListMyProjects : ViewPage<mvc.Common.IPagedList<Project>>
    {
        public string className = "";
    }
}
