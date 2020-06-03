using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Android.Content;
using Android.Util;
using System.Diagnostics;
using System;
using System.Net.Http;
using System.Net;
using System.IO;
using Org.Apache.Http.Client.Params;
using Org.Apache.Http.Client;
using Newtonsoft.Json.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;


namespace Porssisovellus
{

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public static string Symbol = "NULL";
        public static string Name = "NULL";
        public static string Price = "NULL";

        string AppleStatus = "false";
        string MicrosoftStatus = "false";
        string GoogleStatus = "false";

        private StockAdapter adapter;
        private ListView lv;
        private List<Stock> stocks;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            
            GetStocksasync();

            //asetukset-nappi
            FindViewById<ImageButton>(Resource.Id.imagebutton1).Click += (o, e) => {
                Intent nextActivity = new Intent(this, typeof(CheckStockActivity));
                nextActivity.PutExtra("Apple", AppleStatus);
                nextActivity.PutExtra("Microsoft", MicrosoftStatus);
                nextActivity.PutExtra("Google", GoogleStatus);
                StartActivity(nextActivity);
            };

            lv = FindViewById<ListView>(Resource.Id.stocklist);
            lv.ItemClick += lv_ItemClick;

            //Käyttäjän valinnat CheckStock-activitysta
            AppleStatus = Intent.GetStringExtra("Apple");
            MicrosoftStatus = Intent.GetStringExtra("Microsoft");
            GoogleStatus = Intent.GetStringExtra("Google");

            adapter = new StockAdapter(this, Resource.Layout.list_item, GetStocks());
            lv.Adapter = adapter;

        }

        private void lv_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Toast.MakeText(this, stocks[e.Position].sName, ToastLength.Short).Show();
        }

        
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private static async void GetStocksasync()
        {
            
            using (HttpClient client = new HttpClient())
            {
                //https://financialmodelingprep.com/api/v3/quote/AAPL,FB?apikey=e1705fed955b668a39dd7274bde86c53
                //https://financialmodelingprep.com/api/v3/quote/AAPL?apikey=demo

                using (HttpResponseMessage response = await client.GetAsync("https://financialmodelingprep.com/api/v3/quote/AAPL?apikey=demo"))
                {
                    using (HttpContent content = response.Content)
                    {
                        string mycontent = await content.ReadAsStringAsync();

                        JArray jsonArray = JArray.Parse(mycontent);
                        var data = JObject.Parse(jsonArray[0].ToString());
                        //System.Diagnostics.Debug.WriteLine(data);
                        //System.Diagnostics.Debug.WriteLine(data["price"]);

                        string name = data["name"].ToString();
                        string price = data["price"].ToString();

                        System.Diagnostics.Debug.WriteLine(name);
                        System.Diagnostics.Debug.WriteLine(price);

                        Price = price;
                        Name = name;
                       
                    }
                }
            }
        }

        private List<Stock> GetStocks()
        {
            stocks = new List<Stock>();
            
            if (AppleStatus == "true") 
            {
                stocks.Add(new Stock(Resource.Drawable.stockpic, Name, Price));
            }

            if (MicrosoftStatus == "true")
            {
                stocks.Add(new Stock(Resource.Drawable.stockpic, "Microsoft", "1000"));
            }

            if (GoogleStatus == "true")
            {
                stocks.Add(new Stock(Resource.Drawable.stockpic, "Google", "1000"));
            }

            return stocks; 
        }
        
}   }