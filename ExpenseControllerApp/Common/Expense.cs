using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpenseControllerApp.Common
{
    public class Expense
    {
        [PrimaryKey, AutoIncrement]
        public int EID { get; set; }

        public string UserName { get; set; }

        public string CategoryName { get; set; }

        public float Amount { get; set; }

        public long ExpenseDate { get; set;  }

    }
}