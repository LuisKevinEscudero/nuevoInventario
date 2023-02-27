using Inventario.CQRS.Queries;
using Inventario.Models;
using Inventario.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Inventario.CQRS.Handlers
{
    public class GetItemListHandler : IRequestHandler<GetItemListQuery, List<Item>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetItemListHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<List<Item>> Handle(GetItemListQuery request, CancellationToken cancellationToken)
        {
            var items = _unitOfWork.ItemRepository.GetAll().ToList();
            return Task.FromResult(items);
        }

    }
}