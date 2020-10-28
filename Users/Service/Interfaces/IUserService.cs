using Domain.Entities;
using Service.Interfaces.Base;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interfaces
{
    public interface IUserService : IServiceBase<Users>
    {
        bool CheckUser(string email);
        LoginModel Validate(string email, string password);
    }
}
