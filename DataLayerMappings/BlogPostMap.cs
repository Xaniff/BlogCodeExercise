using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;

namespace DataLayerMappings
{
	public class BlogPostMap : EntityTypeConfiguration<BlogPost>
	{
		public BlogPostMap()
		{
			Property(a => a.Title).IsRequired();
			Property(a => a.Message).IsRequired();
			Property(a => a.CreatedOn).IsRequired();
		}
	}
}
