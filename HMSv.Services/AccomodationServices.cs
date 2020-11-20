using HMSv.Data;
using HMSv.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace HMSv.Services
{
	public class AccomodationServices
	{
		public IList<Accomodations> GetAllAccomodations()
		{
			using (var _context = new HMSvContext())
			{
				var result = _context.Accomodation.Include(a => a.AccomodationPackage).ToList();
				return result;
			}
		}

		public Accomodations GetAccomodationById(long Id)
		{
			using (var _context = new HMSvContext())
			{
				var accomodation = _context.Accomodation.Include(ap => ap.AccomodationPackage).SingleOrDefault(a => a.ID == Id);
				if (accomodation == null)
				{
					return null;
				}
				else
				{
					return accomodation;
				}
			}
		}

		public bool SaveAccomodations(Accomodations obj)
		{
			using (var _context = new HMSvContext())
			{
				if (obj.ID == 0)
				{
					_context.Accomodation.Add(obj);
				}
				else
				{
					var accomodation = _context.Accomodation.Single(c => c.ID == obj.ID);
					accomodation.Name = obj.Name;
					accomodation.AccomodationPackageID = obj.AccomodationPackageID;
				}
				return _context.SaveChanges() > 0;
			}
		}

		public bool DeleteAccomodation(long Id)
		{
			using (var _context = new HMSvContext())
			{
				var obj = _context.Accomodation.FirstOrDefault(p => p.ID == Id);
				if (obj != null)
				{
					_context.Accomodation.Remove(obj);
				}
				return _context.SaveChanges() > 0;
			}
		}
	}
}
