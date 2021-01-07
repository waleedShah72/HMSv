using HMSv.Data;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMSv.Areas.Dashboard.ViewModels
{
	public class UsersListingModel
	{
		public IEnumerable<ApplicationUser> Users { get; set; }

		public IList<IdentityRole> UserRoles { get; set; }

		public PagerViewModel Pager { get; set; }
	}
}