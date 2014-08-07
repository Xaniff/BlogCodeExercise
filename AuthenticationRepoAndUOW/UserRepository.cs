using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using UserBoundedContext;
using DomainClasses.Entities;

namespace UserRepoAndUOW
{
	public class UserRepository : IUserRepository
	{
		private readonly IUserContext _context;

		public UserRepository(IUnitOfWork uow)
		{
			_context = uow.Context as IUserContext;
		}

		public void Dispose()
		{
			_context.Dispose();
		}

		public UserProfile Find(int id)
		{
			return _context.UserProfiles.Find(id);
		}

		public LoginKeyStore FindLoginKey(int id)
		{
			return _context.LoginKeyStores.Find(id);
		}

		public int Add(UserProfile newRecord)
		{
			_context.SetAdd(newRecord);
			Save();
			return newRecord.Id;
		}

		public int AddLoginKey(LoginKeyStore newRecord)
		{
			_context.SetAdd(newRecord);
			Save();
			return newRecord.Id;
		}

		public int InsertOrUpdate(UserProfile newRecord)
		{
			if (newRecord.Id == default(int))
			{
				//New entity
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

		public void Remove(UserProfile record)
		{
			//Look up the specified record
			var entity = _context.UserProfiles.Find(record.Id);

			if (entity == null) return;

			_context.SetRemove(entity);
			Save();
		}

		public void RemoveManyLoginKeyStores(IEnumerable<LoginKeyStore> records)
		{
			foreach (var record in records)
			{
				//Look up the specified record
				var entity = _context.LoginKeyStores.Find(record.Id);

				if (entity == null)
					continue;

				_context.SetRemove(entity);
			}
			Save();
		}

		public IQueryable<UserProfile> Grab(Expression<Func<UserProfile, bool>> predicate)
		{
			return _context.UserProfiles.Where(predicate).Include(a => a.LoginKeys).Include(a => a.BlogPosts).Include(a => a.Comments).Include(a => a.Passwords);
		}

		public IQueryable<UserProfile> GrabAll()
		{
			return _context.UserProfiles.AsQueryable();
		}

		public bool Contains(Expression<Func<UserProfile, bool>> predicate)
		{
			return _context.UserProfiles.Count(predicate) > 0;
		}


		public void Save()
		{
			_context.SaveChanges();
		}
	}
}
