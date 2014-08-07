using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;

namespace DataLayerMappings
{
	public class PasswordMap : EntityTypeConfiguration<Password>
	{
		public PasswordMap()
		{
			Property(a => a.Passphrase).IsRequired();
			Property(a => a.Salt).IsRequired();
		}
	}
}
