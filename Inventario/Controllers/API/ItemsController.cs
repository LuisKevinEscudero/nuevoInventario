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
using System.Data.Entity;

namespace Inventario.Controllers.API
{
    public class ItemsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        /*public ItemsController(IMediator mediator, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }*/


        public ItemsController(IMediator mediator)
        {
            _unitOfWork = new UnitOfWork.UnitOfWork(new ApplicationDbContext());
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<ItemDTO>> GetItems()
        {
            var query = new GetItemListQuery();
            var items = await _mediator.Send(query);
            return items.Select(item => ItemMapper.ToDTO(item)).ToList();
        }
        /*[HttpGet]
        public IHttpActionResult GetItems()
        {
            var items = _unitOfWork.ItemRepository
                .GetAll()
                .Select(ItemMapper.ToDTO);
            
            return Ok(items);
        }*/

        [HttpGet]
        public IHttpActionResult GetItem(int id)
        {
            var item = _unitOfWork.ItemRepository.Get(id);
           
            if (item == null)
                return NotFound();
            
            return Ok(ItemMapper.ToDTO(item));
        }


        [HttpPost]
        [Authorize(Roles = RoleName.Admin)]
        public async Task<ItemDTO> CreateItem(ItemDTO itemDTO)
        {
            if (!ModelState.IsValid)
                throw new Exception("Invalid input data");

            var item = ItemMapper.ToItem(itemDTO);

            var categoryList = _unitOfWork.ItemCategoryRepository.GetAll();
            var modelList = _unitOfWork.ItemModelRepository.GetAll();

            var category = categoryList.SingleOrDefault(c => c.Id == item.IdCategory);
            var model = modelList.SingleOrDefault(m => m.Id == item.IdModel);

            item.Category = category;
            item.Model = model;

            if (item.Category == null)
                throw new Exception("Category not found");

            if (item.Model == null)
                throw new Exception("Model not found");

            itemDTO.Id = item.Id;

            _unitOfWork.ItemRepository.Add(item);
            _unitOfWork.Save();

            itemDTO.Id = item.Id;

            return await Task.FromResult(itemDTO);
        }
        /*[HttpPost]
        [Authorize(Roles = RoleName.Admin)]
        public IHttpActionResult CreateItem(ItemDTO itemDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var item = ItemMapper.ToItem(itemDTO);

            var categoryList = _unitOfWork.ItemCategoryRepository.GetAll();
            var modelList = _unitOfWork.ItemModelRepository.GetAll();

            var category = categoryList.SingleOrDefault(c => c.Id == item.IdCategory);
            var model = modelList.SingleOrDefault(m => m.Id == item.IdModel);

            item.Category = category;
            item.Model = model;

            if (item.Category == null)
                return BadRequest("Category not found");

            if (item.Model == null)
                return BadRequest("Model not found");

            itemDTO.Id = item.Id;

            _unitOfWork.ItemRepository.Add(item);
            _unitOfWork.Save();

            itemDTO.Id = item.Id;
            
            return Created(new Uri(Request.RequestUri + "/" + item.Id), itemDTO);
        }*/
        /*json example:
         {
            "Name": "otro item ",
            "Description": "Description3",
            "Quantity": 30,
            "IdCategory": 3,
            "Category": {
                "Name": "Laptops"
            },
            "Brand": "Brand3",
            "IdModel": 3,
            "Model": {
                "Name": "Dell"
            },
            "SerialNumber": "Serial3",
            "Location": "Location3",
            "Status": "Status3",
            "Notes": "Notes3",
            "Stock": 300,
            "Price": 30.0,
            "LastUpdated": "2022-03-01T00:00:00",
            "AddDate": "2021-03-01T00:00:00"
        }*/

        [HttpPut]
        [Authorize(Roles = RoleName.Admin)]
        public IHttpActionResult UpdateItem(int id, ItemDTO itemDTO)
        {

            var itemInDb = _unitOfWork.ItemRepository.Get(id);

            if (itemInDb == null)
                return NotFound();

            if (!string.IsNullOrEmpty(itemDTO.Name))
                itemInDb.Name = itemDTO.Name;

            if (!string.IsNullOrEmpty(itemDTO.Description))
                itemInDb.Description = itemDTO.Description;

            if (itemDTO.Quantity > 0)
                itemInDb.Quantity = itemDTO.Quantity;

            if (itemDTO.IdCategory > 0)
            {

                var category = _unitOfWork.ItemCategoryRepository.Get(itemDTO.IdCategory);
                if (category != null)
                    itemInDb.Category = category;
            }

            if (!string.IsNullOrEmpty(itemDTO.Brand))
                itemInDb.Brand = itemDTO.Brand;

            if (itemDTO.IdModel > 0)
            {

                var model = _unitOfWork.ItemModelRepository.Get(itemDTO.IdModel);
                if (model != null)
                    itemInDb.Model = model;
            }

            if (!string.IsNullOrEmpty(itemDTO.SerialNumber))
                itemInDb.SerialNumber = itemDTO.SerialNumber;

            if (!string.IsNullOrEmpty(itemDTO.Location))
                itemInDb.Location = itemDTO.Location;

            if (!string.IsNullOrEmpty(itemDTO.Status))
                itemInDb.Status = itemDTO.Status;

            if (!string.IsNullOrEmpty(itemDTO.Notes))
                itemInDb.Notes = itemDTO.Notes;

            if (itemDTO.Stock > 0)
                itemInDb.Stock = itemDTO.Stock;

            if (itemDTO.Price > 0)
                itemInDb.Price = itemDTO.Price;

            if (itemDTO.LastUpdated != default(DateTime))
                itemInDb.LastUpdated = itemDTO.LastUpdated;

            if (itemDTO.AddDate != default(DateTime))
                itemInDb.AddDate = itemDTO.AddDate;


            _unitOfWork.Save();

            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = RoleName.Admin)]
        public IHttpActionResult DeleteItem(int id)
        {

            var itemInDb = _unitOfWork.ItemRepository.Get(id);

            if (itemInDb == null)
                return NotFound();


            _unitOfWork.ItemRepository.Delete(itemInDb);

            _unitOfWork.Save();

            return Ok();
        }
    }
}
