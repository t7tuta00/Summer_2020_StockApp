using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util;
using Microcharts;
using Microcharts.Droid;
using Newtonsoft.Json.Linq;
using Plugin.Settings;
using SkiaSharp;
using Java.Nio.FileNio;
using Java.Lang;
using System.Threading.Tasks;
using Android.Text.Format;
using System.Diagnostics.Tracing;




namespace Porssisovellus
{
    [Activity(Label = "StockInfoActivity")]
    public class StockInfoActivity : Activity
    {
        private static System.Timers.Timer aTimer;

        string Stockname;
        string Symbol;
        string Name;
        static string Price;
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
        static string Timestamp;

        static float Price2;
        int counter = 0;

        TextView t;
        TextView t2;
        TextView t3;
        TextView amount;

        ImageView v;
        Button buy;
        Button sell;

        TextView t4;
        TextView t5;
        TextView t6;
        TextView t7;
        TextView t8;
        TextView t9;
        TextView t10;
        TextView t11;
        TextView t12;

        public List<Entry> entries = new List<Entry>
        {
            /*new Entry(Price2)
            {
                Color = SKColor.Parse("#FF1943"),
                Label = "",
                ValueLabel = ""
            }*/

           /* new Entry(100)
            {
                Color = SKColor.Parse("#FF1493"),
                Label = "February",
                ValueLabel = "100"
            },

            new Entry(50)
            {
                Color = SKColor.Parse("#00BFFF"),
                Label = "March",
                ValueLabel = "50"
            },

            new Entry(400)
            {
                Color = SKColor.Parse("#00CED1"),
                Label = "April",
                ValueLabel = "400"
            },
           */
        };

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.stock_info);

            string Stockamount = "10";

            Stockname = Intent.GetStringExtra("Stockname");
            Symbol = Intent.GetStringExtra("Symbol");
            Name = Intent.GetStringExtra("Name");
            Price = Intent.GetStringExtra("Price");
            ChangesPercentage = Intent.GetStringExtra("ChangesPercentage");
            Change = Intent.GetStringExtra("Change");
            DayLow = Intent.GetStringExtra("DayLow");
            DayHigh = Intent.GetStringExtra("DayHigh");
            YearHigh = Intent.GetStringExtra("YearHigh");
            YearLow = Intent.GetStringExtra("YearLow");
            MarketCap = Intent.GetStringExtra("MarketCap");
            PriceAvg50 = Intent.GetStringExtra("PriceAvg50");
            PriceAvg200 = Intent.GetStringExtra("PriceAvg200");
            Volume = Intent.GetStringExtra("Volume");
            AvgVolume = Intent.GetStringExtra("AvgVolume");
            Exchange = Intent.GetStringExtra("Exchange");
            Open = Intent.GetStringExtra("Open");
            PreviousClose = Intent.GetStringExtra("PreviousClose");
            Eps = Intent.GetStringExtra("Eps");
            Pe = Intent.GetStringExtra("Pe");
            EarningsAnnouncement = Intent.GetStringExtra("EarningsAnnouncement");
            SharesOutstanding = Intent.GetStringExtra("SharesOutstanding");
            Timestamp = Intent.GetStringExtra("Timestamp");

            //amount = Intent.GetStringExtra("Stockamount");

            t = FindViewById<TextView>(Resource.Id.textView1);
            t2 = FindViewById<TextView>(Resource.Id.textView2);
            t3 = FindViewById<TextView>(Resource.Id.textView3);
            buy = FindViewById<Button>(Resource.Id.Buy);
            sell = FindViewById<Button>(Resource.Id.Sell);
            v = FindViewById<ImageView>(Resource.Id.Logo);
            amount = FindViewById<TextView>(Resource.Id.amount);

            t4 = FindViewById<TextView>(Resource.Id.Change_Percentage);
            t5 = FindViewById<TextView>(Resource.Id.Change);
            t6 = FindViewById<TextView>(Resource.Id.Daylow);
            t7 = FindViewById<TextView>(Resource.Id.DayHigh);
            t8 = FindViewById<TextView>(Resource.Id.YearHigh);
            t9 = FindViewById<TextView>(Resource.Id.YearLow);
            t10 = FindViewById<TextView>(Resource.Id.PriceAvg50);
            t11 = FindViewById<TextView>(Resource.Id.PriceAvg200);
            t12 = FindViewById<TextView>(Resource.Id.Pe);


            t.Text = Name;
            t2.Text = "Price: " + Price;
            t3.Text = "Changes: " + ChangesPercentage;

            t4.Text = ChangesPercentage;
            t5.Text = Change;
            t6.Text = DayLow;
            t7.Text = DayHigh;
            t8.Text = YearHigh;
            t9.Text = YearLow;
            t10.Text = PriceAvg50;
            t11.Text = PriceAvg200;
            t12.Text = Pe;

            if (Stockname == "Apple Inc.") 
            {
                v.SetImageResource(Resource.Drawable.Applelogo2);
            }

            if (Stockname == "Microsoft")
            {
                v.SetImageResource(Resource.Drawable.Micrologo2);
            }

            if (Stockname == "Google")
            {
                v.SetImageResource(Resource.Drawable.Google_logo);
            }


            amount.Text = Stockamount;

            Price2 = float.Parse(Price);

            var chartView = FindViewById<ChartView>(Resource.Id.chartView1);
            var chart = new LineChart() { Entries = entries };
            chartView.Chart = chart;

            aTimer = new System.Timers.Timer();
            aTimer.Interval = 1000;
            aTimer.Elapsed += OnTimedEvend;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;

            FindViewById<Button>(Resource.Id.Buy).Click += async (o, e) =>
            {
                Intent nextActivity = new Intent(this, typeof(Buystock));
                nextActivity.PutExtra("Stockname", Stockname);
                nextActivity.PutExtra("Price", Price);
                StartActivity(nextActivity);
            };

            FindViewById<Button>(Resource.Id.Sell).Click += async (o, e) =>
            {
                Intent nextActivity = new Intent(this, typeof(SellStock));
                nextActivity.PutExtra("Stockname", Stockname);
                nextActivity.PutExtra("Price", Price);
                StartActivity(nextActivity);
            };
        }

         

        private void OnTimedEvend(object source, System.Timers.ElapsedEventArgs e)
        {
            t = FindViewById<TextView>(Resource.Id.textView1);
            t2 = FindViewById<TextView>(Resource.Id.textView2);
            t3 = FindViewById<TextView>(Resource.Id.textView3);

            t.Text = Name;
            t2.Text = "Price: " + Price;
            //t3.Text = "Changes: " + ChangesPercentage;
            
            Price2 = float.Parse(Price);

            var chartView = FindViewById<ChartView>(Resource.Id.chartView1);
            var chart = new LineChart() { Entries = entries };
            CreateEntry();
            chartView.Chart = chart;
        }

        private async void CreateEntry()
        {
            await GetStocksasync();

            entries.Add(new Entry(Price2)
            {
                Color = SKColor.Parse("#b8b2b4"),
                Label = Timestamp,
                ValueLabel = Price
            });

            if (counter == 4) 
            {
                entries.RemoveAt(0);
                counter--;
            }

            counter++;
        }

        public async Task GetStocksasync()
        {
            using (HttpClient client = new HttpClient())
            {
                //AAPL,GOOG,MSFT
                using (HttpResponseMessage response = await client.GetAsync("https://financialmodelingprep.com/api/v3/quote/"+"AAPL"+"?apikey=demo"))
                {
                    using (HttpContent content = response.Content)
                    {
                        string mycontent = await content.ReadAsStringAsync();

                        JArray jsonArray = JArray.Parse(mycontent);
                        var data = JObject.Parse(jsonArray[0].ToString());

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
    }
}