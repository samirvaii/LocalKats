using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace FriendBearkats
{
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Dob { get; set; }
        public string Address { get; set; }
        public string Major { get; set; }
        public string Gender { get; set; }
        public string SexualPreference { get; set; }
        public string Bio { get; set; }
        public string Hobbies { get; set; }

        public string getEmail ()
        {
            return Email;
        }
    }
}
