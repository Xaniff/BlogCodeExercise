using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
	public interface IEntityRepository<T> : IDisposable
	{
		T Find(int id);
		int Add(T newRecord);
		int InsertOrUpdate(T newRecord);
		void Remove(T record);
		IQueryable<T> Grab(Expression<Func<T, bool>> predicate);
		IQueryable<T> GrabAll();
		bool Contains(Expression<Func<T, bool>> predicate);
		void Save();
	}
}
