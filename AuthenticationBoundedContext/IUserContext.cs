using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using DomainClasses.Entities;

namespace UserBoundedContext
{
	public interface IUserContext : IContext
	{
		IDbSet<UserProfile> UserProfiles { get; }
		IDbSet<Password> Passwords { get; }
		IDbSet<LoginKeyStore> LoginKeyStores { get; } 
	}
}
