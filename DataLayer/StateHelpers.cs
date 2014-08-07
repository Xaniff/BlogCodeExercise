using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Enums;

namespace DataLayer
{
	public class StateHelpers
	{
		public static EntityState ConvertState(ObjectState state)
		{
			switch (state)
			{
				case ObjectState.Added:
					return EntityState.Added;
				case ObjectState.Deleted:
					return EntityState.Deleted;
				case ObjectState.Modified:
					return EntityState.Modified;
				default:
					return EntityState.Unchanged;
			}
		}
	}
}
