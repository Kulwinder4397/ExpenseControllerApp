using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using ExpenseControllerApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpenseControllerApp
{
    [Activity(Label = "Add Expense Category")]
    public class AddCategoryActivity : AppCompatActivity
    {
        EditText et1, et2;
        Button b1, b2;
        DataLayer layer;
        string username;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_add_category);
            username = Intent.GetStringExtra("UserName");
            layer = new DataLayer();
            et1 = FindViewById<EditText>(Resource.Id.text1);
            et2 = FindViewById<EditText>(Resource.Id.text2);

            b1 = FindViewById<Button>(Resource.Id.b1);
            b2 = FindViewById<Button>(Resource.Id.b2);
            b1.Click += B1_Click;
            b2.Click += B2_Click;
        }

        private void B2_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void B1_Click(object sender, EventArgs e)
        {
            string categoryid = et1.Text.Trim();
            string categoryname = et2.Text.Trim();
            string message = "";
           if (categoryid.Length == 0 || categoryname.Length == 0)
            {
                message = "Please Enter Some Value in Boxes";
            }
            else
            {
                Category category = new Category();
                category.CategoryID = categoryid;
                category.CategoryName = categoryname;
                category.UserName = username;
                if (layer.CreateCategory(category))
                {
                    message = "Expense Category Details are Saved!!!";
                    et1.Text = "";
                    et2.Text = "";
                }
                else
                {
                    message = layer.Error;
                }
            }
            Toast.MakeText(this, message, ToastLength.Long).Show();
        }
    }
}