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
    class MyHolder
    {
        public TextView NameTxt;
        public TextView StockValue;
        public ImageView img;

        public MyHolder(View itemView) 
        {
            NameTxt = itemView.FindViewById<TextView>(Resource.Id.nameTxt);
            StockValue = itemView.FindViewById<TextView>(Resource.Id.stockValue);
            img = itemView.FindViewById<ImageView>(Resource.Id.Logo);
        }
    }
}