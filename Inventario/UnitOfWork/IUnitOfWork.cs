using Inventario.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.UnitOfWork
{
    public interface IUnitOfWork
    {
        ItemRepository ItemRepository { get; }
        ItemModelRepository ItemModelRepository { get; }
        ItemCategoryRepository ItemCategoryRepository { get; }
        void Save();
        Task<int> SaveChangesAsync();
    }



}
