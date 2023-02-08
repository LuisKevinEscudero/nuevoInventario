using Inventario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventario.Repository
{
    public class ItemModelRepository : IItemModelRepository
    {
        private readonly ApplicationDbContext _context;

        public ItemModelRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(ItemModel itemModel)
        {
            _context.itemModels.Add(itemModel);
        }

        public IEnumerable<ItemModel> GetAll()
        {
            return _context.itemModels.ToList();
        }

        public ItemModel Get(int id)
        {
            return _context.itemModels.FirstOrDefault(i => i.Id == id);
        }

        public void Remove(ItemModel itemModel)
        {
            _context.itemModels.Remove(itemModel);
        }
    }

}