using HMSv.Data;
using HMSv.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSv.Services
{
	public class AccomodationTypeService
	{
		public IList<AccomodationTypes> GetAllAccomodationTypes()
		{
			using (var _context = new HMSvContext())
			{
				return _context.AccomodationType.ToList();
			}
		}
		public AccomodationTypes GetAccomodationTypeById(int Id)
		{
			using (var _context = new HMSvContext())
			{
				var accomodationType = _context.AccomodationType.SingleOrDefault(a => a.ID == Id);
				if (accomodationType == null)
				{
					return null;
				}
				else
				{
					return accomodationType;
				}
			}
		}

		public bool SaveAccomodationTypes(AccomodationTypes obj)
		{
			using (var _context = new HMSvContext())
			{
				if (obj.ID == 0)
				{
					_context.AccomodationType.Add(obj);
				}
				else
				{
					var product = _context.AccomodationType.Single(c => c.ID == obj.ID);
					product.Name = obj.Name;
				}
				return _context.SaveChanges() > 0;
			}
		}

		public bool DeleteAccomodationType(int Id)
		{
			using (var _context = new HMSvContext())
			{
				var obj = _context.AccomodationType.FirstOrDefault(p => p.ID == Id);
				if (obj != null)
				{
					_context.AccomodationType.Remove(obj);
				}
				return _context.SaveChanges() > 0;
			}
		}

	}
}
