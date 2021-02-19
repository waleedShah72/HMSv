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
		HMSvContext context;
		public IList<AccomodationPackages> GetAllAccomodationPackages()
		{
			using (var _context = new HMSvContext())
			{
				var result = _context.AccomodationPackage.Include(a => a.AccomodationType).Include(x=>x.AccomodationPackagePictures.Select(y=>y.Picture)).ToList();
				return result;
			}
		}

		public AccomodationPackages GetAccomodationPackageById(int Id)
		{
			using (var _context = new HMSvContext())
			{
				var accomodationPackage = _context.AccomodationPackage.Include(ap => ap.AccomodationType).Include(x=>x.AccomodationPackagePictures.Select(p=>p.Picture)).SingleOrDefault(a => a.ID == Id);
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

		public ICollection<AccomodationPackagePictures> GetPicturesbyAccomodationPackageId(int accomodationPackageId)
		{
			if(context==null)
			context = new HMSvContext();
			
				var pictures = context.AccomodationPackage.Find(accomodationPackageId).AccomodationPackagePictures;
				return pictures;
			
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
					var accomodationPackage = _context.AccomodationPackage.Include(x=>x.AccomodationPackagePictures).Single(c => c.ID == obj.ID);
					_context.AccomodationPackagePicture.RemoveRange(accomodationPackage.AccomodationPackagePictures);
					accomodationPackage.Name = obj.Name;
					accomodationPackage.FeePerNight = obj.FeePerNight;
					accomodationPackage.NoOfRooms = obj.NoOfRooms;
					accomodationPackage.AccomodationTypeID = obj.AccomodationTypeID;
					accomodationPackage.Description = obj.Description;
					accomodationPackage.AccomodationPackagePictures = obj.AccomodationPackagePictures;
					_context.AccomodationPackagePicture.AddRange(accomodationPackage.AccomodationPackagePictures);
				}
				return _context.SaveChanges() > 0;
			}
		}

		public bool DeleteAccomodationPackage(int Id)
		{
			using (var _context = new HMSvContext())
			{
				var obj = _context.AccomodationPackage.Include(x=>x.AccomodationPackagePictures).SingleOrDefault(p => p.ID == Id);
				if (obj != null)
				{
					
					_context.AccomodationPackage.Remove(obj);
				}
				return _context.SaveChanges() > 0;
			}
		}

	}
}
