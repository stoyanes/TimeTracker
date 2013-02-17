using System;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeTracker.Controllers;
using TimeTracker.DAL;

namespace TimeTracker.Tests.Controllers
{
    [TestClass]
    public class WorkingCardControllerTest
    {
        private WorkingCardController controller = new WorkingCardController();


        [TestMethod]
        public void Test_WorkingCard_Controller_Details_Action_Return_Not_Null()
        {
            ViewResult result = controller.Details(1) as ViewResult;

            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void Test_WorkingCard_Controller_Edit_Action_Return_Not_Null()
        {
            ViewResult result = controller.Edit(1) as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Test_WorkingCard_Controller_Edit_Aciton_With_Form_Collection()
        {
            WorkingCard wCard = WorkingCardUtility.GetWorkingCardById(1);
            
            FormCollection collection = new FormCollection();

            collection["Id"] = wCard.Id.ToString();
            collection["StartDate"] = wCard.StartDate.ToString();
            collection["Description"] = wCard.Description;
            collection["WorkingHours"] = wCard.WorkingHours.ToString();

            RedirectToRouteResult result = controller.Edit(1, collection) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        // Testing routes

        [TestMethod]
        public void Test_Route_WorkingCard_Controller_No_Action_No_Id()
        {
            var context = new StubHttpContextForRouting(requestUrl: "~/WorkingCard");
            var routes = new RouteCollection();

            RouteConfig.RegisterRoutes(routes);

            RouteData routeData = routes.GetRouteData(context);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("WorkingCard", routeData.Values["controller"]);
            Assert.AreEqual("Index", routeData.Values["action"]);
            Assert.AreEqual(UrlParameter.Optional, routeData.Values["id"]);

        }

        [TestMethod]
        public void Test_Route_WorkingCard_Controller_Details_Action_With_Id()
        {
            var context = new StubHttpContextForRouting(requestUrl: "~/WorkingCard/Details/1");
            var routes = new RouteCollection();

            RouteConfig.RegisterRoutes(routes);

            RouteData routeData = routes.GetRouteData(context);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("WorkingCard", routeData.Values["controller"]);
            Assert.AreEqual("Details", routeData.Values["action"]);
            Assert.AreEqual("1", routeData.Values["id"]);

        }

        [TestMethod]
        public void Test_Route_WorkingCard_Controller_Edit_Action_With_Id()
        {
            var context = new StubHttpContextForRouting(requestUrl: "~/WorkingCard/Edit/1");
            var routes = new RouteCollection();

            RouteConfig.RegisterRoutes(routes);

            RouteData routeData = routes.GetRouteData(context);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("WorkingCard", routeData.Values["controller"]);
            Assert.AreEqual("Edit", routeData.Values["action"]);
            Assert.AreEqual("1", routeData.Values["id"]);

        }

        [TestMethod]
        public void Test_Route_WorkingCard_Controller_ShowAll_Action_With_Id()
        {
            var context = new StubHttpContextForRouting(requestUrl: "~/WorkingCard/ShowAll/4");
            var routes = new RouteCollection();

            RouteConfig.RegisterRoutes(routes);

            RouteData routeData = routes.GetRouteData(context);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("WorkingCard", routeData.Values["controller"]);
            Assert.AreEqual("ShowAll", routeData.Values["action"]);
            Assert.AreEqual("4", routeData.Values["id"]);
        }

        [TestMethod]
        public void Test_Route_WorkingCard_Controller_FillWorkingCard_Action_With_Id()
        {
            var context = new StubHttpContextForRouting(requestUrl: "~/WorkingCard/FillWorkingCard/2003");
            var routes = new RouteCollection();

            RouteConfig.RegisterRoutes(routes);

            RouteData routeData = routes.GetRouteData(context);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("WorkingCard", routeData.Values["controller"]);
            Assert.AreEqual("FillWorkingCard", routeData.Values["action"]);
            Assert.AreEqual("2003", routeData.Values["id"]);
        }

    }
}
