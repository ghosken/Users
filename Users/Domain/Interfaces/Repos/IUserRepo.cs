using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces.Repos
{
    public interface IUserRepo : IRepoBase<Users>
    {
        bool checarUsuario(string email);
        Users Validade(string email, string password);
    }
}
