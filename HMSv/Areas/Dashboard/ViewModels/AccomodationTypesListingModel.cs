using HMSv.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMSv.Areas.Dashboard.ViewModels
{
	public class AccomodationTypesListingModel
	{
		public IList<AccomodationTypes> AccomodationTypes { get; set; }

		public PagerViewModel Pager { get; set; }
	}
}