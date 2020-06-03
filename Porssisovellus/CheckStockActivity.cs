using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Porssisovellus
{
    [Activity(Label = "CheckStockActivity")]
    public class CheckStockActivity : Activity
    {
        CheckedTextView Apple;
        CheckedTextView Microsoft;
        CheckedTextView Google;
        string appleStatus;
        string microsoftStatus;
        string GoogleStatus;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.checkstock_activity);

            Apple = FindViewById<CheckedTextView>(Resource.Id.apple);
            Microsoft = FindViewById<CheckedTextView>(Resource.Id.microsoft);
            Google = FindViewById<CheckedTextView>(Resource.Id.google);


            var btnGoBack = FindViewById<Button>(Resource.Id.gobackbutton);

            //jos tarvii tuoda lisää:
            appleStatus = Intent.GetStringExtra("Apple");
            microsoftStatus = Intent.GetStringExtra("Microsoft");
            GoogleStatus = Intent.GetStringExtra("Google");

            if (appleStatus == "true") 
            {
                Apple.Checked = true;
            }
            else 
            {
                Apple.Checked = false;
            }

            if (microsoftStatus == "true")
            {
                Microsoft.Checked = true;
            }
            else
            {
                Microsoft.Checked = false;
            }

            if (GoogleStatus == "true")
            {
                Google.Checked = true;
            }
            else
            {
                Google.Checked = false;
            }

            Apple.Click += (o, e) =>
            {
                if (Apple.Checked == true) 
                {
                   Apple.Checked = false;
                   appleStatus = "false";
                }
                else 
                {
                    Apple.Checked = true;
                    appleStatus = "true";
                }
            };

            Microsoft.Click += (o, e) =>
            {
                if (Microsoft.Checked == true)
                {
                    Microsoft.Checked = false;
                    microsoftStatus = "false";
                }
                else
                {
                    Microsoft.Checked = true;
                    microsoftStatus = "true";

                }
            };

            Google.Click += (o, e) =>
            {
                if (Google.Checked == true)
                {
                    Google.Checked = false;
                    GoogleStatus = "false";
                }
                else
                {
                    Google.Checked = true;
                    GoogleStatus = "true";
                }
            };

            //Takaisin nappi
            btnGoBack.Click += delegate
            {
                this.Finish();
            };

            //takaisin gobacknappi
            FindViewById<Button>(Resource.Id.gobackbutton).Click += (o, e) => {
                Intent nextActivity = new Intent(this, typeof(MainActivity));
                
                nextActivity.PutExtra("Apple", appleStatus);
                nextActivity.PutExtra("Microsoft", microsoftStatus);
                nextActivity.PutExtra("Google", GoogleStatus);
                StartActivity(nextActivity);
            };
        }
    }

    
}