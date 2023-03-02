using Inventario.DTOs;
using Inventario.Models;
using System;
using System.Linq;
using System.Web.Http;
using Inventario.UnitOfWork;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using Inventario.CQRS.Queries;
using Inventario.CQRS.Commands;
using System.Net;
using System.Net.Http;
using System.IO;

namespace Inventario.Controllers.API
{
    public class ItemsController : ApiController
    {
        private readonly IMediator _mediator;

        public ItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<ItemDTO>> GetItems()
        {
            try
            {
                var items = await _mediator.Send(new GetItemListQuery());

                if (items == null)
                {
                    var message = new HttpError("[GETALLITEMS] Items not found");
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
                }

                return items.Select(item => ItemMapper.ToDTO(item)).ToList();
            }
            catch (Exception ex)
            {
                var message = new HttpError($"[GETALLITEMS] An error occurred while processing the request: {ex.Message}");
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message));
            }
        }

        [HttpGet]
        public async Task<ItemDTO> GetItem(int id)
        {
            try
            {
                if (id <= 0)
                {
                    var message = new HttpError($"[GETITEMBYID] Invalid id parameter: {id}");
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, message));
                }

                var item = await _mediator.Send(new GetItemByIdQuery(id));

                if (item == null)
                {
                    var message = new HttpError($"[GETITEMBYID] Item not found with id: {id}");
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
                }

                return ItemMapper.ToDTO(item);
            }
            catch (Exception ex)
            {
                var message = new HttpError($"[GETITEMBYID] An error occurred while processing the request: {ex.Message}");
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message));
            }
        }



        [HttpGet]
        [Route("api/items/models")]
        public async Task<List<ItemModel>> GetItemsModel()
        {
            try
            {
                var itemsModel = await _mediator.Send(new GetItemsModelQuery());

                if (itemsModel == null)
                {
                    var message = new HttpError("[ITEMSMODEL] itemsModel not found");
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
                }

                return itemsModel;
            }
            catch (Exception ex)
            {
                var message = new HttpError($"[ITEMSMODEL] An error occurred while processing the request: {ex.Message}");
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message));
            }
        }


        [HttpGet]
        [Route("api/items/categories")]
        public async Task<List<ItemCategory>> GetItemsCategory()
        {
            try
            {
                var itemsCategory = await _mediator.Send(new GetItemsCategoryQuery());

                if (itemsCategory == null)
                {
                    var message = new HttpError("[ITEMSCATEGORY] itemsCategory not found");
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
                }

                return itemsCategory;
            }
            catch (Exception ex)
            {
                var message = new HttpError($"[ITEMSCATEGORY] An error occurred while processing the request: {ex.Message}");
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message));
            }
        }



        [HttpPost]
        //[Authorize(Roles = RoleName.Admin)]
        public async Task<Item> CreateItem(Item item)
        {
            var itemsModel = await _mediator.Send(new GetItemsModelQuery());

            if (itemsModel == null)
            {
                var message = new HttpError("[CREATE] itemsModel not found");
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
            }

            var itemsCategory = await _mediator.Send(new GetItemsCategoryQuery());

            if (itemsCategory == null)
            {
                var message = new HttpError("[CREATE]ititemsCategoryemsModel not found");
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
            }

            var category = itemsCategory.SingleOrDefault(c => c.Id == item.IdCategory);
            var model = itemsModel.SingleOrDefault(m => m.Id == item.IdModel);

            if (category == null)
            {
                var message = new HttpError("[CREATE]Category not found with id: " + item.IdCategory);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
            }

            if (model == null)
            {
                var message = new HttpError("[CREATE]Model not found with id: " + item.IdModel);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
            }

            item.Category = category;
            item.Model = model;

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

            return await _mediator.Send(command);
        }


        [HttpPut]
        //[Authorize(Roles = RoleName.Admin)]
        public async Task<Item> UpdateItem(int id, Item item)
        {

            var queryItemById = new GetItemByIdQuery(id);
            var itemInDb = await _mediator.Send(queryItemById);

            if (itemInDb == null)
            {
                var message = new HttpError("[UPDATE] itemInDb not found with id: " + id);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
            }

            if (!string.IsNullOrEmpty(item.Name))
                itemInDb.Name = item.Name;

            if (!string.IsNullOrEmpty(item.Description))
                itemInDb.Description = item.Description;

            if (item.Quantity > 0)
                itemInDb.Quantity = item.Quantity;

            if (item.IdCategory > 0)
            {
                var queryItemsCategory = new GetItemsCategoryQuery();
                var itemsCategory = await _mediator.Send(queryItemsCategory);

                if (itemsCategory == null)
                {
                    var message = new HttpError("[UPDATE] itemsCategory not found");
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
                }

                var category = itemsCategory.SingleOrDefault(c => c.Id == item.IdCategory);

                if (category == null)
                {
                    var message = new HttpError("[UPDATE] category not found with id: " + item.IdCategory);
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
                }

                if (category != null)
                    itemInDb.Category = category;
            }

            if (!string.IsNullOrEmpty(item.Brand))
                itemInDb.Brand = item.Brand;

            if (item.IdModel > 0)
            {
                var queryItemsModel = new GetItemsModelQuery();
                var itemsModel = await _mediator.Send(queryItemsModel);

                if (itemsModel == null)
                {
                    var message = new HttpError("[UPDATE] itemsModel not found");
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
                }
                
                var model = itemsModel.SingleOrDefault(m => m.Id == item.IdModel);

                if (model == null)
                {
                    var message = new HttpError("[UPDATE] model not found with id: " + item.IdModel);
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
                }

                if (model != null)
                    itemInDb.Model = model;
            }

            if (!string.IsNullOrEmpty(item.SerialNumber))
                itemInDb.SerialNumber = item.SerialNumber;

            if (!string.IsNullOrEmpty(item.Location))
                itemInDb.Location = item.Location;

            if (!string.IsNullOrEmpty(item.Status))
                itemInDb.Status = item.Status;

            if (!string.IsNullOrEmpty(item.Notes))
                itemInDb.Notes = item.Notes;

            if (item.Stock > 0)
                itemInDb.Stock = item.Stock;

            if (item.Price > 0)
                itemInDb.Price = item.Price;

            if (item.LastUpdated != default(DateTime))
                itemInDb.LastUpdated = item.LastUpdated;

            if (item.AddDate != default(DateTime))
                itemInDb.AddDate = item.AddDate;

            var updateItemCommand = new UpdateItemCommand(
                    itemInDb.Id,
                    itemInDb.Name,
                    itemInDb.Description,
                    itemInDb.Quantity,
                    itemInDb.LastUpdated,
                    itemInDb.Category.Id,
                    itemInDb.Category,
                    itemInDb.Brand,
                    itemInDb.Model.Id,
                    itemInDb.Model,
                    itemInDb.SerialNumber,
                    itemInDb.Location,
                    itemInDb.Status,
                    itemInDb.Notes,
                    itemInDb.AddDate,
                    itemInDb.Stock,
                    itemInDb.Price     
                );
            return await _mediator.Send(updateItemCommand);
        }

        [HttpDelete]
        //[Authorize(Roles = RoleName.Admin)]
        public async Task<Unit> DeleteItem(int id)
        {
            try
            {
                var itemInDb = await _mediator.Send(new GetItemByIdQuery(id));

                if (itemInDb == null)
                {
                    var message = new HttpError("[DELETE] itemInDb not found with id: " + id);
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
                }

                await _mediator.Send(new DeleteItemCommand(itemInDb.Id));
                return Unit.Value;
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                var message = new HttpError("[DELETE] An error occurred while deleting item with id " + id + $": {ex.Message}");
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message));
            }
        }

    }
}
