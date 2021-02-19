using HMSv.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMSv.ViewModels
{
	public class HomeViewModels
	{
		public IList<AccomodationTypes> AccomodationType { get; set; }
		public IList<AccomodationPackages> AccomodationPackage { get; set; }


	}
}