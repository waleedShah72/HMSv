using HMSv.Areas.Dashboard.ViewModels;
using HMSv.Data;
using HMSv.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HMSv.Areas.Dashboard.Controllers
{
	public class UserController : Controller
	{
		private ApplicationSignInManager _signInManager;
		private ApplicationUserManager _userManager;
		private ApplicationRoleManager _roleManager;


		public UserController()
		{
		}

		public UserController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, ApplicationRoleManager roleManager)
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
			//viewModel
			var viewModel = new UsersListingModel();
			//fetching Roles
			var roles = RoleManager.Roles;
			viewModel.UserRoles = roles.ToList();
			return View(viewModel);
		}

		public ActionResult Listing(string search, string roleID, int? page)
		{
			page = page ?? 1;
			//viewModel
			var viewModel = new UsersListingModel();
			//fetching Users
			var users = UserManager.Users;

			if (!string.IsNullOrEmpty(search))
				users = users.Where(u => u.Email.Contains(search));

			if (!string.IsNullOrEmpty(roleID))
				users = users.Where(u => u.Roles.Select(x=>x.RoleId).Contains(roleID));

			viewModel.Pager = new PagerViewModel(users.Count(), page, 3);

			var skip = (page.Value - 1) * 3;// 3 is the records to skip..
			viewModel.Users = users.OrderBy(u => u.Email).Skip(skip).Take(3).ToList();

			return PartialView("_Listing", viewModel);
		}

		public ActionResult Create()
		{
			var viewModel = new UsersCreateModel();
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
				var user = await UserManager.FindByIdAsync(ID);
				var viewModel = new UsersCreateModel(user);
				return PartialView("_Action", viewModel);
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<JsonResult> Save(ApplicationUser model)
		{
			JsonResult json = new JsonResult();
			var error = "";
			IdentityResult r = null;
				if (!string.IsNullOrEmpty(model.Id))    //Edit Case
				{
					var user = await UserManager.FindByIdAsync(model.Id);
					user.UserName = model.UserName;
					user.Email = model.UserName;
					user.FullName = model.FullName;
					user.Country = model.Country;
					user.City = model.City;
					user.Address = model.Address;

					r = await UserManager.UpdateAsync(user);
				}
				else
				{
					var user = new ApplicationUser()
					{
						UserName = model.UserName,
						Email = model.UserName,
						FullName = model.FullName,
						Country = model.Country,
						City = model.City,
						Address = model.Address
					};
					r = await UserManager.CreateAsync(user);
					
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

		public async Task<ActionResult> UserRole(string ID)
		{
			var viewModel = new UserRoleViewModel();
			var user = await UserManager.FindByIdAsync(ID);
			var roles = RoleManager.Roles;

			var userRoleIds = user.Roles.Select(x => x.RoleId).ToList();
			viewModel.UserID = ID;
			viewModel.Roles = roles.Where(x=>!userRoleIds.Contains(x.Id)).ToList();
			viewModel.UserRoles = roles.Where(x => userRoleIds.Contains(x.Id)).ToList();
			return PartialView("_UserRole",viewModel);
		}

		[HttpPost]
		public async Task<JsonResult> SaveUserRole(string userID, string roleID, bool isDelete=false)
		{
			var json = new JsonResult();
			var user = await UserManager.FindByIdAsync(userID);
			var role = await RoleManager.FindByIdAsync(roleID);
			var result = new IdentityResult();

			if (user != null && role != null)
			{
				switch (isDelete)
				{
					case false:
						result = await UserManager.AddToRoleAsync(user.Id, role.Name);
						break;
					case true:
						result = await UserManager.RemoveFromRoleAsync(user.Id, role.Name);
						break;
				}
				json.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors), Link = Url.Action("Listing", "User") };
			}
			else
			{
				json.Data = new { Success = false, Message = "No User or role found" };

			}
			return json;
		}

		public ActionResult Delete(string Id)
		{
			var user = UserManager.FindById(Id);
			var viewModel = new UsersCreateModel(user);
			return PartialView("_Delete", viewModel);
		}

		[HttpPost]
		public JsonResult Delete(ApplicationUser model)
		{
			var json = new JsonResult();
			var error = "";
			var user = UserManager.FindById(model.Id);
			IdentityResult result = null;
			
			try
			{
				result = UserManager.Delete(user);
			}
			catch (Exception exp)
			{
				error = exp.Message + " " + exp.InnerException.Message;
			}
			if (result.Succeeded)
			{
				json.Data = new { Success = true, Link = Url.Action("Listing", "User"), Message = Status.Successfull };
			}
			else
			{
				json.Data = new { Success = false, Link = Url.Action("Listing", "User"), Message = Status.Failed + " " + error };
			}
			return json;

		}
	}
}