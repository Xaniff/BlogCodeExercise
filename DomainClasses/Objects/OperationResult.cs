using DomainClasses.Enums;

namespace DomainClasses.Objects
{
	public class OperationResult
	{
		/// <summary>
		/// With no parameters, this default to a successful operation.
		/// </summary>
		public OperationResult()
		{
			Result = OperationEnum.Success;
		}

		/// <summary>
		/// Creates an <see cref="OperationResult">operation result object</see> with an 
		/// empty error message.
		/// </summary>
		/// <param name="result">The <see cref="OperationEnum">result enumerator</see> of the operation.</param>
		public OperationResult(OperationEnum result)
		{
			Result = result;
		}

		/// <summary>
		/// Creates an <see cref="OperationResult">operation result</see> object with 
		/// the given <see cref="OperationEnum">operation enumerator</see> object and
		/// the given error message.
		/// </summary>
		/// <param name="result">Result of the operation.</param>
		/// <param name="errorMessage">Error message to convey.</param>
		public OperationResult(OperationEnum result, string errorMessage = "")
		{
			Result = result;
			ErrorMessage = errorMessage;
		}

		public OperationEnum Result { get; set; }
		private string _errorMessage = "";
		/// <summary>
		/// The message to pass back to the user. This would typically be an error message
		/// of some sort describing what the issue was.
		/// </summary>
		public string ErrorMessage
		{
			get
			{
				switch (Result)
				{
					case (OperationEnum.Success):
						return "";
					case (OperationEnum.Failure):
						return _errorMessage;
					default:
						return _errorMessage;
				}
			}
			set { _errorMessage = value ?? ""; }
		}
	}
}
