using Inventario.Models;
using System.Collections.Generic;

namespace Inventario.Repository
{
    public interface IItemModelRepository
    {
        IEnumerable<ItemModel> GetAll();
        ItemModel Get(int id);
        void Add(ItemModel itemModel);
        void Remove(ItemModel itemModel);
    }

}