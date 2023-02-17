using Inventario.CQRS.Queries;
using Inventario.Models;
using Inventario.UnitOfWork;
using MediatR;
using Microsoft.Owin.Security.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Inventario.CQRS.Handlers
{
    public class GetItemByIdHandler : IRequestHandler<GetItemByIdQuery, Item>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetItemByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<Item> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
        {
            var item = _unitOfWork.ItemRepository.Get(request.Id);
            return await Task.FromResult(item);
        }

    }
}