using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DotLiquid.Tags;
using Java.Lang;
using Java.Util;

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

            //Bind data
            MyHolder holder = new MyHolder(convertView);
            holder.NameTxt.Text = stocks[position].sName;
            holder.StockValue.Text = stocks[position].sValue;
            holder.img.SetImageResource(stocks[position].sImageDrawable);

            convertView.SetBackgroundColor(Android.Graphics.Color.LightBlue);

            return convertView;
        }



    }

   
}
