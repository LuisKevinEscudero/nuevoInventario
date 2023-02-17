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
    public class GetItemsModelListHandler : IRequestHandler<GetItemsModelQuery, List<ItemModel>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetItemsModelListHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<List<ItemModel>> Handle(GetItemsModelQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_unitOfWork.ItemModelRepository.GetAll().ToList());
        }
    }
}