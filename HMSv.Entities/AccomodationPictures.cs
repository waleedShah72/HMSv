using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSv.Entities
{
	public class AccomodationPictures
	{
		public int ID { get; set; }

		public int PictureID { get; set; }

		public Pictures Picture { get; set; }

		public int AccomodationID { get; set; }

	}
}
