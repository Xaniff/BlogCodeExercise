using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using DomainClasses.Entities;

namespace BlogBoundedContext
{
	public interface IPostContext : IContext
	{
		IDbSet<UserProfile> UserProfiles { get; }
		IDbSet<BlogPost> BlogPosts { get; }
		IDbSet<Comment> Comments { get; } 
	}
}
