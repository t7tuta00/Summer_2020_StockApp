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
    [Activity(Label = "Buystock")]
    public class Buystock : Activity
    {
        TextView t;
        TextView t2;
        EditText t3;
        TextView t4;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.buy_stock);

            string Stockname = Intent.GetStringExtra("Stockname");
            string Price = Intent.GetStringExtra("Price");

            string Amount = "10";

            t = FindViewById<TextView>(Resource.Id.Stockname);
            t2 = FindViewById<TextView>(Resource.Id.Price);
            t3 = FindViewById<EditText>(Resource.Id.Stockamount);
            t4 = FindViewById<TextView>(Resource.Id.amount);

            t.Text = Stockname;
            t2.Text = Price;
            t4.Text = Amount;

            FindViewById<Button>(Resource.Id.Buy).Click += (o, e) =>
            {
                //Api ja Sql tähän
                Toast.MakeText(this, "Osakkeet ostettu", ToastLength.Short).Show();
            };


        }
    }
}