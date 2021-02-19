using HMSv.Entities;
using HMSv.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMSv.Areas.Dashboard.Controllers
{
    public class DashboardController : Controller
    {
		private DashboardService _dashboardService;

		public DashboardController()
		{
			if(_dashboardService==null)
			_dashboardService = new DashboardService();
		}

        // GET: Dashboard/Dashboard
        public ActionResult Index()
        {
            return View();
        }

		[HttpPost]
		public JsonResult UploadPicture()
		{
			JsonResult result = new JsonResult();
			var picsList = new List<Pictures>();

			var files = Request.Files;
			for (int i = 0; i < files.Count; i++)
			{
				var picture = files[i];
				var fileName = Guid.NewGuid() + Path.GetExtension(picture.FileName);
				var filePath = Server.MapPath("~/Content/Images/site/") + fileName;

			    picture.SaveAs(filePath);

				//Now saving it to database..
				var dbPicture = new Pictures();
				dbPicture.URL = fileName;

				if (_dashboardService.SavePicture(dbPicture))
				{
					picsList.Add(dbPicture);
				}
				else
				{

				}
			}

			result.Data = picsList;

			return result;
		}
    }
}