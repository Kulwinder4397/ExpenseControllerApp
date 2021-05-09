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
    [Activity(Label = "Expense Report")]
    public class ExpenseReportActivity : AppCompatActivity
    {
        Button b1, b2;
        Spinner spinner;
        string username;
        DataLayer layer;
        EditText et1;
        TextView tv2;
        string[] months;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_expense_report);
            username = Intent.GetStringExtra("UserName");

            layer = new DataLayer();
            spinner = FindViewById<Spinner>(Resource.Id.spinner);
            b1 = FindViewById<Button>(Resource.Id.b1);
            b2 = FindViewById<Button>(Resource.Id.b2);
            et1 = FindViewById<EditText>(Resource.Id.text1);
            tv2 = FindViewById<TextView>(Resource.Id.tv2);

            b1.Click += B1_Click;
            b2.Click += B2_Click;

            months = new string[] { "Choose Any Month", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            ArrayAdapter<string> adapter= new ArrayAdapter<string>(this, Resource.Layout.list_row_category, Resource.Id.text1, months);
            spinner.Adapter = adapter;
        }

        private void B2_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void B1_Click(object sender, EventArgs e)
        {
            string yearvalue = et1.Text.Trim();
            string output;
            if (yearvalue.Length != 4)
            {
                output = "Please Enter Year Value";
            }
            else
            {
                try
                {
                    float total = 0;
                    int year = int.Parse(yearvalue);
                    if (spinner.SelectedItemPosition == 0)
                    {
                        total = layer.GetUserExpenseTotal(username, year);
                        output = "Expense in " + year + " Year: $" + total;
                    }
                    else
                    {
                        int month = spinner.SelectedItemPosition;
                        total = layer.GetUserExpenseTotal(username, year, month);
                        output = "Expense in " + months[month] + " " + year + ": $" + total;
                    }
                }
                catch (Exception)
                {
                    output = "Year is not Valid";
                }
            }

            tv2.Text = output;
        }
    }
}