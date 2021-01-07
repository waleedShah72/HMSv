using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMSv.Areas.Dashboard.ViewModels
{
	public class RolesListingModel
	{
		public IEnumerable<IdentityRole> Roles { get; set; }

		public PagerViewModel Pager { get; set; }
	}
}