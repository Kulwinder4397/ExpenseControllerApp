using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ExpenseControllerApp.Common
{
    public class DataLayer
    {
        private SQLiteConnection connection;

        public string Error { get; set; }

        public DataLayer()
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            connection = new SQLiteConnection(Path.Combine(path, "expense_db.db"));
            CreateTable();

        }

        public void CreateTable()
        {
            try
            {
                connection.CreateTable<Users>();
                connection.CreateTable<Category>();
                connection.CreateTable<Expense>();
            }
            catch (Exception ex)
            {

            }
        }

        public bool CheckUser(string username, string password)
        {
            List<Users> users = connection.Query<Users>("Select * from Users");
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].UserName.Equals(username) && users[i].Password.Equals(password))
                {
                    return true;
                }

            }
            return false;
        }

        public bool CreateUser(Users user)
        {
            try
            {
                connection.Insert(user);
                return true;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return false;
            }
        }

        public bool CreateCategory(Category category)
        {
            try
            {
                connection.Insert(category);
                return true;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return false;
            }
        }

        public List<Category> GetAllCategory()
        {
            List<Category> categories = connection.Query<Category>("Select * from Category");
            return categories;
        }

        public List<Category> GetUserCategory(string username)
        {
            List<Category> categories = new List<Category>();
            List<Category> datas = GetAllCategory();
            foreach (Category category in datas)
            {
                if (category.UserName.Equals(username))
                {
                    categories.Add(category);
                }
            }
            return categories;
        }

        public bool DeleteCategory(Category category)
        {
            try
            {
                connection.Delete(category);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool CreateExpense(Expense expense)
        {
            try
            {
                connection.Insert(expense);
                return true;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return false;
            }
        }

        public List<Expense> GetAllExpenses()
        {
            List<Expense> expenses = connection.Query<Expense>("Select * from Expense ORDER BY ExpenseDate Desc");
            return expenses;
        }

        public List<Expense> GetUserExpenses(string username)
        {
            List<Expense> expenses = new List<Expense>();
            List<Expense> datas = GetAllExpenses();
            foreach (Expense expense in datas)
            {
                if (expense.UserName.Equals(username))
                {
                    expenses.Add(expense);
                }
            }
            return expenses;
        }

        public bool CheckExpenseByCategoryName(string categoryname)
        {
            List<Expense> expenses = GetAllExpenses();
            if(expenses!= null && expenses.Count > 0)
            {
                foreach (Expense expense in expenses)
                {
                    if(expense.CategoryName.Equals(categoryname))
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        public float GetUserExpenseTotal(string username,int year,int month)
        {
            float total = 0;
            List<Expense> expenses = GetUserExpenses(username);
            if (expenses != null && expenses.Count > 0)
            {
                foreach (Expense expense in expenses)
                {
                    TimeSpan time = TimeSpan.FromMilliseconds(expense.ExpenseDate);
                    DateTime date = new DateTime(1970, 1, 1) + time;
                    if (date.Year == year && date.Month == month)
                    {
                        total += expense.Amount;
                    }
                }
            }
            return total;
        }
        public float GetUserExpenseTotal(string username, int year)
        {
            float total = 0;
            List<Expense> expenses = GetUserExpenses(username);
            if (expenses != null && expenses.Count > 0)
            {
                foreach (Expense expense in expenses)
                {
                    TimeSpan time = TimeSpan.FromMilliseconds(expense.ExpenseDate);
                    DateTime date = new DateTime(1970, 1, 1) + time;
                    if (date.Year == year )
                    {
                        total += expense.Amount;
                    }
                }
            }
            return total;
        }
    }
}