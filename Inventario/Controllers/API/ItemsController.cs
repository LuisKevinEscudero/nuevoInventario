using Inventario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Inventario.Controllers.API
{
    public class ItemsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public ItemsController()
        {
            _context = new ApplicationDbContext();
        }
        
        
    }
}
