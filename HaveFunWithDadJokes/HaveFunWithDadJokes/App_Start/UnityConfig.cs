using HaveFunWithDadJoke.Helper;
using HaveFunWithDadJokes.Controllers;
using HaveFunWithDadJokes.Helper;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;


namespace HaveFunWithDadJokes
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

          

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IAPIHelper, ApiHelper>();
            container.RegisterType<IClient, Client>(new ContainerControlledLifetimeManager());
            container.RegisterType<ILoggerFactory, LoggerFactory>(new ContainerControlledLifetimeManager());
            container.RegisterType<ILogger<DadJokesController>, Logger<DadJokesController>>(new ContainerControlledLifetimeManager());

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}