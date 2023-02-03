using Inventario.DTOs;
using Inventario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace Inventario.Controllers.API
{
    public class ItemsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public ItemsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        public IHttpActionResult GetItems()
        {
            var items = _context.Items
                .Include(m => m.Model)
                .Include(c => c.Category)
                .ToList().Select(ItemMapper.ToDTO);
            
            return Ok(items);
        }

        [HttpGet]
        public IHttpActionResult GetItem(int id)
        {
            var item = _context.Items
                .Include(m => m.Model)
                .Include(c => c.Category)
                .SingleOrDefault(i => i.Id == id);
           
            if (item == null)
                return NotFound();
            
            return Ok(ItemMapper.ToDTO(item));
        }

        [HttpPost]
        public IHttpActionResult CreateItem(ItemDTO itemDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var item = ItemMapper.ToItem(itemDTO);

            var categoryList = _context.ItemCategories.ToList();
            var modelList = _context.itemModels.ToList();

            var category = categoryList.SingleOrDefault(c => c.Id == item.IdCategory);
            var model = modelList.SingleOrDefault(m => m.Id == item.IdModel);

            item.Category = category;
            item.Model = model;

            if (item.Category == null)
                return BadRequest("Category not found");

            if (item.Model == null)
                return BadRequest("Model not found");

            itemDTO.Id = item.Id;

            _context.Items.Add(item);
            _context.SaveChanges();

            itemDTO.Id = item.Id;
            
            return Created(new Uri(Request.RequestUri + "/" + item.Id), itemDTO);
        }
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
        public IHttpActionResult UpdateItem(int id, ItemDTO itemDTO)
        {
            var itemInDb = _context.Items.SingleOrDefault(i => i.Id == id);

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
                var category = _context.ItemCategories.SingleOrDefault(c => c.Id == itemDTO.IdCategory);
                if (category != null)
                    itemInDb.Category = category;
            }

            if (!string.IsNullOrEmpty(itemDTO.Brand))
                itemInDb.Brand = itemDTO.Brand;

            if (itemDTO.IdModel > 0)
            {
                var model = _context.itemModels.SingleOrDefault(m => m.Id == itemDTO.IdModel);
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

            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteItem(int id)
        {
            var itemInDb = _context.Items.SingleOrDefault(i => i.Id == id);

            if (itemInDb == null)
                return NotFound();

            _context.Items.Remove(itemInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
