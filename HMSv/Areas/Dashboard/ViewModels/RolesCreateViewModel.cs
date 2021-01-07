using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMSv.Areas.Dashboard.ViewModels
{
	public class RolesCreateViewModel
	{
		public RolesCreateViewModel()
		{
			ID = "";
		}
		public RolesCreateViewModel(IdentityRole role)
		{
			ID = role.Id;
			Name = role.Name;
		}
		public string ID { get; set; }

		public string Name { get; set; }
	}
}