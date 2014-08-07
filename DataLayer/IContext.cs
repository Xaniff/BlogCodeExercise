using System;

namespace DataLayer
{
	public interface IContext : IDisposable
	{
		int SaveChanges();
		void SetModified(object entity);
		void SetAdd(object entity);
		void SetRemove(object entity);
	}
}
