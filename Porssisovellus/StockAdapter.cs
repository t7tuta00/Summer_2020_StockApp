using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DotLiquid.Tags;
using DotLiquid.Util;
using Java.Lang;
using Java.Util;
using Microcharts;
using Microcharts.Droid;
using SkiaSharp;

namespace Porssisovellus 
{
    class StockAdapter : ArrayAdapter
    {
        private Context c;
        private List<Stock> stocks;
        private int resource;
        private LayoutInflater inflater;

        public StockAdapter(Context context, int resource, List<Stock> objects) : base(context, resource, objects) 
        {
            this.c = context;
            this.resource = resource;
            this.stocks = objects;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (inflater == null) 
            {
                inflater = (LayoutInflater)c.GetSystemService(Context.LayoutInflaterService);
            } 
            
            if(convertView == null) 
            {
                convertView = inflater.Inflate(resource, parent, false);
            }

            MyHolder holder = new MyHolder(convertView);
            holder.NameTxt.Text = stocks[position].sName;
            holder.StockValue.Text = stocks[position].sValue;
            holder.img.SetImageResource(stocks[position].sImageDrawable);
            holder.img2.SetImageResource(stocks[position].sImageDrawable2);
            holder.Percentage.Text = stocks[position].sPercentage;
            
            string percentageValue = stocks[position].sPercentage;
            double percentageValue2 = double.Parse(percentageValue);

            convertView.SetBackgroundColor(Android.Graphics.Color.White);

            return convertView;
        }



    }

   
}
