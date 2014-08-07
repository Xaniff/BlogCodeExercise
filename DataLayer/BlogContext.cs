using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayerMappings;
using DomainClasses.Entities;

namespace DataLayer
{
	public class BlogContext : DbContext
	{
		public BlogContext() : base("DatabaseConnectionString")
		{
			
		}

		public DbSet<BlogPost> BlogPosts { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Password> Passwords { get; set; }
		public DbSet<UserProfile> UserProfiles { get; set; }
		public DbSet<LoginKeyStore> LoginKeyStores { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.Add(new BlogPostMap());
			modelBuilder.Configurations.Add(new CommentMap());
			modelBuilder.Configurations.Add(new PasswordMap());
			modelBuilder.Configurations.Add(new UserProfileMap());
			modelBuilder.Configurations.Add(new LoginKeyStoreMap());
		}
	}
}
