using HMSv.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMSv.Areas.Dashboard.ViewModels
{
	public class AccomodationPackageCreateModel
	{
		public AccomodationPackageCreateModel()
		{
			ID = 0;
		}
		public AccomodationPackageCreateModel(AccomodationPackages model)
		{
			ID = model.ID;
			Name = model.Name;
			NoOfRooms = model.NoOfRooms;
			FeePerNight = model.FeePerNight;
			AccomodationTypeID = model.AccomodationTypeID;
			Description = model.Description;
		}
		public int? ID { get; set; }

		public string Name { get; set; }

		public int? NoOfRooms { get; set; }

		public decimal? FeePerNight { get; set; }

		public int? AccomodationTypeID { get; set; }

		public string Description { get; set; }

		public string PictureIDs { get; set; }

		public IList<AccomodationTypes> AccomodationType { get; set; }

		public List<AccomodationPackagePictures> AccomodationPackagePictures { get; set; }
	}
}