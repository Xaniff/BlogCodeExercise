using System.ComponentModel.Design;
using DomainClasses.Enums;
using DomainClasses.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;
using UserRepoAndUOW.Actions;

namespace UserRepoAndUOWTest.Actions
{
	[TestClass]
	public class LoginActionTest
	{
		[TestMethod]
		public void ValidateUserWithSuccessfulUsernameAndPassword()
		{
			const string username = "testUser";
			const string password = "testPassword";

			var loginModel = new Login
			{
				Username = username,
				Password = password
			};

			var action = Mock.Create<ILoginProcessorAction>();
			Mock.Arrange(() => action.ValidateUsernameAndPassword(username, password)).Returns(true);

			var sut = new LoginAction(action);
			var result = sut.ValidateUser(loginModel);

			Assert.AreEqual(OperationEnum.Success, result.Result);
			Assert.AreEqual(string.Empty, result.ErrorMessage);
		}

		[TestMethod]
		public void InvalidUsernameShouldFailWithError()
		{
			const string username = "testUser";
			const string password = "testPassword";

			var loginModel = new Login
			{
				Username = username,
				Password = password
			};

			var action = Mock.Create<ILoginProcessorAction>();
			Mock.Arrange(() => action.ValidateUsernameAndPassword(username, password)).Returns(false);
			Mock.Arrange(() => action.CheckThatUsernameExists(username)).Returns(false);

			var sut = new LoginAction(action);
			var result = sut.ValidateUser(loginModel);

			Assert.AreEqual(OperationEnum.Failure, result.Result);
			Assert.AreEqual("No such user... is it typed correctly?", result.ErrorMessage);
		}

		[TestMethod]
		public void PasswordIsIncorrectAndShouldFailWithError()
		{
			const string username = "testUser";
			const string password = "testPassword";

			var loginModel = new Login
			{
				Username = username,
				Password = password
			};

			var action = Mock.Create<ILoginProcessorAction>();
			Mock.Arrange(() => action.ValidateUsernameAndPassword(username, password)).Returns(false);
			Mock.Arrange(() => action.CheckThatUsernameExists(username)).Returns(true);

			var sut = new LoginAction(action);
			var result = sut.ValidateUser(loginModel);

			Assert.AreEqual(OperationEnum.Failure, result.Result);
			Assert.AreEqual("Wrong password. Please try again!", result.ErrorMessage);
		}
	}
}
