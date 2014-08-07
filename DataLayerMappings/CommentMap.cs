using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;

namespace DataLayerMappings
{
	public class CommentMap : EntityTypeConfiguration<Comment>
	{
		public CommentMap()
		{
			Property(a => a.Body).IsRequired();
			Property(a => a.CreatedOn).IsRequired();
		}
	}
}
