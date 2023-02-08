using Inventario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Repository
{
    public interface IItemCategoryRepository
    {
        IEnumerable<ItemCategory> GetAll();
        ItemCategory Get(int id);
        void Add(ItemCategory itemCategory);
        void Delete(ItemCategory itemCategory);
        void Save();
    }

}
