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
using Microcharts;
using SkiaSharp;

namespace Porssisovellus
{
    public class Stock
    {
        public int sImageDrawable;
        public int sImageDrawable2;

        public String sName;
        public String sValue;
        public String sPercentage;

        public Stock(int sImageDrawable, String sName, String sValue, int sImageDrawable2, String sPercentage)
        {
            this.sImageDrawable = sImageDrawable;
            this.sName = sName;
            this.sValue = sValue;
            this.sImageDrawable2 = sImageDrawable2;
            this.sPercentage = sPercentage;
        }

        public String getsPercentage()
        {
            return sPercentage;
        }

        public void setsPercentage(String sPercentage)
        {
            this.sPercentage = sPercentage;
        }

        public int getsImageDrawable()
        {
            return sImageDrawable;
        }

        public void setsImageDrawable(int sImageDrawable)
        {
            this.sImageDrawable = sImageDrawable;
        }

        public int getsImageDrawable2()
        {
            return sImageDrawable;
        }

        public void setsImageDrawable2(int sImageDrawable2)
        {
            this.sImageDrawable2 = sImageDrawable2;
        }

        public String getsName()
        {
            return sName;
        }

        public void setsName(String sName)
        {
            this.sName = sName;
        }

        public String getsValue()
        {
            return sValue;
        }

        public void setsValue(String sValue)
        {
            this.sValue = sValue;
        }

    }
}