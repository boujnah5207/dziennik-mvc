using System;
using System.Web.Mvc;
using Dziennik_MVC.Models.Entities;
using Dziennik_MVC.Areas.Admin.Controllers;
using Dziennik_MVC.Models.Data.Abstract;
using NUnit.Framework;
using Moq;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using System.Web.Security;

namespace Dziennik_MVC.Tests
{
    [TestFixture]
    public class ProfileControllerTest
    {
        [Test]
        public void Can_View_Admin_Profile()
        {
            // Arrange  
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(x => x.HttpContext.User.Identity.Name).Returns("user3");
            controllerContext.SetupGet(x => x.HttpContext.User.Identity.IsAuthenticated).Returns(true);
            controllerContext.SetupGet(x => x.HttpContext.Request.IsAuthenticated).Returns(true);


            var mock = new Mock<IUsersRepository>();
            mock.Setup(m => m.GetUser(It.IsAny<string>())).Returns(new Admins { Login = controllerContext.Object.HttpContext.User.Identity.Name});


            ProfileController target = new ProfileController(mock.Object);
            target.ControllerContext = controllerContext.Object;

            // Action
            Users result = (Users)target.Profile().Model;

            // Assert

            Assert.AreEqual("user3", result.Login);
        }

        [Test]
        public void Can_Edit_Admin_Profile()
        {
            // Arrange  
            var mock = new Mock<IUsersRepository>();
            mock.Setup(m => m.GetUser(It.IsAny<int>())).Returns( new Admins { UserID = 1, Login="Skubi" } );

            ProfileController target = new ProfileController(mock.Object);
            
            // Action
            Users result = (Users)target.Edit(2).Model;

            // Assert

            Assert.AreEqual("Skubi", result.Login);
        }

        [Test]
        public void Can_Save_Valid_Changes()
        {

            // Arrange - create mock repository
            Mock<IUsersRepository> mock = new Mock<IUsersRepository>();

            // Arrange - create the controller
            ProfileController target = new ProfileController(mock.Object);

            // Arrange - create a Admin
            Admins user = new Admins { Login = "Skubi" };

            // Act - try to save the product
            ActionResult result = target.Edit(user);

            // Assert - check that the repository was called
            mock.Verify(m => m.EditUser(user));

            // Assert - check the method result type
            Assert.IsInstanceOfType(typeof(RedirectToRouteResult), result);
        }
    }
}
