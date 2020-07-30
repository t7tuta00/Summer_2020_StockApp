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
using Microcharts.Droid;

namespace Porssisovellus
{
    class MyHolder
    {
        public TextView NameTxt;
        public TextView StockValue;
        public ImageView img;
        public ImageView img2;
        public TextView Percentage;

        public MyHolder()
        {
        }

        public MyHolder(View itemView) 
        {
            NameTxt = itemView.FindViewById<TextView>(Resource.Id.nameTxt);
            StockValue = itemView.FindViewById<TextView>(Resource.Id.stockValue);
            img = itemView.FindViewById<ImageView>(Resource.Id.Logo);
            img2 = itemView.FindViewById<ImageView>(Resource.Id.Status);
            Percentage = itemView.FindViewById<TextView>(Resource.Id.percentValue);
        }

    }
}