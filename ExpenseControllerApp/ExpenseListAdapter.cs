using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ExpenseControllerApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpenseControllerApp
{
   public class ExpenseListAdapter : BaseAdapter<Expense>
    {
        private readonly Activity context;
        private readonly List<Expense> expenses;

        public ExpenseListAdapter(Activity context, List<Expense> expenses)
        {
            this.expenses = expenses;
            this.context = context;
        }

        public override int Count
        {
            get { return expenses.Count; }

        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Expense this[int position]
        {
            get { return expenses[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(context).Inflate(Resource.Layout.list_row_expense, null, false);
            }

            TextView txt1 = row.FindViewById<TextView>(Resource.Id.text1);
            TextView txt2 = row.FindViewById<TextView>(Resource.Id.text2);
            TextView txt3 = row.FindViewById<TextView>(Resource.Id.text3);

            txt1.Text = expenses[position].CategoryName;
            txt2.Text = "Amount: $" + expenses[position].Amount;
            TimeSpan time = TimeSpan.FromMilliseconds(expenses[position].ExpenseDate);
            DateTime date = new DateTime(1970, 1, 1) + time;
            txt3.Text = "Expense Date: " + date.ToLongDateString();

            return row;
        }
    }
}