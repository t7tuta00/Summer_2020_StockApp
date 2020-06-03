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

namespace Porssisovellus
{
    public class Stock
    {
        //Stock id
        public int sImageDrawable;
        // Name
        public String sName;
        // Stock value
        public String sValue;

        public Stock(int sImageDrawable, String sName, String sValue)
        {
            this.sImageDrawable = sImageDrawable;
            this.sName = sName;
            this.sValue = sValue;
        }

        public int getsImageDrawable()
        {
            return sImageDrawable;
        }

        public void setsImageDrawable(int sImageDrawable)
        {
            this.sImageDrawable = sImageDrawable;
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