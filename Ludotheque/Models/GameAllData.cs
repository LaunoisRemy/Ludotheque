using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ludotheque.Models
{
    public class GameAllData : Game
    {
        public IQueryable<Category> ThemeCategoryList { get; set; }
        public IQueryable<Category> MaterialSupportCategoryList { get; set; }
        public IQueryable<Category> MechanismCategoryList { get; set; }
    }
}
