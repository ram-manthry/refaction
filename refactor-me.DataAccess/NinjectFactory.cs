using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Modules;

namespace refactor_me.DataAccess
{
    public class NinjectFactory : NinjectModule
    {
        private IKernel _kernel;
        public override void Load()
        {
            _kernel = new StandardKernel();

            _kernel.Bind(x =>
            {
                x.FromThisAssembly() // Scans currently assembly
                    .SelectAllClasses() // Retrieve all non-abstract classes
                    .BindDefaultInterface(); // Binds the default interface to them;
            });
        }

        public T GetInstance<T>()
        {
            return _kernel.Get<T>();
        }

    }
}