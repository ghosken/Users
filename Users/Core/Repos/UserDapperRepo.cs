using Core.Contexts;
using Domain.Entities;
using Domain.Interfaces.Repos;
using System;
using System.Collections.Generic;
using System.Text;


namespace Core.Repos
{
    public class UsersDapperRepo : IUserRepo
    {
        private readonly PrincipalContext _context;

        public UsersDapperRepo(PrincipalContext context)
        {
            _context = context;
        }
        public IEnumerable<Users> GetAll()
        {
            var cn = _context.Database.GetDbConnection();

            string sql = @"
                SELECT u.Id, u.Name, u.Email, u.Password, u.Created, u.Modified, u.Last_login,
                p.Id, p.UsersId, p.Ddd, p.Number 
                FROM Userss u
                INNER JOIN Phones p
                on u.Id = p.UsersId
                order by u.Id";

            var dapperDictionary = new Dictionary<Guid, Users>();

            var resultDapper = cn.Query<Users, Phone, Users>(sql,
                 (Users, phone) =>
                 {
                     if (!dapperDictionary.TryGetValue(Users.Id, out Users UsersResult))
                     {
                         UsersResult = Users;
                         if (UsersResult.Phones == null && phone != null)
                             UsersResult.Phones = new List<Phone>();
                         dapperDictionary.Add(Users.Id, UsersResult);
                     }
                     if (phone != null)
                         UsersResult.Phones.Add(phone);

                     return UsersResult;
                 },
                 splitOn: "Id")
                .Distinct()
                .ToList();

            return resultDapper;

        }

        public Users Add(Users entity)
        {
            _context.Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public Users Get(Guid id)
        {


            var cn = _context.Database.GetDbConnection();

            string sql = @"
                SELECT u.Id, u.Name, u.Email, u.Password, u.Created, u.Modified, u.Last_login,
                p.Id, p.UsersId, p.Ddd, p.Number 
                FROM Userss u
                INNER JOIN Phones p
                on u.Id = p.UsersId
                WHERE u.Id = @id
                order by u.Id";

            var dapperDictionary = new Dictionary<Guid, Users>();

            var resultDapper = cn.Query<Users, Phone, Users>(sql,
                 (Users, phone) =>
                 {
                     if (!dapperDictionary.TryGetValue(Users.Id, out Users UsersResult))
                     {
                         UsersResult = Users;
                         if (UsersResult.Phones == null && phone != null)
                             UsersResult.Phones = new List<Phone>();
                         dapperDictionary.Add(Users.Id, UsersResult);
                     }
                     if (phone != null)
                         UsersResult.Phones.Add(phone);

                     return UsersResult;
                 },
                  param: new { id },
                 splitOn: "Id")
                .Distinct()
                .SingleOrDefault();

            return resultDapper;

        }

        public void LastLogin(Guid UsersId)
        {
            var Users = Get(UsersId);
            Users.RegistrarAcesso();
            _context.User.Update(Users);
            _context.SaveChanges();
        }

        public Users Validate(string email, string password)
        {
            var cn = _context.Database.GetDbConnection();

            string sql = @"
                SELECT u.Id, u.Name, u.Email, u.Password, u.Created, u.Modified, u.Last_login,
                p.Id, p.UsersId, p.Ddd, p.Number 
                FROM Userss u
                INNER JOIN Phones p
                on u.Id = p.UsersId
                WHERE u.Email = @email and u.Password = @password
                order by u.Id";

            var dapperDictionary = new Dictionary<Guid, Users>();

            var resultDapper = cn.Query<Users, Phone, Users>(sql,
                 (Users, phone) =>
                 {
                     if (!dapperDictionary.TryGetValue(Users.Id, out Users UsersResult))
                     {
                         UsersResult = Users;
                         if (UsersResult.Phones == null && phone != null)
                             UsersResult.Phones = new List<Phone>();
                         dapperDictionary.Add(Users.Id, UsersResult);
                     }
                     if (phone != null)
                         UsersResult.Phones.Add(phone);

                     return UsersResult;
                 },
                  param: new { email, password },
                 splitOn: "Id")
                .Distinct()
                .SingleOrDefault();


            if (resultDapper != null) //usuario logado !
            {
                resultDapper.RegistrarAcesso();
                _context.User.Update(resultDapper);
                _context.SaveChanges();
            }

            return resultDapper;
        }

        public bool CheckUsers(string email)
        {
            var cn = _context.Database.GetDbConnection();

            string sql = "SELECT * FROM Userss u WHERE u.Email = @email";
            var resultDapper = cn.QueryFirstOrDefault<Users>(sql, new { Email = email });
            return (resultDapper != null);

        }

    }
}
