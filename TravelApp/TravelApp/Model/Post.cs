using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TravelApp.Model
{
    public class Post
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(250)]
        public string Experience { get; set; }
    }
}
