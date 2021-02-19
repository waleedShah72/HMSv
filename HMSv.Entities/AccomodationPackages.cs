using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSv.Entities
{
	public class AccomodationPackages
	{
		public int ID { get; set; }

		public string Name { get; set; }

		public int NoOfRooms { get; set; }

		public decimal FeePerNight { get; set; }

		public string Description { get; set; }

		public int AccomodationTypeID { get; set; }

		public AccomodationTypes AccomodationType { get; set; }

		public List<AccomodationPackagePictures> AccomodationPackagePictures { get; set; }
	}
}
