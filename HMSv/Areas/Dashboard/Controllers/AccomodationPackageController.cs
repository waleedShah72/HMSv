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
	//[Authorize(Roles = "Admin,Frontdesk,Manager")]
	public class AccomodationPackageController : Controller
    {
		private AccomodationPackageService _service;
		private AccomodationTypeService _serviceAccomodationtype;
		private DashboardService _serviceDashboard;

		public AccomodationPackageController()
		{
			if (_service == null)
				_service = new AccomodationPackageService();
			if (_serviceAccomodationtype == null)
				_serviceAccomodationtype = new AccomodationTypeService();
			if (_serviceDashboard == null)
				_serviceDashboard = new DashboardService();
		}
		// GET: Dashboard/AccomodationPackage
		public ActionResult Index()
        {
			//viewModel
			var viewModel = new AccomodationPackageListingModel();
			//fetching Accomodation Types
			var accomodationTypes = _serviceAccomodationtype.GetAllAccomodationTypes();
			viewModel.AccomodationTypes = accomodationTypes;
			return View(viewModel);
        }

		public ActionResult Listing(string search, int? accomodationTypeID,int? page)
		{
			page = page ?? 1;
			//viewModel
			var viewModel = new AccomodationPackageListingModel();
			//fetching Accomodation Packages
			var accomodationPackages = _service.GetAllAccomodationPackages();


			viewModel.AccomodationPackages = accomodationPackages;


			if (!string.IsNullOrEmpty(search))
				viewModel.AccomodationPackages = viewModel.AccomodationPackages.Where(ap => ap.Name.ToLower().Contains(search.ToLower())).ToList();

			if (accomodationTypeID.HasValue && accomodationTypeID.Value>0)
				viewModel.AccomodationPackages = viewModel.AccomodationPackages.Where(ap => ap.AccomodationTypeID.Equals(accomodationTypeID.Value)).ToList();

			viewModel.Pager = new PagerViewModel(viewModel.AccomodationPackages.Count, page, 3);

			var skip=(page.Value - 1) * 3;// 3 is the records to skip..
			viewModel.AccomodationPackages=viewModel.AccomodationPackages.OrderBy(a=>a.ID).Skip(skip).Take(3).ToList();
			
			return PartialView("_Listing", viewModel);
		}

		public ActionResult Create()
		{
			var viewModel = new AccomodationPackageCreateModel()
			{
				AccomodationType = _serviceAccomodationtype.GetAllAccomodationTypes()
			};
			return PartialView("_Action", viewModel);
		}

		public ActionResult Edit(int ID)
		{
			var result = _service.GetAccomodationPackageById(ID);
			if (result == null)
			{
				return HttpNotFound();
			}
			else
			{
				var viewModel = new AccomodationPackageCreateModel(result)
				{
					AccomodationType = _serviceAccomodationtype.GetAllAccomodationTypes(),
					AccomodationPackagePictures=result.AccomodationPackagePictures.ToList()
				};
				ViewBag.Action = "Edit";
				return PartialView("_Action", viewModel);
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public JsonResult Save(AccomodationPackageCreateModel model)
		{
			var json = new JsonResult();
			var error="";
			var result = false;

			//model.PictureIDs = "90,67,23" = ["90", "67", "23"] = {90, 67, 23}
			List<int> pictureIDs = !string.IsNullOrEmpty(model.PictureIDs) ? model.PictureIDs.Split(',').Select(x => int.Parse(x)).ToList() : new List<int>();
			var pictures = _serviceDashboard.GetPicturesbyId(pictureIDs);
			AccomodationPackages package;
			if (model.ID == 0)
			{
				package = new AccomodationPackages();
				package.Name = model.Name;
				package.NoOfRooms = model.NoOfRooms.Value;
				package.FeePerNight = model.FeePerNight.Value;
				package.AccomodationTypeID = model.AccomodationTypeID.Value;
				package.Description = model.Description;
				package.AccomodationPackagePictures = new List<AccomodationPackagePictures>();
				package.AccomodationPackagePictures.AddRange(pictures.Select(x => new AccomodationPackagePictures() { PictureID = x.ID }));
			}
			else
			{
			    package = _service.GetAccomodationPackageById(model.ID.Value);
				package.ID = model.ID.Value;
				package.Name = model.Name;
				package.NoOfRooms = model.NoOfRooms.Value;
				package.FeePerNight = model.FeePerNight.Value;
				package.AccomodationTypeID = model.AccomodationTypeID.Value;
				package.Description = model.Description;
				package.AccomodationPackagePictures.Clear();
				package.AccomodationPackagePictures.AddRange(pictures.Select(x => new AccomodationPackagePictures() { PictureID = x.ID, AccomodationPackageID=package.ID }));

			}
			try
			{
				 result = _service.SaveAccomodationPackages(package);
			}
			catch (Exception exp)
			{
				error = exp.InnerException.Message+" "+exp.Message;
			}
			if (result)
			{
				json.Data = new { Success = true, Link = Url.Action("Listing", "AccomodationPackage"),Message=Status.Successfull };
			}
			else
			{
				json.Data = new { Success = false, Link = Url.Action("Listing", "AccomodationPackage"), Message = Status.Failed+" "+error };
			}
			return json;
		}

		public ActionResult Delete(int Id)
		{
			var model = _service.GetAccomodationPackageById(Id);
			var viewModel = new AccomodationPackageCreateModel(model);
			return PartialView("_Delete", viewModel);
		}

		[HttpPost]
		public JsonResult Delete(AccomodationPackages model)
		{
			var json = new JsonResult();
			var error = "";
			var result = false;
			try
			{
				result = _service.DeleteAccomodationPackage(model.ID);
			}
			catch (Exception exp)
			{
				error = exp.Message+" "+exp.InnerException.Message;
			}
			if (result)
			{
				json.Data = new { Success = true, Link = Url.Action("Listing", "AccomodationPackage"),Message=Status.Successfull };
			}
			else
			{
				json.Data = new { Success = false, Link = Url.Action("Listing", "AccomodationPackage"), Message = Status.Failed + " " + error };
			}
			return json;
		}

		protected override void Dispose(bool disposing)
		{
			_service = null;_serviceAccomodationtype = null;
			_serviceDashboard = null;
			base.Dispose(disposing);
		}
	}
}