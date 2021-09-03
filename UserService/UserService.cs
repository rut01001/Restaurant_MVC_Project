using RestaurantContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserServiceImpl
{
   public class UserService : IUserService
    {
        public bool IsAdmin { get; set; }
    }
}
