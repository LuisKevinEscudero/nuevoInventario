

using Inventario.CQRS.Queries;
using Inventario.ViewModels;
using MediatR;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Inventario.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ActionResult> Index()  
        {
            var query = new GetItemListQuery();
            var items = await _mediator.Send(query);

            var viewModel = new ItemViewModel
            {
                items = items
            };

            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}