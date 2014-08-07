using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Enums;
using DomainClasses.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;
using UserRepoAndUOW.Actions;

namespace UserRepoAndUOWTest.Actions
{
	[TestClass]
	public class RegistrationActionTest
	{
		[TestMethod]
		public void ValidateWithValidRegistrationModel()
		{
			var model = new Registration
			{
				Username = "testUser",
				Password = "testPassword"
			};

			var action = Mock.Create<IRegistrationProcessorAction>();
			Mock.Arrange(() => action.ValidateRegistration(model)).Returns(new OperationResult(OperationEnum.Success));

			var sut = new RegistrationAction(action);
			var result = sut.ValidateRegistration(model);

			Assert.AreEqual(OperationEnum.Success, result.Result);
		}
	}
}
