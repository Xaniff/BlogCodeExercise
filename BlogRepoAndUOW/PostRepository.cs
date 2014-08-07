using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BlogBoundedContext;
using DataLayer;
using DomainClasses.Entities;

namespace BlogRepoAndUOW
{
	public class PostRepository : IPostRepository
	{
		private readonly IPostContext _context;

		public PostRepository(IUnitOfWork uow)
		{
			_context = uow.Context as IPostContext;
		}

		public void Dispose()
		{
			_context.Dispose();
		}

		public BlogPost Find(int id)
		{
			return _context.BlogPosts.Find(id);
		}

		public Comment FindComment(int id)
		{
			return _context.Comments.Find(id);
		}

		public int Add(BlogPost newRecord)
		{
			newRecord.CreatedOn = DateTime.Now;
			_context.SetAdd(newRecord);
			Save();
			return newRecord.Id;
		}

		public int Add(Comment newRecord)
		{
			newRecord.CreatedOn = DateTime.Now;
			_context.SetAdd(newRecord);
			Save();
			return newRecord.Id;
		}

		public int InsertOrUpdate(BlogPost newRecord)
		{
			if (newRecord.Id == default(int))
			{
				//New entity
				newRecord.CreatedOn = DateTime.Now;
				_context.SetAdd(newRecord);
			}
			else
			{
				//Existing entity
				_context.SetModified(newRecord);
			}
			Save();
			return newRecord.Id;
		}

		public int InsertOrUpdate(Comment newRecord)
		{
			if (newRecord.Id == default(int))
			{
				//New entity
				newRecord.CreatedOn = DateTime.Now;
				_context.SetAdd(newRecord);
			}
			else
			{
				//Existing entity
				_context.SetModified(newRecord);
			}
			Save();
			return newRecord.Id;
		}

		public void Remove(BlogPost record)
		{
			//Look up the specified record
			var entity = Find(record.Id);
			
			//If this is null, there's nothing to do here
			if (entity == null)
				return;

			//Look up the comments associated with the record
			var comments = GrabComments(a => a.Post.Id == record.Id).ToList();
			if (comments.Count > 0)
			{
				foreach (var comment in comments)
				{
					_context.SetRemove(comment);
				}
				Save();
			}
			
			//Finally, remove the blog post entity
			_context.SetRemove(entity);

			Save();
		}

		public IQueryable<BlogPost> Grab(Expression<Func<BlogPost, bool>> predicate)
		{
			return _context.BlogPosts.Where(predicate).OrderByDescending(a => a.CreatedOn).Include(a => a.Author);
		}

		public IQueryable<Comment> GrabComments(Expression<Func<Comment, bool>> predicate)
		{
			return
				_context.Comments.Where(predicate).OrderBy(a => a.CreatedOn).Include(a => a.Author).Include(a => a.Post);
		}

		public IQueryable<UserProfile> GrabUserProfile(Expression<Func<UserProfile, bool>> predicate)
		{
			return _context.UserProfiles.Where(predicate).Include(a => a.BlogPosts).Include(a => a.Comments);
		}

		public IQueryable<BlogPost> GrabAll()
		{
			return _context.BlogPosts.Include(a => a.Author).Include(a => a.Comments).AsQueryable();
		}

		public IQueryable<UserProfile> GrabAllProfiles()
		{
			return _context.UserProfiles.Include(a => a.BlogPosts).AsQueryable();
		}

		public bool Contains(Expression<Func<BlogPost, bool>> predicate)
		{
			return _context.BlogPosts.Count(predicate) > 0;
		}

		public IQueryable<Comment> GrabAllComments()
		{
			return _context.Comments.Include(a => a.Author).Include(a => a.Post).AsQueryable();
		}

		public void Save()
		{
			_context.SaveChanges();
		}
	}
}
