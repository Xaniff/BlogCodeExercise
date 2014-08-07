using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;

namespace DataLayerMappings
{
	public class UserProfileMap : EntityTypeConfiguration<UserProfile>
	{
		public UserProfileMap()
		{
			Property(a => a.Username).IsRequired();
		}
	}
}
