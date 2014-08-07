using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;

namespace DataLayerMappings
{
	public class LoginKeyStoreMap : EntityTypeConfiguration<LoginKeyStore>
	{
		public LoginKeyStoreMap()
		{
			Property(a => a.CreatedOn).IsRequired();
			Property(a => a.AuthKey).IsRequired();
		}
	}
}
