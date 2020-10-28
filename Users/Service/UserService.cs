using Domain.Entities;
using Domain.Interfaces.Repos;
using Service.Interfaces;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;

        public UserService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public Users Add(Users user) => _userRepo.Add(user);

        public bool CheckUser(string email) => _userRepo.CheckUser(email);

        public LoginModel Validate(string email, string password)
        {
            var pwd = password.GetCadastroHashCode();
            var data = _userRepo.Validate(email, pwd);
            if (data == null) return null;

            List<PhoneUserModel> phones = null;
            if (data.Phones != null)
            {
                phones = new List<PhoneUserModel>();
                data.Phones.ForEach(q =>
                {
                    phones.Add(new PhoneUserModel() { Ddd = q.Ddd, Number = q.Number });
                });
            }

            return new LoginModel(data.Id, data.Name, data.Email, data.Created
                                , data.Modified, data.Last_login, phones);

        }

        public Users GetbyId(Guid Id) => _userRepo.Get(Id);

        public IEnumerable<Users> GetAll() => _userRepo.GetAll();
    }
}
