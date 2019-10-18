using DataAccessInfrastructure.Repositories;
using Shared.Interfaces.Managers;
using Data.Manager;
using Ninject.Modules;
using Shared.Interfaces.Repositories;

namespace FamilyControlCenter.DependencyResolver
{
    public class FamilyModule : NinjectModule
    {
        public FamilyModule()
        {
        }

        public override void Load()
        {
            //  register each required repository
            PartialRegisterRepositories();

            //  register each required manager
            PartialRegisterServices();
        }

        private void PartialRegisterRepositories()
        {
            Bind<ISqlRepository>().To<SqlRepository>().InSingletonScope();
        }

        private void PartialRegisterServices()
        {
            //  initialize content manager
            Bind<IFccManager>().To<FccManager>().InSingletonScope();
        }
    }
}