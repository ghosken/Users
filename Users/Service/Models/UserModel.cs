using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Models
{
    public class UserModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual List<PhoneUserModel> Phones { get; set; }
    }
}
