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
    public class CategoryListAdapter : BaseAdapter<Category>
    {
        private readonly Activity context;
        private readonly List<Category> categories;

        public CategoryListAdapter(Activity context, List<Category> categories)
        {
            this.categories = categories;
            this.context = context;
        }

        public override int Count
        {
            get { return categories.Count; }

        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Category this[int position]
        {
            get { return categories[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(context).Inflate(Resource.Layout.list_row_category, null, false);
            }

            TextView txt1 = row.FindViewById<TextView>(Resource.Id.text1);

            txt1.Text = categories[position].CategoryName + " (" + categories[position].CategoryID + ")";

            return row;
        }
    }
}