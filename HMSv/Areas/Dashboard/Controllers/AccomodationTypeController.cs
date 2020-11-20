using HMSv.Areas.Dashboard.ViewModels;
using HMSv.Entities;
using HMSv.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMSv.Areas.Dashboard.Controllers
{
    public class AccomodationTypeController : Controller
    {
		private AccomodationTypeService _service;

		public AccomodationTypeController()
		{
			if (_service == null)
				_service = new AccomodationTypeService();
		}

        // GET: Dashboard/AccomodationType
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult Listing(string search)
		{
			var viewModel = new AccomodationTypesListingModel();
			var accomodationTypes = _service.GetAllAccomodationTypes();
			viewModel.AccomodationTypes = accomodationTypes;
			if (!string.IsNullOrEmpty(search))
				viewModel.AccomodationTypes = viewModel.AccomodationTypes.Where(a=>a.Name.ToLower().Contains(search.ToLower())).ToList();
			return PartialView("_Listing",viewModel);
		}

		public ActionResult Create()
		{
			var viewModel = new AccomodationTypesCreateModel();
			ViewBag.Action = "Create";
			return PartialView("_Action", viewModel);
		}

		public ActionResult Edit(int ID)
		{
			var result = _service.GetAccomodationTypeById(ID);
			if (result == null)
			{
				return HttpNotFound();
			}
			else
			{
				var viewModel = new AccomodationTypesCreateModel(result);
				ViewBag.Action = "Edit";
				return PartialView("_Action", viewModel);
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Save(AccomodationTypes model)
		{
			var result = _service.SaveAccomodationTypes(model);
			return RedirectToAction("Listing");
		}

		public ActionResult Delete(int Id)
		{
			var model = _service.GetAccomodationTypeById(Id);
			var viewModel = new AccomodationTypesCreateModel()
			{
				ID = model.ID,
				Name = model.Name,
				Description = model.Description
			};
			return PartialView("_Delete", viewModel);
		}

		[HttpPost]
		public ActionResult Delete(AccomodationTypes model)
		{
			var result = _service.DeleteAccomodationType(model.ID);
			if (result)
			{
				return RedirectToAction("Listing");
			}
			else
			{
				var viewModel = new AccomodationTypesCreateModel(model);
				return PartialView("_Delete", viewModel);
			}
		}
		protected override void Dispose(bool disposing)
		{
			_service = null;
			base.Dispose(disposing);
		}
	}
}