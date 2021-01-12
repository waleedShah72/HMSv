using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSv.Entities
{
	public class Accomodations
	{
		public long ID { get; set; }

		public string Name { get; set; }

		public int AccomodationPackageID { get; set; }

		public AccomodationPackages AccomodationPackage { get; set; }

		public ICollection<AccomodationPictures> AccomodationPictures { get; set; }
	}
}
