using DomainClasses.Enums;

namespace DomainClasses.Interfaces
{
	public interface IObjectWithState
	{
		ObjectState ObjectState { get; set; }
	}
}
