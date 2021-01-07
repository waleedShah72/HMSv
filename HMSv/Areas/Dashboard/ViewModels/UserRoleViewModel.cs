using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMSv.Areas.Dashboard.ViewModels
{
	public class UserRoleViewModel
	{
		public string UserID { get; set; }
		public IEnumerable<IdentityRole> Roles { get; set; }

		public IEnumerable<IdentityRole> UserRoles { get; set; }
	}
}