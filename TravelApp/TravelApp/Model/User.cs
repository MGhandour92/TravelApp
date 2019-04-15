using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TravelApp.Model
{
    public class User : INotifyPropertyChanged
    {
        private int _id;
        private string _email;
        private string _password;

        [PrimaryKey, AutoIncrement]
        public int Id { get => _id; set { _id = value; OnPropertyChanged("Id"); } }

        public string Email { get => _email; set { _email = value; OnPropertyChanged("Email"); } }

        public string Password { get => _password; set { _password = value; OnPropertyChanged("Password"); } }

        public event PropertyChangedEventHandler PropertyChanged;

        public static int Insert(User user)
        {
            //Create SQLite connection
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                //Create Table using the connection
                conn.CreateTable<User>();

                //insert the model object (record)
                int rowsInserted = conn.Insert(user);

                return rowsInserted;
            }
        }

        public static User GetUser(string userEmail)
        {
            //Read Database
            //Create Connection
            //Using statement because it implments IDisposable, and to avoid forgetting to close connection
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                //Create or Replace the table
                //To overcome exceptions when user first open this page before inserting
                conn.CreateTable<User>();

                //Select Records from the table
                var user = conn.Table<User>().Where(u => u.Email == userEmail).FirstOrDefault();
                return user;
            }
        }

        public static bool Login(string userName, string password)
        {
            bool emptyEmail = string.IsNullOrEmpty(userName);
            bool emptyPassword = string.IsNullOrEmpty(password);

            if (!emptyEmail || !emptyPassword)
            {
                var user = GetUser(userName);

                if (user != null)
                {
                    App.currentUser = user;
                    return user.Password == password ? true : false;
                }
                else
                    return false;
            }
            else
                return false;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
