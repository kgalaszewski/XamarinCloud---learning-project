using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace UGAndroidCloud.Models
{
    public class InfoModel
    {
        public string Type { get; set; }
        public string Breed { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
    }
}