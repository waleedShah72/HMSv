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
	public class AccomodationPackageService
	{
		public IList<AccomodationPackages> GetAllAccomodationPackages()
		{
			using (var _context = new HMSvContext())
			{
				var result = _context.AccomodationPackage.Include(a => a.AccomodationType).ToList();
				return result;
			}
		}

		public AccomodationPackages GetAccomodationPackageById(int Id)
		{
			using (var _context = new HMSvContext())
			{
				var accomodationPackage = _context.AccomodationPackage.Include(ap => ap.AccomodationType).SingleOrDefault(a => a.ID == Id);
				if (accomodationPackage == null)
				{
					return null;
				}
				else
				{
					return accomodationPackage;
				}
			}
		}

		public bool SaveAccomodationPackages(AccomodationPackages obj)
		{
			using (var _context = new HMSvContext())
			{
				if (obj.ID == 0)
				{
					_context.AccomodationPackage.Add(obj);
				}
				else
				{
					var accomodationPackage = _context.AccomodationPackage.Single(c => c.ID == obj.ID);
					accomodationPackage.Name = obj.Name;
					accomodationPackage.FeePerNight = obj.FeePerNight;
					accomodationPackage.NoOfRooms = obj.NoOfRooms;
					accomodationPackage.AccomodationTypeID = obj.AccomodationTypeID;
				}
				return _context.SaveChanges() > 0;
			}
		}

		public bool DeleteAccomodationPackage(int Id)
		{
			using (var _context = new HMSvContext())
			{
				var obj = _context.AccomodationPackage.FirstOrDefault(p => p.ID == Id);
				if (obj != null)
				{
					_context.AccomodationPackage.Remove(obj);
				}
				return _context.SaveChanges() > 0;
			}
		}
	}
}
