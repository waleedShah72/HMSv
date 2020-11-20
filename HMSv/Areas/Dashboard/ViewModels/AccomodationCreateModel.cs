using HMSv.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMSv.Areas.Dashboard.ViewModels
{
	public class AccomodationCreateModel
	{
		public AccomodationCreateModel()
		{
			ID = 0;
		}

		public AccomodationCreateModel(Accomodations model)
		{
			ID = model.ID;
			Name = model.Name;
			AccomodationPackageID = model.AccomodationPackageID;
		}
		public long? ID { get; set; }

		public string Name { get; set; }

		public int? AccomodationPackageID { get; set; }

		public IList<AccomodationPackages> AccomodationPackages { get; set; }
	}
}