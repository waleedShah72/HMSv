using HMSv.Services;
using HMSv.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMSv.Controllers
{
	public class HomeController : Controller
	{
		private AccomodationTypeService _serviceAccomodationType;
		private AccomodationServices _serviceAccomodation;
		private AccomodationPackageService _serviceAccomodationPackage;

		public HomeController()
		{
			if (_serviceAccomodationType == null && _serviceAccomodationPackage == null && _serviceAccomodation == null) {
				_serviceAccomodationType = new AccomodationTypeService();
				_serviceAccomodationPackage = new AccomodationPackageService();
				_serviceAccomodation = new AccomodationServices();
			}
		}
		public ActionResult Index()
		{
			var _service = _serviceAccomodationType;
			var viewModel = new HomeViewModels() {
				AccomodationType=_service.GetAllAccomodationTypes()
			};

			return View(viewModel);
		}

		public ActionResult GetAccomodation(int? accomodationTypeID, int? accomodationPackageID)
		{
			var accomodationType = _serviceAccomodationType.GetAccomodationTypeById(accomodationTypeID.Value);
			var accomodationPackages = _serviceAccomodationPackage.GetAllAccomodationPackages().Where(x => x.AccomodationTypeID == accomodationType.ID).ToList();
			var selectedPackageID = accomodationPackageID.HasValue ? accomodationPackageID.Value : accomodationPackages.First().ID;
			var accomodations = _serviceAccomodation.GetAllAccomodations().Where(x => x.AccomodationPackageID == selectedPackageID).ToList();

			var viewModel = new AccomodationViewModels() {
				AccomodationType=accomodationType,
				AccomodationPackages=accomodationPackages,
				SelectedAccomodationPackageID=selectedPackageID,
				Accomodations=accomodations
			};
			return View(viewModel);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				_serviceAccomodation = null;
				_serviceAccomodationPackage = null;
				_serviceAccomodationType = null;
			}
			base.Dispose(disposing);
		}
	}
}