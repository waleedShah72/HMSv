using HMSv.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMSv.Areas.Dashboard.ViewModels
{
	public class AccomodationListingViewModel
	{
		public IList<AccomodationPackages> AccomodationPackages { get; set; }

		public IList<Accomodations> Accomodations { get; set; }

		public PagerViewModel Pager { get; set; }
	}
}