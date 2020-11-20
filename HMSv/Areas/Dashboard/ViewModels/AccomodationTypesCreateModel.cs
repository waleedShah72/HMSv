using HMSv.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMSv.Areas.Dashboard.ViewModels
{
	public class AccomodationTypesCreateModel
	{
		public AccomodationTypesCreateModel()
		{
			ID = 0;
		}
		public AccomodationTypesCreateModel(AccomodationTypes model)
		{
			ID = model.ID;
			Name = model.Name;
			Description = model.Description;
		}

		public int? ID { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }
	}
}