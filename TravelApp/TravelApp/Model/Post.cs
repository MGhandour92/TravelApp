using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace TravelApp.Model
{
    public class Post : INotifyPropertyChanged
    {
        private int _id;
        private string _experience;
        private string _venueName;
        private string _categoryId;
        private string _categoryName;
        private string _address;
        private double _latitude;
        private double _longtitude;
        private int _distance;
        private int _userId;

        [PrimaryKey, AutoIncrement]
        public int Id { get => _id; set { _id = value; OnPropertyChanged("Id"); } }

        [MaxLength(250)]
        public string Experience { get => _experience; set { _experience = value; OnPropertyChanged("Experience"); } }

        public string VenueName { get => _venueName; set { _venueName = value; OnPropertyChanged("VenueName"); } }

        public string CategoryId { get => _categoryId; set { _categoryId = value; OnPropertyChanged("CategoryId"); } }
        public string CategoryName { get => _categoryName; set { _categoryName = value; OnPropertyChanged("CategoryName"); } }

        public string Address { get => _address; set { _address = value; OnPropertyChanged("Address"); } }

        public double Latitude { get => _latitude; set { _latitude = value; OnPropertyChanged("Latitude"); } }
        public double Longtitude { get => _longtitude; set { _longtitude = value; OnPropertyChanged("Longtitude"); } }

        public int Distance { get => _distance; set { _distance = value; OnPropertyChanged("Distance"); } }

        public int UserId { get => _userId; set { _userId = value; OnPropertyChanged("UserId"); } }

        public event PropertyChangedEventHandler PropertyChanged;

        public static int Insert(Post post)
        {
            //Create SQLite connection
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                //Create Table using the connection
                conn.CreateTable<Post>();

                //insert the model object (record)
                int rowsInserted = conn.Insert(post);

                return rowsInserted;
            }
        }

        public static List<Post> GetPosts()
        {
            //Read Database
            //Create Connection
            //Using statement because it implments IDisposable, and to avoid forgetting to close connection
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                //Create or Replace the table
                //To overcome exceptions when user first open this page before inserting
                conn.CreateTable<Post>();

                //Select Records from the table
                var postList = conn.Table<Post>().Where(p => p.UserId == App.currentUser.Id).ToList();
                return postList;
            }
        }

        public static Dictionary<string, int> PostCategories(List<Post> posts)
        {
            //var categories2 = (from p in postTable
            //                  orderby p.CategoryId
            //                  select p.CategoryName).Distinct().ToList();

            var categories = posts.OrderBy(p => p.CategoryId).
                Select(p => p.CategoryName).Distinct().ToList();

            Dictionary<string, int> categoriesCount = new Dictionary<string, int>();

            foreach (var category in categories)
            {
                var count = posts.Where(p => p.CategoryName == category).ToList().Count;

                categoriesCount.Add(category, count);
            }

            return categoriesCount;
        }
        
        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
