using Inventario.Models;
using Inventario.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;
using System.IO;
using MediatR;
using Inventario.CQRS.Queries;
using System.Threading.Tasks;
using Inventario.CQRS.Commands;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Net;

namespace Inventario.Controllers
{
    public class ItemsController : Controller
    {

        private readonly IMediator _mediator;
        private readonly HttpClient _client;
        public ItemsController(IMediator mediator)
        {
            _mediator = mediator;
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44377") // URL de la API
            };
        }

        public ActionResult Index()
        {
            if (User.IsInRole(RoleName.Admin))
                return View("List");
            else
                return View("ReadOnlyList");
        }

        public async Task<ActionResult> Detail(int id)
        {
            if (!int.TryParse(id.ToString(), out int parsedId) || parsedId <= 0)
            {
                throw new ArgumentException("[DETAIL] Invalid id parameter", nameof(id));
            }

            try
            {
                if (_mediator == null)
                {
                    throw new Exception("[DETAIL] _mediator is null");
                }

                var item = await _mediator.Send(new GetItemByIdQuery(id));

                if (item == null || item.Id != id)
                {
                    throw new Exception($"[DETAIL] Item not found with id: {id}");
                }

                return View(item);
            }
            catch (ArgumentException ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, $"[DETAIL] An error occurred while processing the request: {ex.Message}");
            }
        }


        [Authorize(Roles = RoleName.Admin)]
        public async Task<ActionResult> New()
        {
            try
            {
                if (_mediator == null)
                {
                    throw new Exception("[NEW] _mediator is null");
                }

                var itemModels = await _mediator.Send(new GetItemsModelQuery());

                if (itemModels == null || !itemModels.Any())
                {
                    throw new Exception("[NEW] No item models found");
                }

                var itemCategories = await _mediator.Send(new GetItemsCategoryQuery());

                if (itemCategories == null || !itemCategories.Any())
                {
                    throw new Exception("[NEW] No item categories found");
                }

                var viewModel = new ItemFormViewModel
                {
                    Item = new Item(),
                    ItemModels = itemModels,
                    ItemCategories = itemCategories
                };

                return View("ItemForm", viewModel);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, $"[NEW] An error occurred while processing the request: {ex.Message}");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Admin)]
        public async Task<ActionResult> Save(Item item)
        {
            if (!ModelState.IsValid)
            {
                string path = @"C:\Users\luiskevin.escudero\Desktop\error-log.txt";
                using (StreamWriter writer = new StreamWriter(path))
                {
                    foreach (var state in ModelState.Values)
                    {
                        foreach (var error in state.Errors)
                        {
                            writer.WriteLine(error.ErrorMessage);
                        }
                    }
                }

                var viewModel = new ItemFormViewModel
                {
                    Item = item,
                    ItemModels = await _mediator.Send(new GetItemsModelQuery()),
                    ItemCategories = await _mediator.Send(new GetItemsCategoryQuery())
                };

                return View("ItemForm", viewModel);
            }

            if (item.Id == 0)
            {
                var listModel = await _mediator.Send(new GetItemsModelQuery());
                item.Model = listModel.FirstOrDefault(m => m.Id == item.IdModel);
                var listCategory = await _mediator.Send(new GetItemsCategoryQuery());
                item.Category = listCategory.FirstOrDefault(c => c.Id == item.IdCategory);
                item.AddDate = DateTime.Now;
                item.LastUpdated = DateTime.Now;
                var command = new InsertItemCommand(
                   item.Id,
                   item.Name,
                   item.Description,
                   item.Quantity,
                   item.LastUpdated,
                   item.IdCategory,
                   item.Category,
                   item.Brand,
                   item.IdModel,
                   item.Model,
                   item.SerialNumber,
                   item.Location,
                   item.Status,
                   item.Notes,
                   item.AddDate,
                   item.Stock,
                   item.Price
                );

                await _mediator.Send(command);
            }
            else
            {
                var url = $"api/items/{item.Id}"; // URL del método PUT de la API
                var json = JsonConvert.SerializeObject(item); // Serializar el objeto a JSON
                var content = new StringContent(json, Encoding.UTF8, "application/json"); // Crear el contenido de la solicitud
                var response = await _client.PutAsync(url, content); // Llamada al método PUT de la API

                if (response.IsSuccessStatusCode)
                {
                    using (StreamWriter writer = new StreamWriter(@"C:\Users\luiskevin.escudero\Desktop\bien-log.txt"))
                    {
                        writer.WriteLine("bien");
                    }
                }
                else
                {
                    using (StreamWriter writer = new StreamWriter(@"C:\Users\luiskevin.escudero\Desktop\mal-log.txt"))
                    {
                        writer.WriteLine("mal");
                    }
                }
                /*var itemInDb = await _mediator.Send(new GetItemByIdQuery(item.Id));
                
                var listModel = await _mediator.Send(new GetItemsModelQuery());
                var modelInDb = listModel.FirstOrDefault(m => m.Id == item.IdModel);
                
                var listCategory = await _mediator.Send(new GetItemsCategoryQuery());
                var categoryInDb = listCategory.FirstOrDefault(c => c.Id == item.IdCategory);

                itemInDb.Name = item.Name;
                itemInDb.Description = item.Description;
                itemInDb.Model = modelInDb;
                itemInDb.Category = categoryInDb;
                itemInDb.IdModel = item.IdModel;
                itemInDb.IdCategory = item.IdCategory;
                itemInDb.Brand = item.Brand;
                itemInDb.SerialNumber = item.SerialNumber;
                itemInDb.Status = item.Status;
                itemInDb.Location = item.Location;
                itemInDb.Stock = item.Stock;
                itemInDb.Price = item.Price;
                itemInDb.Notes = item.Notes;
                itemInDb.Quantity = item.Quantity;
                string path = @"C:\Users\luiskevin.escudero\Desktop\props-log.txt";
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.WriteLine("id: " + item.Id);
                    writer.WriteLine("name: " + item.Name);
                    writer.WriteLine("description: " + item.Description);
                    writer.WriteLine("quantity: " + item.Quantity);
                    writer.WriteLine("lastUpdated: " + item.LastUpdated);
                    writer.WriteLine("idCategory: " + item.IdCategory);
                    writer.WriteLine("category: " + categoryInDb.Name);
                    writer.WriteLine("brand: " + item.Brand);
                    writer.WriteLine("idModel: " + item.IdModel);
                    writer.WriteLine("model: " + modelInDb.Name);
                    writer.WriteLine("serialNumber: " + item.SerialNumber);
                    writer.WriteLine("location: " + item.Location);
                    writer.WriteLine("status: " + item.Status);
                    writer.WriteLine("notes: " + item.Notes);
                    writer.WriteLine("addDate: " + item.AddDate);
                    writer.WriteLine("stock: " + item.Stock);
                    writer.WriteLine("price: " + item.Price);
                }
                var updateItemCommand = new UpdateItemCommand(
                   item.Id,
                   item.Name,
                   item.Description,
                   item.Quantity,
                   item.LastUpdated,
                   item.IdCategory,
                   item.Category,
                   item.Brand,
                   item.IdModel,
                   item.Model,
                   item.SerialNumber,
                   item.Location,
                   item.Status,
                   item.Notes,
                   item.AddDate,
                   item.Stock,
                   item.Price
                 );
               
                await _mediator.Send(updateItemCommand);*/
            }
            return RedirectToAction("Index", "Items");
        }

        public async Task<ActionResult> Edit(int id)
        {
            var item = await _mediator.Send(new GetItemByIdQuery(id));

            if (item == null)
                return HttpNotFound();

            var viewModel = new ItemFormViewModel
            {
                Item = item,
                ItemModels = await _mediator.Send(new GetItemsModelQuery()),
                ItemCategories = await _mediator.Send(new GetItemsCategoryQuery())
            };

            return View("ItemForm", viewModel);
        }

    }
}