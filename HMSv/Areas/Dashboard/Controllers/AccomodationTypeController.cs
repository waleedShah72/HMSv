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

		public ActionResult Listing(string search, int? accomodationTypeID, int? page)
		{
			page = page ?? 1;
			var viewModel = new AccomodationTypesListingModel();
			var accomodationTypes = _service.GetAllAccomodationTypes();
			viewModel.AccomodationTypes = accomodationTypes;
			if (!string.IsNullOrEmpty(search))
				viewModel.AccomodationTypes = viewModel.AccomodationTypes.Where(a=>a.Name.ToLower().Contains(search.ToLower())).ToList();

			viewModel.Pager = new PagerViewModel(viewModel.AccomodationTypes.Count, page, 3);
			var skip = (page.Value - 1) * 3;// 3 is the records to skip..
			viewModel.AccomodationTypes = viewModel.AccomodationTypes.OrderBy(a => a.ID).Skip(skip).Take(3).ToList();
			return PartialView("_Listing",viewModel);
		}

		public ActionResult Create()
		{
			var viewModel = new AccomodationTypesCreateModel();
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
				return PartialView("_Action", viewModel);
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public JsonResult Save(AccomodationTypes model)
		{
			var json = new JsonResult();
			var result = false;
			var error = "";
			try
			{
				result = _service.SaveAccomodationTypes(model);
			}
			catch (Exception exp)
			{
				error = exp.Message + " " + exp.InnerException.Message;
			}
			if (result)
			{
				json.Data = new { Success = true, Link = Url.Action("Listing", "AccomodationType"), Message = Status.Successfull + " " + error };
			}
			else
			{
				json.Data = new { Success = false, Link = Url.Action("Listing", "AccomodationType"), Message = Status.Failed + " " + error };
			}
			return json;
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
		public JsonResult Delete(AccomodationTypes model)
		{
			var json = new JsonResult();
			var error = "";
			var result = false;
			try
			{
				result = _service.DeleteAccomodationType(model.ID);
			}
			catch(Exception exp)
			{
				error = exp.Message + " " + exp.InnerException.Message;
			}
			if (result)
			{
				json.Data = new { Success = true, Link = Url.Action("Listing", "AccomodationType"), Message = Status.Successfull };
			}
			else
			{
				json.Data = new { Success = false, Link = Url.Action("Listing", "AccomodationType"), Message = Status.Failed + " " + error };
			}
			return json;
		}
		protected override void Dispose(bool disposing)
		{
			_service = null;
			base.Dispose(disposing);
		}
	}
}