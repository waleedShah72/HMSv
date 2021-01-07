using HMSv.Data;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace HMSv.Areas.Dashboard.ViewModels
{
	public class UsersCreateModel
	{

		public UsersCreateModel()
		{
			ID = "";
		}

		public UsersCreateModel(ApplicationUser model)
		{
			ID = model.Id;
			UserName = model.UserName;
			FullName = model.FullName;
			Country = model.Country;
			City = model.City;
			Address = model.Address;
		}

		public string ID { get; set; }

		public string UserName { get; set; }

		public string FullName { get; set; }

		public string Country { get; set; }

		public string City { get; set; }

		public string Address { get; set; }

		public string RoleID { get; set; }

		public ICollection<IdentityRole> Roles { get; set; }
	}
}