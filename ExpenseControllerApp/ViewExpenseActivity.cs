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
    [Activity(Label = "View Expense")]
    public class ViewExpenseActivity : AppCompatActivity
    {
        string username;
        Button b1;
        ListView list1;
        DataLayer layer;
        ExpenseListAdapter adapter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_view_expense);
            username = Intent.GetStringExtra("UserName"); layer = new DataLayer();

            b1 = FindViewById<Button>(Resource.Id.b1);
            list1 = FindViewById<ListView>(Resource.Id.list1);

            b1.Click += B1_Click; ;

            adapter = new ExpenseListAdapter(this, layer.GetUserExpenses(username));
            list1.Adapter = adapter;

        }

        private void B1_Click(object sender, EventArgs e)
        {
            Finish();
        }
    }
}