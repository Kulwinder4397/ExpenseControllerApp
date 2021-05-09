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
    [Activity(Label = "Add Expense Entry")]
    public class AddExpenseActivity : AppCompatActivity
    {
        Button b1, b2;
        EditText et1;
        DatePicker date1;
        Spinner spinner;
        string username;
        DataLayer layer;
        CategoryListAdapter adapter;
        int year, month, day;
        List<Category> categories;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_add_expense);
            username = Intent.GetStringExtra("UserName");
            layer = new DataLayer();

            et1 = FindViewById<EditText>(Resource.Id.text1);
            date1 = FindViewById<DatePicker>(Resource.Id.date1);
            spinner = FindViewById<Spinner>(Resource.Id.spinner);
            b1 = FindViewById<Button>(Resource.Id.b1);
            b2 = FindViewById<Button>(Resource.Id.b2);

            categories = layer.GetUserCategory(username);
            adapter = new CategoryListAdapter(this, categories);
            spinner.Adapter = adapter;

            b1.Click += B1_Click;
            b2.Click += B2_Click;
            date1.DateChanged += Date1_DateChanged;
        }

        private void Date1_DateChanged(object sender, DatePicker.DateChangedEventArgs e)
        {
            year = e.Year;
            month = e.MonthOfYear;
            day = e.DayOfMonth;
        }

        private void B2_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void B1_Click(object sender, EventArgs e)
        {
            string amount_string = et1.Text.Trim();
            if (year == 0 || amount_string.Length == 0 || categories == null || categories.Count() == 0)
            {
                Toast.MakeText(this, "Please Fill Full Form", ToastLength.Long).Show();
            }
            else
            {
                try
                {
                    DateTime expensedate = new DateTime(year, month, day);
                    DateTime begin = new DateTime(1970, 1, 1);
                    Category category = categories[spinner.SelectedItemPosition];
                    Expense expense = new Expense
                    {
                        UserName = username,
                        CategoryName = category.CategoryName,
                        Amount = float.Parse(amount_string),
                        ExpenseDate = (long)(expensedate - begin).TotalMilliseconds
                    };
                    if (layer.CreateExpense(expense))
                    {
                        Toast.MakeText(this, "Expense Entry is Saved", ToastLength.Long).Show();
                    }
                    else
                    {
                        Toast.MakeText(this, "Expense Entry is not Saved", ToastLength.Long).Show();
                    }
                }
                catch(Exception )
                {
                    Toast.MakeText(this, "Please Enter Valid Number in Amount Box", ToastLength.Long).Show();
                }
                
            }
        }
    }
}