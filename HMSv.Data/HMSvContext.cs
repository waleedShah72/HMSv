using HMSv.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HMSv.Data
{
	public class HMSvContext : IdentityDbContext<ApplicationUser>
	{
		public HMSvContext()
			: base("DefaultConnection", throwIfV1Schema: false)
		{
		}

		public static HMSvContext Create()
		{
			return new HMSvContext();
		}

		public DbSet<AccomodationTypes> AccomodationType { get; set; }

		public DbSet<AccomodationPackages> AccomodationPackage { get; set; }

		public DbSet<Accomodations> Accomodation { get; set; }
	}

	// You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
	public class ApplicationUser : IdentityUser
	{
		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
		{
			// Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
			var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
			// Add custom user claims here
			return userIdentity;
		}
	}
}
