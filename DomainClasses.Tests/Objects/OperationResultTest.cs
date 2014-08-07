using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Enums;
using DomainClasses.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainClasses.Tests.Objects
{
	[TestClass]
	public class OperationResultTest
	{
		[TestMethod]
		public void CreatingObjectShouldAlwaysYieldSuccess()
		{
			var result = new OperationResult();

			Assert.AreEqual(OperationEnum.Success, result.Result);
		}

		[TestMethod]
		public void CreatingAnObjectWithSuccessShouldAlwaysRetainThatValue()
		{
			var result = new OperationResult(OperationEnum.Success);

			Assert.AreEqual(OperationEnum.Success, result.Result);
		}

		[TestMethod]
		public void CreatingObjectWithFailureShouldAlwaysRetainThatValue()
		{
			var result = new OperationResult(OperationEnum.Failure);

			Assert.AreEqual(OperationEnum.Failure, result.Result);
		}

		[TestMethod]
		public void CreatingObjectWithSuccessShouldHaveEmptyMessageEvenIfGiven()
		{
			var result = new OperationResult(OperationEnum.Success, "Message");

			Assert.AreEqual(string.Empty, result.ErrorMessage);
			Assert.AreEqual(OperationEnum.Success, result.Result);
		}

		[TestMethod]
		public void CreatingObjectWithFailureCanHaveEmptyOrPopualtedString()
		{
			const string inputMessage = "Message";
			var result = new OperationResult(OperationEnum.Failure, inputMessage);

			Assert.AreEqual(OperationEnum.Failure, result.Result);
			Assert.AreEqual(inputMessage, result.ErrorMessage);
		}
	}
}
