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
using MediatR;
using Inventario.CQRS.Queries;
using System.Threading.Tasks;
using Inventario.CQRS.Commands;

namespace Inventario.Controllers
{
    public class ItemsController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public ItemsController(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
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
            //var item = _unitOfWork.ItemRepository.Get(id);
            var query = new GetItemByIdQuery(id);
            var item = await _mediator.Send(query);

            

            if (item == null)
                return HttpNotFound();
            _unitOfWork.Dispose();
            return View(item);
        }

        //[Authorize(Roles = RoleName.Admin)]
        public async Task<ActionResult> New() 
        {
            var query = new GetItemsModelQuery();
            var itemModels = await _mediator.Send(query);

            var query1 = new GetItemsCategoryQuery();
            var itemCategories = await _mediator.Send(query1);

            //var itemModels = _unitOfWork.ItemModelRepository.GetAll().ToList();
            //var itemCategories = _unitOfWork.ItemCategoryRepository.GetAll().ToList();

            var viewModel = new ItemFormViewModel
            {
                Item = new Item(),
                ItemModels = itemModels,
                ItemCategories = itemCategories
            };
            _unitOfWork.Dispose();
            return View("ItemForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = RoleName.Admin)]
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
                _unitOfWork.Dispose();
            }
            else
            {
                var itemInDb = await _mediator.Send(new GetItemByIdQuery(item.Id));
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
               
                await _mediator.Send(updateItemCommand);
                _unitOfWork.Save();
                _unitOfWork.Dispose();
            }

            _unitOfWork.Save();
            _unitOfWork.Dispose();
            return RedirectToAction("Index", "Items");
        }
        /*public ActionResult Save(Item item)
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

                _unitOfWork.Save();
            }

            _unitOfWork.Save();

            return RedirectToAction("Index", "Items");
        }*/


        //[Authorize(Roles = RoleName.Admin)]
        /*public ActionResult Edit(int id)
        {
            var item = _unitOfWork.ItemRepository.Get(id);

            if (item == null)
                return HttpNotFound();

            var viewModel = new ItemFormViewModel
            {
                Item = item,
                ItemModels = _unitOfWork.ItemModelRepository.GetAll().ToList(),
                ItemCategories = _unitOfWork.ItemCategoryRepository.GetAll().ToList()
            };

            return View("ItemForm", viewModel);
        }*/
        public async Task<ActionResult> Edit(int id)
        {
            var item = await _mediator.Send(new GetItemByIdQuery(id));
            //_unitOfWork.ItemRepository.Get(id);

            if (item == null)
                return HttpNotFound();

            var viewModel = new ItemFormViewModel
            {
                Item = item,
                ItemModels = await _mediator.Send(new GetItemsModelQuery()),
                //_unitOfWork.ItemModelRepository.GetAll().ToList(),
                ItemCategories = await _mediator.Send(new GetItemsCategoryQuery())
                //_unitOfWork.ItemCategoryRepository.GetAll().ToList()
            };
            _unitOfWork.Dispose();
            return View("ItemForm", viewModel);
        }

    }
}