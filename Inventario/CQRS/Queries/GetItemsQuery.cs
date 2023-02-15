using Inventario.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventario.CQRS.Queries
{
    public class GetItemListQuery : IRequest<List<Item>>
    {
        public List<Item> Items { get; set; }
    }

}