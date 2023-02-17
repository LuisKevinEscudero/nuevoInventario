using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventario.CQRS.Commands
{
    public class DeleteItemCommand : IRequest
    {
        public int Id { get; set; }

        public DeleteItemCommand(int id)
        {
            Id = id;
        }
    }
}