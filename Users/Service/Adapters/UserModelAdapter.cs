using Domain.Entities;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Adapters
{
    public class UserModelAdapter
    {
        public static Users Adapter(this UserModel data)
        {
            if (data == null) return null;
            List<Phone> phones = null;
            if (data.Phones != null)
            {
                phones = new List<Phone>();
                phones.AddRange(data.Phones.Select(q => new Phone(q.Ddd, q.Number)));
            }

            return new Users(
                Guid.Empty,
                data.Name,
                data.Email,
                data.Password.GetCadastroHashCode(),
                phones
                );
        }

        public static UserModel Adapter(this Users data)
        {
            if (data == null) return null;
            List<PhoneUserModel> phones = null;
            if (data.Phones != null)
            {
                phones = new List<PhoneUserModel>();
                phones.AddRange(
                    data.Phones.Select(q =>
                    new PhoneUserModel
                    {
                        Ddd = q.Ddd,
                        Number = q.Number
                    })
               );
            }

            UserModel user = new UserModel()
            {
                Email = data.Email,
                Name = data.Name,
                Phones = phones
            };

            return user;
        }
    }
}
