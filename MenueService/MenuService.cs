using DalInfraContracts;
using SQLInfraDAL;
using System;
using System.Data;
using System.Data.SqlClient;
using RestaurantContracts;

namespace MenueService
{
    public class MenuService : Imenu
    {
        private IDAL dal { get; set; }

        public MenuService(IDAL dal)
        {
            this.dal = dal;
        }

        public DataSet GetCategories()
        {
            ///todo
            // DAL dal = new DAL();
            //return dal.ExecuteQuery("GET_CATEGORIES");
            // return new DAL().ExecuteQuery("GET_CATEGORIES");

            return dal.ExecuteQuery("GET_CATEGORIES");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CategoryId">מספר קטגוריה</param>
        /// <returns>טבלה עם נתונים של הקטגוריה</returns>
        public DataSet GetDishesCategory(int CategoryId, bool IsAllergy)
        {
            //todo
            // DAL dalr = new DAL();
            var paramCategoryId = dal.CreateParameter("CATEGORY_ID", CategoryId.ToString());
            var paramIsAllergy = dal.CreateParameter("IsAllergy", IsAllergy);
            return dal.ExecuteQuery("GET_DISHES_FOR_CATEGORY", paramCategoryId,paramIsAllergy);
        }
        private string createID()
        {
            Random rnd = new Random();
            return (rnd.Next(1000, 100000000)).ToString();
        }

        public void InsertDish(string NameDish, int PriceDish, bool IsAllergyDish, int CategoryId)
        {
            string dishId = createID();
            var paramDishId = dal.CreateParameter("@DISH_ID", dishId);
            var paramCategoryId = dal.CreateParameter("@CATEGORY_ID", CategoryId);
            var paramDishName = dal.CreateParameter("@NAME", NameDish);
            var paramDishPrice = dal.CreateParameter("@PRICE", PriceDish);
            var paramDishAllergy = dal.CreateParameter("@CONTAINS_ALLERGENS", IsAllergyDish);
            try
            {
                dal.ExecuteNonQuery("ADD_ֹDISH", paramDishId, paramCategoryId, paramDishName, paramDishPrice, paramDishAllergy);

            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) //Violation of primary key
                {
                    InsertDish(NameDish, PriceDish, IsAllergyDish, CategoryId);
                }
                else
                {
                    dishId = "0";
                }
            }
        }
        public void deleteDish(string dishId)
        {
            var paramDishId = dal.CreateParameter("@DISH_ID", dishId);
            dal.ExecuteNonQuery("DELETE_DISH", paramDishId);
        }
        public void EditDish(string NameDish, float PriceDish, bool AllergyDish,string DishId)
        {
            var paramNameDish = dal.CreateParameter("@NameDish", NameDish);
            var paramPriceDish = dal.CreateParameter("@PriceDish", PriceDish);
            var paramAllergyDish = dal.CreateParameter("@AllergyDish", AllergyDish);
            var paramDishId = dal.CreateParameter("@DishId", DishId);
            dal.ExecuteNonQuery("EDIT_DISH", paramNameDish, paramPriceDish, paramAllergyDish, paramDishId);
        }
    }
}

