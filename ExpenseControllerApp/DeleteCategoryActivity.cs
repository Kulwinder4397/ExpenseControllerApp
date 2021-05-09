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
    [Activity(Label = "Delete Expense Category")]
    public class DeleteCategoryActivity : AppCompatActivity
    {
        
        Button b1, b2;
        Spinner spinner;
        string username;
        DataLayer layer;
        CategoryListAdapter adapter;
        List<Category> categories;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_delete_category);
            username = Intent.GetStringExtra("UserName");
            
            layer = new DataLayer();

            
            
            spinner = FindViewById<Spinner>(Resource.Id.spinner);
            b1 = FindViewById<Button>(Resource.Id.b1);
            b2 = FindViewById<Button>(Resource.Id.b2);

            categories = layer.GetUserCategory(username);
            adapter = new CategoryListAdapter(this, categories);
            spinner.Adapter = adapter;

            b1.Click += B1_Click;
            b2.Click += B2_Click;
        }

        private void B2_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void B1_Click(object sender, EventArgs e)
        {
            string message = "";
            if(categories!=null && categories.Count() > 0)
            {
                Category category = categories[spinner.SelectedItemPosition];
                if(layer.CheckExpenseByCategoryName(category.CategoryName))
                {
                    message = "You can not delete this Expense category";
                }
                else
                {
                    if (layer.DeleteCategory(category))
                    {
                        message = "Expense Category Details is Removed";
                        categories.RemoveAt(spinner.SelectedItemPosition);
                        adapter.NotifyDataSetChanged();
                    }
                    else
                    {
                        message = "Expense Category Details in not Removed";
                    }
                }
                
            }
            else
            {
                message = "There is Expense Category Available For Delete.";
            }
            Toast.MakeText(this, message, ToastLength.Long).Show();
        }
    }
}