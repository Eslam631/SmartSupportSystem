using ServiceAbstraction;

namespace Services
{
    public class ServiceManager(Func<IAuthService> _AuthServiceFunc,Func<IDepartmentService> _DepartmentServiceFunc) : IServiceManager
    {
        public IAuthService AuthService => _AuthServiceFunc.Invoke();

        public IDepartmentService DepartmentService => _DepartmentServiceFunc.Invoke();
    }
}
