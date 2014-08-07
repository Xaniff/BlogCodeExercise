using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using DataLayerMappings;
using DomainClasses.Entities;

namespace BlogBoundedContext
{
	public class PostContext : BaseContext<PostContext>, IPostContext
	{
		public IDbSet<UserProfile> UserProfiles { get; set; }
		public IDbSet<BlogPost> BlogPosts { get; set; }
		public IDbSet<Comment> Comments { get; set; } 

		public void SetModified(object entity)
		{
			Entry(entity).State = EntityState.Modified;
		}

		public void SetAdd(object entity)
		{
			Entry(entity).State = EntityState.Added;
		}

		public void SetRemove(object entity)
		{
			Entry(entity).State = EntityState.Deleted;
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.Add(new UserProfileMap());
			modelBuilder.Configurations.Add(new BlogPostMap());
			modelBuilder.Configurations.Add(new CommentMap());
		}
	}
}
