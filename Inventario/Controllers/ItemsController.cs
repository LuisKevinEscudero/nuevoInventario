using Inventario.Models;
using Inventario.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;
using Inventario.UnitOfWork;
using System.Web.Configuration;

namespace Inventario.Controllers
{
    public class ItemsController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        /*public ItemsController()
        {
            _unitOfWork = new UnitOfWork.UnitOfWork(new ApplicationDbContext());
        }*/
        public ItemsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
            var item = _unitOfWork.ItemRepository.Get(id);
            if (item == null)
                return HttpNotFound();

            return View(item);
        }



        [Authorize(Roles = RoleName.Admin)]
        public ActionResult New()
        {
            var itemModels = _unitOfWork.ItemModelRepository.GetAll();
            var itemCategories = _unitOfWork.ItemCategoryRepository.GetAll();

            var viewModel = new ItemFormViewModel
            {
                Item = new Item(),
                ItemModels = (List<ItemModel>)itemModels,
                ItemCategories = (List<ItemCategory>)itemCategories
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
                    ItemModels = _unitOfWork.ItemModelRepository.GetAll().ToList(),
                    ItemCategories = _unitOfWork.ItemCategoryRepository.GetAll().ToList()
                };

                return View("ItemForm", viewModel);
            }


            if (item.Id == 0)
            {
                item.Model = _unitOfWork.ItemModelRepository.Get(item.IdModel);
                item.Category = _unitOfWork.ItemCategoryRepository.Get(item.IdCategory);
                item.AddDate = DateTime.Now;
                item.LastUpdated = DateTime.Now;
                _unitOfWork.ItemRepository.Add(item);
            }
            else
            {
                var itemInDb = _unitOfWork.ItemRepository.Get(item.Id);
                var modelInDb = _unitOfWork.ItemModelRepository.Get(item.IdModel);
                var categoryInDb = _unitOfWork.ItemCategoryRepository.Get(item.IdCategory);


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

            _unitOfWork.Save();

            return RedirectToAction("Index", "Items");
        }


        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Edit(int id)
        {
            var item = _unitOfWork.ItemRepository.Get(id);

            if (item == null)
                return HttpNotFound();

            var viewModel = new ItemFormViewModel
            {
                Item = item,
                ItemModels = (List<ItemModel>)_unitOfWork.ItemModelRepository.GetAll(),
                ItemCategories = (List<ItemCategory>)_unitOfWork.ItemCategoryRepository.GetAll()
            };

            return View("ItemForm", viewModel);
        }

    }
}