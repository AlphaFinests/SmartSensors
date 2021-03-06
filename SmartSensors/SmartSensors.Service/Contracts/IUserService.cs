﻿using SmartSensors.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSensors.Service.Contracts
{
    public interface IUserService
    {
        List<UserViewModel> GetAllUsers();


        void AddUser(AddUserViewModel model);

        UserViewModel ServiceEditUser(string username);

        Task ServiceEditUser(UserViewModel userViewModel);
    }
}
