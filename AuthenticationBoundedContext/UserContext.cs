using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using DataLayerMappings;
using DomainClasses.Entities;

namespace UserBoundedContext
{
	public class UserContext : BaseContext<UserContext>, IUserContext
	{
		public IDbSet<UserProfile> UserProfiles { get; set; }
		public IDbSet<Password> Passwords { get; set; }
		public IDbSet<LoginKeyStore> LoginKeyStores { get; set; }

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
			modelBuilder.Configurations.Add(new PasswordMap());
		}
	}
}
