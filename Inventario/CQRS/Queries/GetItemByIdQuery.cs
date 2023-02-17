using Inventario.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventario.CQRS.Queries
{
    public class GetItemByIdQuery : IRequest<Item>
    {
        public int Id { get; set; } 

        public GetItemByIdQuery(int id)
        {
            Id = id;
        }
    }

}