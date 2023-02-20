using Inventario.Models;
using Inventario.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Inventario.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;

        private ItemRepository _itemRepository;
        private ItemModelRepository _itemModelRepository;
        private ItemCategoryRepository _itemCategoryRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public ItemRepository ItemRepository
        {
            get
            {
                if (_itemRepository == null)
                    _itemRepository = new ItemRepository(_context);
                return _itemRepository;
            }
        }

        public ItemModelRepository ItemModelRepository
        {
            get
            {
                if (_itemModelRepository == null)
                {
                    _itemModelRepository = new ItemModelRepository(_context);
                }
                return _itemModelRepository;
            }
        }

        public ItemCategoryRepository ItemCategoryRepository
        {
            get
            {
                if (_itemCategoryRepository == null)
                {
                    _itemCategoryRepository = new ItemCategoryRepository(_context);
                }
                return _itemCategoryRepository;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }

}