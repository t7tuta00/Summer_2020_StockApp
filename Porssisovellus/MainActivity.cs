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
using System.Threading.Tasks;
using Java.Util.Prefs;
using Android.Graphics;
using System.Drawing;

namespace Porssisovellus
{

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        string Data;
        string Symbol;
        string Name;
        string Price;
        string ChangesPercentage;
        string Change;
        string DayLow;
        string DayHigh;
        string YearHigh;
        string YearLow;
        string MarketCap;
        string PriceAvg50;
        string PriceAvg200;
        string Volume;
        string AvgVolume;
        string Exchange;
        string Open;
        string PreviousClose;
        string Eps;
        string Pe;
        string EarningsAnnouncement;
        string SharesOutstanding;
        string Timestamp;

        string AppleStatus;
        string MicrosoftStatus;
        string GoogleStatus;

        private StockAdapter adapter;
        private ListView lv;
        private List<Stock> stocks;
        
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            AppleStatus = Xamarin.Essentials.Preferences.Get("apple", "default_value");
            MicrosoftStatus = Xamarin.Essentials.Preferences.Get("micro", "default_value");
            GoogleStatus = Xamarin.Essentials.Preferences.Get("google", "default_value");

            
            //Käyttäjän valinnat CheckStock-activitysta
            if (Intent.GetStringExtra("Apple") != null)
            {
                AppleStatus = Intent.GetStringExtra("Apple");
            }

            if (Intent.GetStringExtra("Apple") != null)
            {
                MicrosoftStatus = Intent.GetStringExtra("Microsoft");
            }
            if (Intent.GetStringExtra("Apple") != null)
            {
                GoogleStatus = Intent.GetStringExtra("Google");
            }

            if (AppleStatus == "true")
            {
                System.Diagnostics.Debug.WriteLine("APPLE VALITTU");
                Xamarin.Essentials.Preferences.Set("apple", "true");
            }
            else
            {
                Xamarin.Essentials.Preferences.Set("apple", "false");
                //Xamarin.Essentials.Preferences.Remove("apple");
            }

            if (MicrosoftStatus == "true")
            {
                System.Diagnostics.Debug.WriteLine("MICROSOFT VALITTU");
                Xamarin.Essentials.Preferences.Set("micro", "true");
            }
            else
            {
                Xamarin.Essentials.Preferences.Set("micro", "false");
                //Xamarin.Essentials.Preferences.Remove("apple");
            }

            if (GoogleStatus == "true")
            {
                System.Diagnostics.Debug.WriteLine("GOOGLE VALITTU");
                Xamarin.Essentials.Preferences.Set("google", "true");
            }
            else
            {
                Xamarin.Essentials.Preferences.Set("google", "false");
                //Xamarin.Essentials.Preferences.Remove("apple");
            }

            await GetStocksasync();

            FindViewById<Button>(Resource.Id.button1).Click += (o, e) =>
            {
                Intent nextActivity = new Intent(this, typeof(CheckStockActivity));
                nextActivity.PutExtra("Apple", AppleStatus);
                nextActivity.PutExtra("Microsoft", MicrosoftStatus);
                nextActivity.PutExtra("Google", GoogleStatus);
                StartActivity(nextActivity);
            };


            lv = FindViewById<ListView>(Resource.Id.stocklist);
            adapter = new StockAdapter(this, Resource.Layout.list_item, GetStocks());
            lv.Adapter = adapter;

            lv.ItemClick += lv_ItemClick;
        }

        private void lv_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Toast.MakeText(this, stocks[e.Position].sName, ToastLength.Short).Show();
            Intent nextActivity = new Intent(this, typeof(StockInfoActivity));

            string stockname = stocks[e.Position].sName;
            nextActivity.PutExtra("Stockname", stockname);
            nextActivity.PutExtra("Symbol", Symbol);
            nextActivity.PutExtra("Name", Name);
            nextActivity.PutExtra("Price", Price);
            nextActivity.PutExtra("ChangesPercentage", ChangesPercentage);
            nextActivity.PutExtra("Change", Change);
            nextActivity.PutExtra("DayLow", DayLow);
            nextActivity.PutExtra("DayHigh", DayHigh);
            nextActivity.PutExtra("YearHigh", YearHigh);
            nextActivity.PutExtra("YearLow", YearLow);
            nextActivity.PutExtra("MarketCap", MarketCap);
            nextActivity.PutExtra("PriceAvg50", PriceAvg50);
            nextActivity.PutExtra("PriceAvg200", PriceAvg200);
            nextActivity.PutExtra("Volume", Volume);
            nextActivity.PutExtra("AvgVolume", AvgVolume);
            nextActivity.PutExtra("Exchange", Exchange);
            nextActivity.PutExtra("Open", Open);
            nextActivity.PutExtra("PreviousClose", PreviousClose);
            nextActivity.PutExtra("Eps", Eps);
            nextActivity.PutExtra("Pe", Pe);
            nextActivity.PutExtra("EarningsAnnouncement", EarningsAnnouncement);
            nextActivity.PutExtra("SharesOutstanding", SharesOutstanding);
            nextActivity.PutExtra("Timestamp", Timestamp);

            StartActivity(nextActivity);
        }

        public async Task GetStocksasync()
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync("https://financialmodelingprep.com/api/v3/quote/AAPL?apikey=demo"))
                {
                    using (HttpContent content = response.Content)
                    {
                        string mycontent = await content.ReadAsStringAsync();

                        JArray jsonArray = JArray.Parse(mycontent);
                        var data = JObject.Parse(jsonArray[0].ToString());

                        Data = data.ToString();
                        Symbol = data["symbol"].ToString();
                        Name = data["name"].ToString();
                        Price = data["price"].ToString();
                        ChangesPercentage = data["changesPercentage"].ToString();
                        Change = data["change"].ToString();
                        DayLow = data["dayLow"].ToString();
                        DayHigh = data["dayHigh"].ToString();
                        YearHigh = data["yearHigh"].ToString();
                        YearLow = data["yearLow"].ToString();
                        MarketCap = data["marketCap"].ToString();
                        PriceAvg50 = data["priceAvg50"].ToString();
                        PriceAvg200 = data["priceAvg200"].ToString();
                        Volume = data["volume"].ToString();
                        AvgVolume = data["avgVolume"].ToString();
                        Exchange = data["exchange"].ToString();
                        Open = data["open"].ToString();
                        PreviousClose = data["previousClose"].ToString();
                        Eps = data["eps"].ToString();
                        Pe = data["pe"].ToString();
                        EarningsAnnouncement = data["earningsAnnouncement"].ToString();
                        SharesOutstanding = data["sharesOutstanding"].ToString();
                        Timestamp = DateTime.Now.ToString();
                    }
                }
            }
        }

        private List<Stock> GetStocks()
        {
            stocks = new List<Stock>();

            if (AppleStatus == "true")
            {
                double ChangesPercentage2 = double.Parse(ChangesPercentage);

                if (ChangesPercentage2 > 0)
                {
                    ChangesPercentage = "+" + ChangesPercentage;
                    stocks.Add(new Stock(Resource.Drawable.Applelogo2, Name, Price, Resource.Drawable.arrowUP, ChangesPercentage));
                    
                }
                if (ChangesPercentage2 < 0)
                {
                    stocks.Add(new Stock(Resource.Drawable.Applelogo2, Name, Price, Resource.Drawable.arrowDOWN, ChangesPercentage));
                    
                }
                if (ChangesPercentage2 == 0)
                {
                    stocks.Add(new Stock(Resource.Drawable.Applelogo2, Name, Price, Resource.Drawable.arrowSTRAIGHT, ChangesPercentage));
               
                }
            }

            if (MicrosoftStatus == "true")
            {
                //EXAMPLE
                double ChangesPercentage2 = 2.5;

                if (ChangesPercentage2 > 0)
                {
                    ChangesPercentage = "+" + ChangesPercentage2.ToString();
                    stocks.Add(new Stock(Resource.Drawable.Micrologo2, "Microsoft", "100", Resource.Drawable.arrowUP, ChangesPercentage));
                }
                if (ChangesPercentage2 < 0)
                {
                    stocks.Add(new Stock(Resource.Drawable.Micrologo2, "Microsoft", "100", Resource.Drawable.arrowDOWN, "-2.5"));
                }
                if (ChangesPercentage2 == 0)
                {
                    stocks.Add(new Stock(Resource.Drawable.Micrologo2, "Microsoft", "100", Resource.Drawable.arrowSTRAIGHT, "2.5"));
                }


            }

                
            if (GoogleStatus == "true")
            {
                //EXAMPLE
                double ChangesPercentage2 = -2.5;

                if (ChangesPercentage2 > 0)
                {
                    ChangesPercentage = "+" + ChangesPercentage2.ToString();
                    stocks.Add(new Stock(Resource.Drawable.Google_logo, "Google", "100", Resource.Drawable.arrowUP, ChangesPercentage.ToString()));
                }
                if (ChangesPercentage2 < 0)
                {
                    stocks.Add(new Stock(Resource.Drawable.Google_logo, "Google", "100", Resource.Drawable.arrowDOWN, ChangesPercentage2.ToString()));
                }

                if (ChangesPercentage2 == 0)
                {
                    stocks.Add(new Stock(Resource.Drawable.Google_logo, "Google", "100", Resource.Drawable.arrowSTRAIGHT, "0"));
                }

            }

            return stocks;
        }

    }
        
}