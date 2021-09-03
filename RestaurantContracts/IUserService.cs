using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantContracts
{
    public interface IUserService
    {
        public bool IsAdmin { get; set; }
    }
}
