using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace RestaurantContracts
{
    public interface Imenu
    {
        public DataSet GetCategories();
        public DataSet GetDishesCategory(int CategoryId, bool IsAllergy);
        public void InsertDish(string NameDish, int PriceDish, bool IsAllergyDish, int CategoryId);
        public void deleteDish(string dishId);
        public void EditDish(string NameDish, float PriceDish, bool AllergyDish, string DishId);
    }
}
