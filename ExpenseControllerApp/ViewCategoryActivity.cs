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
    [Activity(Label = "View Expense Category")]
    public class ViewCategoryActivity : AppCompatActivity
    {
        Button b1;
        ListView list1;
        DataLayer layer;
        CategoryListAdapter adapter;
        string username;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_view_category);
            username = Intent.GetStringExtra("UserName");
            layer = new DataLayer();

            b1 = FindViewById<Button>(Resource.Id.b1);
            list1 = FindViewById<ListView>(Resource.Id.list1);

            b1.Click += B1_Click; ;

            adapter = new CategoryListAdapter(this, layer.GetUserCategory(username));
            list1.Adapter = adapter;

        }

        private void B1_Click(object sender, EventArgs e)
        {
            Finish();
        }
    }
}