using Inventario.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Inventario.Repository
{
    public class ItemCategoryRepository : IItemCategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public ItemCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ItemCategory> GetAll()
        {
            return _context.ItemCategories.ToList();
        }

        public ItemCategory Get(int id)
        {
            return _context.ItemCategories.Find(id);
        }

        public void Add(ItemCategory itemCategory)
        {
            _context.ItemCategories.Add(itemCategory);
        }

        public void Delete(ItemCategory itemCategory)
        {
            _context.ItemCategories.Remove(itemCategory);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }

}