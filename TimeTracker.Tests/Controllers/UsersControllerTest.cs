using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using System.Web.Routing;
using TimeTracker.Controllers;
using TimeTracker.Models;
using TimeTracker.DAL;

namespace TimeTracker.Tests.Controllers
{
    [TestClass]
    public class UsersControllerTest
    {

        private UsersController controller = new UsersController();

        [TestMethod]
        public void Test_Users_Controller_Edit_Action_Return_Not_Null_View()
        {
            ViewResult result = controller.Edit(5009) as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Test_Users_Controller_Edit_Action_With_Form_Collection()
        {
            User usr = UserUtility.GetUserById(5009);
            FormCollection collection = new FormCollection();
            collection["UserName"] = usr.UserName;
            collection["FirstName"] = usr.UserName + "Edited by unit test";
            collection["LastName"] = usr.LastName;
            collection["Position"] = usr.Position;
            collection["Email"] = usr.Email;

            RedirectToRouteResult res = controller.Edit(5009, collection) as RedirectToRouteResult;

            Assert.IsNotNull(res);
            Assert.IsInstanceOfType(res, typeof(RedirectToRouteResult));

        }

        // Routing tests

        [TestMethod]
        public void Test_Route_Users_Controller_No_Action_No_Id()
        {
            var context = new StubHttpContextForRouting(requestUrl: "~/Users");
            var routes = new RouteCollection();

            RouteConfig.RegisterRoutes(routes);

            RouteData routeData = routes.GetRouteData(context);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Users", routeData.Values["controller"]);
            Assert.AreEqual("Index", routeData.Values["action"]);
            Assert.AreEqual(UrlParameter.Optional, routeData.Values["id"]);
        }

        [TestMethod]
        public void Test_Route_Users_Controller_Active_Action_No_Pars()
        {
            var context = new StubHttpContextForRouting(requestUrl: "~/Users/Active");
            var routes = new RouteCollection();

            RouteConfig.RegisterRoutes(routes);

            RouteData routeData = routes.GetRouteData(context);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Users", routeData.Values["controller"]);
            Assert.AreEqual("Active", routeData.Values["action"]);
            Assert.AreEqual(UrlParameter.Optional, routeData.Values["id"]);
        }

        [TestMethod]
        public void Test_Route_Users_Controller_Edit_Action_With_Pars()
        {
            var context = new StubHttpContextForRouting(requestUrl: "~/Users/Edit/3009");
            var routes = new RouteCollection();

            RouteConfig.RegisterRoutes(routes);

            RouteData routeData = routes.GetRouteData(context);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Users", routeData.Values["controller"]);
            Assert.AreEqual("Edit", routeData.Values["action"]);
            Assert.AreEqual("3009", routeData.Values["id"]);
        }

        [TestMethod]
        public void Test_Route_Users_Controller_Details_Action_With_Pars()
        {
            var context = new StubHttpContextForRouting(requestUrl: "~/Users/Details/3009");
            var routes = new RouteCollection();

            RouteConfig.RegisterRoutes(routes);

            RouteData routeData = routes.GetRouteData(context);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Users", routeData.Values["controller"]);
            Assert.AreEqual("Details", routeData.Values["action"]);
            Assert.AreEqual("3009", routeData.Values["id"]);
        }

        [TestMethod]
        public void Test_Route_Users_Controller_Delete_Action_With_Pars()
        {
            var context = new StubHttpContextForRouting(requestUrl: "~/Users/Delete/3009");
            var routes = new RouteCollection();

            RouteConfig.RegisterRoutes(routes);

            RouteData routeData = routes.GetRouteData(context);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Users", routeData.Values["controller"]);
            Assert.AreEqual("Delete", routeData.Values["action"]);
            Assert.AreEqual("3009", routeData.Values["id"]);
        }

        [TestMethod]
        public void Test_Route_Users_Controller_Tasks_Action_With_Pars()
        {
            var context = new StubHttpContextForRouting(requestUrl: "~/Users/Tasks/3009");
            var routes = new RouteCollection();

            RouteConfig.RegisterRoutes(routes);

            RouteData routeData = routes.GetRouteData(context);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Users", routeData.Values["controller"]);
            Assert.AreEqual("Tasks", routeData.Values["action"]);
            Assert.AreEqual("3009", routeData.Values["id"]);
        }
    }
}
