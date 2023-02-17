using Inventario.CQRS.Commands;
using Inventario.DTOs;
using Inventario.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Web;

namespace Inventario.CQRS.Handlers
{
    public class UpdateItemHandler : IRequestHandler<UpdateItemCommand, ItemDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateItemHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ItemDTO> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            return await request.UpdateItemAsync();
        }
    }

}