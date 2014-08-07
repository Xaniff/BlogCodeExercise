using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;

namespace BlogRepoAndUOW.Actions
{
	public class CommentAction
	{
		private ICommentProcessorAction _processorAction;

		public CommentAction(ICommentProcessorAction processorAction)
		{
			_processorAction = processorAction;
		}

		/// <summary>
		/// Retrieves the five most recent posts for the recent comment portion of the home page.
		/// </summary>
		/// <returns></returns>
		public List<Comment> GetRecentComments()
		{
			var result = _processorAction.GetRecentComments();
			return result;
		}
	}
}
