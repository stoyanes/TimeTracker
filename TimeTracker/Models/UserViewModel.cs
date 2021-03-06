﻿using System;
using System.Linq;
using TimeTracker.DAL;

namespace TimeTracker.Models
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string  UserName { get; set; }
        public string  FirstName { get; set; }
        public string  LastName { get; set; }
        public string  Position { get; set; }
        public string  Email { get; set; }

        public UserViewModel()
        {
            UserId = 0;
            UserName = null;
            FirstName = null;
            LastName = null;
            Position = null;
            Email = null;
        }

        public UserViewModel(int id, string un, string fn, string ln, string pos, string e)
        {
            UserId = id;
            UserName = un;
            FirstName = fn;
            LastName = ln;
            Position = pos;
            Email = e;
        }

        public UserViewModel(User usr)
        {
            UserId = usr.UserId;
            UserName = usr.UserName;
            FirstName = usr.FirstName;
            LastName = usr.LastName;
            Position = usr.Position;
            Email = usr.Email;
        }
    }
}