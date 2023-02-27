using Inventario.Models;
using Inventario.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ItemRepository ItemRepository { get; }
        ItemModelRepository ItemModelRepository { get; }
        ItemCategoryRepository ItemCategoryRepository { get; }
        void Save();
        void Reset();
        void Reset(ApplicationDbContext _context);
    }



}
