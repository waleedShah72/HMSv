using HMSv.Data;
using HMSv.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSv.Services
{
	public class DashboardService
	{
		public ICollection<Pictures> GetPicturesbyId(List<int> pictureIDs)
		{
			using (var _context=new HMSvContext())
			{
				return pictureIDs.Select(x => _context.Picture.Find(x)).ToList();
			}
		}
		public bool SavePicture(Pictures picture)
		{
			using (var _context = new HMSvContext())
			{
				_context.Picture.Add(picture);
				return _context.SaveChanges() > 0;
			}
		}
	}
}
