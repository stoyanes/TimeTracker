using System;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeTracker.Controllers;
using TimeTracker.Models;

namespace TimeTracker.Tests.Controllers
{
    [TestClass]
    public class TaskControllerTest
    {
        private readonly TaskController controller = new TaskController();

        [TestMethod]
        public void Test_Index_Method_Return_Not_Null_View()
        {
            ViewResult result = this.controller.Index();

            Assert.IsNotNull(result, "Should have returned a ViewResult");
        }

        [TestMethod]
        public void Test_Details_Method_Return_Not_Null()
        {
            ViewResult result = this.controller.Details(2004);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Test_Create_Method_Return_Not_Null()
        {
            ViewResult result = controller.Create() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Test_Create_Task_To_Create_Task_In_Database()
        {
            FormCollection collection = new FormCollection();
            collection["Description"] = "Desr from unit test.";
            collection["Title"] = "test title from unit test";
            collection["StatusId"] = "1";
            collection["StartDate"] = DateTime.Today.ToString();
           
            RedirectToRouteResult res = controller.Create(collection) as RedirectToRouteResult;

            Assert.IsNotNull(res);
            Assert.IsInstanceOfType(res, typeof(RedirectToRouteResult));
        }


        [TestMethod]
        public void Test_Edit_Task_Action_With_Id()
        {
            ViewResult result = controller.Edit(2003) as ViewResult;

            Assert.IsNotNull(result.ViewName);
            Assert.AreEqual(typeof(TaskViewModel), result.Model.GetType());
        }



        // Testing routes for Task controller

        [TestMethod]
        public void Test_Route_Task_Controller_No_Action_No_Id()
        {
            var context = new StubHttpContextForRouting(requestUrl: "~/Task");
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            RouteData routeData = routes.GetRouteData(context);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Task", routeData.Values["controller"]);
            Assert.AreEqual("Index", routeData.Values["action"]);
            Assert.AreEqual(UrlParameter.Optional, routeData.Values["id"]);
        }


        // Testing routes on action with no paramethers
        [TestMethod]
        public void Test_Route_Task_Controller_Action_Create_No_Id()
        {
            var context = new StubHttpContextForRouting(requestUrl: "~/Task/Create");
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            RouteData routeData = routes.GetRouteData(context);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Task", routeData.Values["controller"]);
            Assert.AreEqual("Create", routeData.Values["action"]);
            Assert.AreEqual(UrlParameter.Optional, routeData.Values["id"]);
        }

        [TestMethod]
        public void Test_Route_Task_Controller_Action_Active_No_Id()
        {
            var context = new StubHttpContextForRouting(requestUrl: "~/Task/Active");
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            RouteData routeData = routes.GetRouteData(context);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Task", routeData.Values["controller"]);
            Assert.AreEqual("Active", routeData.Values["action"]);
            Assert.AreEqual(UrlParameter.Optional, routeData.Values["id"]);
        }

        [TestMethod]
        public void Test_Route_Task_Controller_Action_Current_No_Id()
        {
            var context = new StubHttpContextForRouting(requestUrl: "~/Task/Current");
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            RouteData routeData = routes.GetRouteData(context);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Task", routeData.Values["controller"]);
            Assert.AreEqual("Current", routeData.Values["action"]);
            Assert.AreEqual(UrlParameter.Optional, routeData.Values["id"]);
        }


        // Testing routes with actions and ids


        [TestMethod]
        public void Test_Route_Task_Controller_Action_Edit_With_Id()
        {
            var context = new StubHttpContextForRouting(requestUrl: "~/Task/Edit/2002");
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            RouteData routeData = routes.GetRouteData(context);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Task", routeData.Values["controller"]);
            Assert.AreEqual("Edit", routeData.Values["action"]);
            Assert.AreEqual("2002", routeData.Values["id"]);
        }


        [TestMethod]
        public void Test_Route_Task_Controller_Details_Edit_With_Id()
        {
            var context = new StubHttpContextForRouting(requestUrl: "~/Task/Details/2002");
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            RouteData routeData = routes.GetRouteData(context);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Task", routeData.Values["controller"]);
            Assert.AreEqual("Details", routeData.Values["action"]);
            Assert.AreEqual("2002", routeData.Values["id"]);
        }

        [TestMethod]
        public void Test_Route_Task_Controller_Delete_Edit_With_Id()
        {
            var context = new StubHttpContextForRouting(requestUrl: "~/Task/Delete/2002");
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            RouteData routeData = routes.GetRouteData(context);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Task", routeData.Values["controller"]);
            Assert.AreEqual("Delete", routeData.Values["action"]);
            Assert.AreEqual("2002", routeData.Values["id"]);
        }
    }
}
