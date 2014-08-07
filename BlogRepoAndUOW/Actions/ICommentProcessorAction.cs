using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;

namespace BlogRepoAndUOW.Actions
{
	public interface ICommentProcessorAction
	{
		/// <summary>
		/// Retrieves the five most recent posts for the recent comment portion of the home page.
		/// </summary>
		/// <returns></returns>
		List<Comment> GetRecentComments();
	}
}
