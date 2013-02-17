using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeTracker;
using TimeTracker.Controllers;

namespace TimeTracker.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {



        [TestMethod]
        public void Test_Home_Controller_Index_Action_Set_View_Bag()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.AreEqual("Modify this template to jump-start your ASP.NET MVC application.", result.ViewBag.Message);
        }



        [TestMethod]
        public void Test_Home_Controller_About_Action_For_Returning_Not_Null()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Test_Home_Controller_Contact_Action_For_Returning_Not_Null_View()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
