﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.GoogleAnalytics.Models
{
    public class PublicInfoModel
    {
        public string TrackingScript { get; set; }
        public bool AddCookieConsentChangedScriptsToFooter { get; set; }
    }
}
