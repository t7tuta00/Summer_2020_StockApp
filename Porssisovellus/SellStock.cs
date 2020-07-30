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
    [Activity(Label = "SellStock")]
    public class SellStock : Activity
    {
        TextView t;
        TextView t2;
        EditText t3;
        TextView t4;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.sell_stock);

            string Stockname = Intent.GetStringExtra("Stockname");
            string Price = Intent.GetStringExtra("Price");

            t = FindViewById<TextView>(Resource.Id.Stockname);
            t2 = FindViewById<TextView>(Resource.Id.Price);
            t3 = FindViewById<EditText>(Resource.Id.Stockamount);
            t4 = FindViewById<TextView>(Resource.Id.amount);
           

            string Amount = "10";

            t.Text = Stockname;
            t2.Text = Price;
            t4.Text = Amount;

            FindViewById<Button>(Resource.Id.Sell).Click += (o, e) =>
            {
                //Api ja Sql tähän
                Toast.MakeText(this, "Stock sold", ToastLength.Short).Show();
            };


        }
    }
}