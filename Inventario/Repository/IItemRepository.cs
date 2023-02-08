using Inventario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Repository
{
    public interface IItemRepository
    {
        void Add(Item item);
        void Delete(Item item);
        Item Get(int id);
        List<Item> GetAll();
    }
}
