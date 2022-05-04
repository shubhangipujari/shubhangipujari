﻿using AdminService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminService.Repository
{
    public interface IUser
    {
        IEnumerable<UserDetail> GetUsers();
        UserDetail GetUserById(int userId);
        void CreateUser(UserDetail user);
        void DeleteUser(int userId);
        void UpdateUser(UserDetail user);

        Tokan Login(string name, string password);
        void Save();


    }
}
