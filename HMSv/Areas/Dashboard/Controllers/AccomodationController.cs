﻿using HMSv.Areas.Dashboard.ViewModels;
using HMSv.Entities;
using HMSv.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMSv.Areas.Dashboard.Controllers
{
	public class AccomodationController : Controller
	{
		private AccomodationServices _service;
		private AccomodationPackageService _servicePackages;
		public AccomodationController()
		{
			if(_service==null)
			_service = new AccomodationServices();
			if(_servicePackages==null)
			_servicePackages = new AccomodationPackageService();
		}
		public ActionResult Index()
		{
			//viewModel
			var viewModel = new AccomodationListingViewModel();
			//fetching Accomodation Types
			var accomodationPackages = _servicePackages.GetAllAccomodationPackages();
			viewModel.AccomodationPackages = accomodationPackages;
			return View(viewModel);
		}

		public ActionResult Listing(string search, int? accomodationPackageID, int? page)
		{
			page = page ?? 1;
			//viewModel
			var viewModel = new AccomodationListingViewModel();
			//fetching Accomodation Packages
			var accomodation = _service.GetAllAccomodations();

			viewModel.Accomodations = accomodation;


			if (!string.IsNullOrEmpty(search))
				viewModel.Accomodations = viewModel.Accomodations.Where(ap => ap.Name.ToLower().Contains(search.ToLower())).ToList();

			if (accomodationPackageID.HasValue && accomodationPackageID.Value > 0)
				viewModel.Accomodations = viewModel.Accomodations.Where(ap => ap.AccomodationPackageID.Equals(accomodationPackageID.Value)).ToList();

			viewModel.Pager = new PagerViewModel(viewModel.Accomodations.Count, page, 3);

			var skip = (page.Value - 1) * 3;// 3 is the records to skip..
			viewModel.Accomodations = viewModel.Accomodations.OrderBy(a => a.ID).Skip(skip).Take(3).ToList();

			return PartialView("_Listing", viewModel);
		}

		public ActionResult Create()
		{
			var viewModel = new AccomodationCreateModel()
			{
				AccomodationPackages = _servicePackages.GetAllAccomodationPackages()
			};
			ViewBag.Action = "Create";
			return PartialView("_Action", viewModel);
		}

		public ActionResult Edit(long ID)
		{
			var accomodation = _service.GetAccomodationById(ID);
			if (accomodation==null)
			{
				return HttpNotFound();
			}
			else
			{
				var viewModel = new AccomodationCreateModel(accomodation)
				{
					AccomodationPackages=_servicePackages.GetAllAccomodationPackages()
				};
				ViewBag.Action = "Edit";
				return PartialView("_Action", viewModel);
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Save(Accomodations model)
		{
			var result = _service.SaveAccomodations(model);
			return RedirectToAction("Listing");
		}

		public ActionResult Delete(long Id)
		{
			var model = _service.GetAccomodationById(Id);
			var viewModel = new AccomodationCreateModel(model);
			return PartialView("_Delete", viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(Accomodations model)
		{
			var result = _service.DeleteAccomodation(model.ID);
			if (result)
			{
				return RedirectToAction("Listing");
			}
			else
			{
				var viewModel = new AccomodationCreateModel(model);
				return PartialView("_Delete", viewModel);
			}
		}
	}
}