//using AutoMapper;


using KPMGTest.Interface;
using KPMGTest.Model;
using KPMGTest.Data;
using Ninject;
using System;
using System.Web.Mvc;
using System.Web.Routing;



namespace MyCATPlus.Web.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        #region Fields

        private IKernel ninjectKernel;

        #endregion Fields



        public NinjectControllerFactory()
        {
            // Set ninjectKernel as new instance of StandardKernel
            ninjectKernel = new StandardKernel();
            // Do object bindings
            Bindings();
        }



        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }



        public void Bindings()
        {
                  

            ninjectKernel.Bind<ITaxInfoRepository>().To<EFTaxInfoRepository>();
            
            

        }
    }


}