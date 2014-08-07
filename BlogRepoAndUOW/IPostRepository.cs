using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using DomainClasses.Entities;

namespace BlogRepoAndUOW
{
	public interface IPostRepository : IEntityRepository<BlogPost>
	{
	}
}
