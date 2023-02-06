using Inventario.Models;
using Inventario.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;

namespace Inventario.Controllers
{
    public class ItemsController : Controller
    {

        private readonly ApplicationDbContext _context;

        public ItemsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            if (User.IsInRole(RoleName.Admin))
                return View("List");
            else
                return View("ReadOnlyList");
        }


        public ActionResult Detail(int id)
        {
            var item = _context.Items
                .Include(m => m.Model)
                .Include(c=>c.Category)
                .SingleOrDefault(i => i.Id == id);

            if (item == null)
                return HttpNotFound();

            return View(item);
        }

        [Authorize(Roles = RoleName.Admin)]
        public ActionResult New()
        {
            var itemModels = _context.itemModels.ToList();
            var itemCategories = _context.ItemCategories.ToList();

            var viewModel = new ItemFormViewModel
            {
                Item = new Item(),
                ItemModels = itemModels,
                ItemCategories = itemCategories
            };

            return View("ItemForm", viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Save(Item item)
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
                    ItemModels = _context.itemModels.ToList(),
                    ItemCategories = _context.ItemCategories.ToList()
                };

                return View("ItemForm", viewModel);
            }


            if (item.Id == 0)
            {
                var model = _context.itemModels.ToList();
                var category = _context.ItemCategories.ToList();

                item.Model = model[item.IdModel - 1];
                item.Category = category[item.IdCategory - 1];
                item.AddDate = DateTime.Now;
                item.LastUpdated = DateTime.Now;
                _context.Items.Add(item);
            }
            else
            {
                var itemInDb = _context.Items.Single(i => i.Id == item.Id);
                var modelInDb = _context.itemModels.Single(m => m.Id == item.IdModel);
                var categoryInDb = _context.ItemCategories.Single(c => c.Id == item.IdCategory);
                
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
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Items");
        }


        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Edit(int id)
        {
            var item = _context.Items.SingleOrDefault(i => i.Id == id);

            if (item == null)
                return HttpNotFound();

            var viewModel = new ItemFormViewModel
            {
                Item = item,
                ItemModels = _context.itemModels.ToList(),
                ItemCategories = _context.ItemCategories.ToList()
            };

            return View("ItemForm", viewModel);
        }
    }
}