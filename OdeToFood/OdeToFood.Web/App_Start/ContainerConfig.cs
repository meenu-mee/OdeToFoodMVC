using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using OdeToFood.Data.Models;
using OdeToFood.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace OdeToFood.Web
{
    public class ContainerConfig
    {
        internal static void RegisterContainer(HttpConfiguration httpConfiguration)
        {
            //using Autofac API to to craete containerbuilder
            var builder = new ContainerBuilder();

            // in container we registered all MVC controllers in this MvcApplication
            // by telling the assembly that contains controllers
            // using custom extension method to integrate MVC5 with Autofac 
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // to register WebApi Controllers in MvcApplication
            builder.RegisterApiControllers(typeof(MvcApplication).Assembly);

            // use InMemoryRestaurantData Type when object of IRestaurantData type
            // is asked and use singleton/consistent data source but not  in the 
            // case when multiple users are present
            //builder.RegisterType<InMemoryRestaurantData>()
            //     .As<IRestaurantData>()
            //       .SingleInstance();

            //now use SqlRestaurantData when appn looks for IRestaurantData
            builder.RegisterType<SqlRestaurantData>()
                   .As<IRestaurantData>()
                   .InstancePerRequest(); //create an instance of SqlRestaurantData for each HTTP request                          

            // register type OdeToFoodDbContext since its passed in ctor of 
            // SqlRestaurantData
            builder.RegisterType<OdeToFoodDbContext>().InstancePerRequest(); 


            // DbContext needs connectionstrings in web.config to tell what server to connect to,
            // what db in server, username and password                   

            //now build a container to be given to MVC5 framework to resolve dependencies
            //set our container as dependency resolver throughout running of the MVC5 framework
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            httpConfiguration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}