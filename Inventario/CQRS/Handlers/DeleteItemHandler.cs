using Inventario.CQRS.Commands;
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
    public class DeleteItemHandler : IRequestHandler<DeleteItemCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteItemHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Unit> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the item to be deleted
            var item = _unitOfWork.ItemRepository.Get(request.Id);

            if (item == null)
            {
                throw new Exception($"Item with Id {request.Id} not found.");
            }

            // Delete the item from the database
            _unitOfWork.ItemRepository.Delete(item);
             _unitOfWork.Save();

            return Task.FromResult(Unit.Value);
        }
    }

}