using HMSv.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMSv.ViewModels
{
	public class AccomodationViewModels
	{
		public int? SelectedAccomodationPackageID { get; set; }
		public AccomodationTypes AccomodationType { get; set; }

		public IList<AccomodationPackages> AccomodationPackages { get; set; }
		public IList<Accomodations> Accomodations { get; set; }
	}
}