using HMSv.Areas.Dashboard.ViewModels;
using HMSv.Data;
using HMSv.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HMSv.Areas.Dashboard.Controllers
{
	public class RolesController : Controller
	{
		private ApplicationSignInManager _signInManager;
		private ApplicationUserManager _userManager;
		private ApplicationRoleManager _roleManager;

		public RolesController()
		{
		}

		public RolesController(ApplicationUserManager userManager, ApplicationSignInManager signInManager,ApplicationRoleManager roleManager)
		{
			UserManager = userManager;
			SignInManager = signInManager;
			RoleManager = roleManager;
		}

		public ApplicationSignInManager SignInManager
		{
			get
			{
				return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
			}
			private set
			{
				_signInManager = value;
			}
		}

		public ApplicationUserManager UserManager
		{
			get
			{
				return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
			}
			private set
			{
				_userManager = value;
			}
		}
		public ApplicationRoleManager RoleManager
		{
			get
			{
				return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
			}
			private set
			{
				_roleManager = value;
			}
		}



		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Listing(string search, int? page)
		{
			page = page ?? 1;
			//viewModel
			var viewModel = new RolesListingModel();
			//fetching Users
			var roles = RoleManager.Roles;

			viewModel.Roles = roles;

			if (!string.IsNullOrEmpty(search))
				viewModel.Roles = viewModel.Roles.Where(u => u.Name.ToLower().Contains(search.ToLower()));

			viewModel.Pager = new PagerViewModel(viewModel.Roles.Count(), page, 3);

			var skip = (page.Value - 1) * 3;// 3 is the records to skip..
			viewModel.Roles = viewModel.Roles.OrderBy(u => u.Name).Skip(skip).Take(3).ToList();

			return PartialView("_Listing", viewModel);
		}

		public ActionResult Create()
		{
			var viewModel = new RolesCreateViewModel();
			return PartialView("_Action", viewModel);
		}

		public async Task<ActionResult> Edit(string ID)
		{

			if (string.IsNullOrEmpty(ID))
			{
				return HttpNotFound();
			}
			else
			{
				var role = await RoleManager.FindByIdAsync(ID);
				var viewModel = new RolesCreateViewModel(role);
				return PartialView("_Action", viewModel);
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<JsonResult> Save(IdentityRole model)
		{
			JsonResult json = new JsonResult();
			var error = "";
			IdentityResult r = null;
			if (!string.IsNullOrEmpty(model.Id))    //Edit Case
			{
				var role = await RoleManager.FindByIdAsync(model.Id);
				role.Name = model.Name;
				r = await RoleManager.UpdateAsync(role);
			}
			else									//Create Case....
			{
				var role = new IdentityRole()
				{
					Name = model.Name
				};
				r = await RoleManager.CreateAsync(role);

			}

			if (r.Succeeded)
			{
				json.Data = new { Success = r.Succeeded, Link = Url.Action("Listing"), Message = Status.Successfull, Errors = string.Join(", ", r.Errors) };
			}
			else
			{
				json.Data = new { Success = r.Succeeded, Link = Url.Action("Listing"), Message = Status.Failed + " " + error, Errors = string.Join(", ", r.Errors) };

			}
			return json;
		}

		public ActionResult Delete(string Id)
		{
			var role = RoleManager.FindById(Id);
			var viewModel = new RolesCreateViewModel(role);
			return PartialView("_Delete", viewModel);
		}

		[HttpPost]
		public JsonResult Delete(IdentityRole model)
		{
			var json = new JsonResult();
			var error = "";
			var role = RoleManager.FindById(model.Id);
			IdentityResult result = null;

			try
			{
				result = RoleManager.Delete(role);
			}
			catch (Exception exp)
			{
				error = exp.Message + " " + exp.InnerException.Message;
			}
			if (result.Succeeded)
			{
				json.Data = new { Success = true, Link = Url.Action("Listing", "Roles"), Message = Status.Successfull };
			}
			else
			{
				json.Data = new { Success = false, Link = Url.Action("Listing", "Roles"), Message = Status.Failed + " " + error };
			}
			return json;

		}
	}
}