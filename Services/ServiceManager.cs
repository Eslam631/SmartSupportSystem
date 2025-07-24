using ServiceAbstraction;

namespace Services
{
    public class ServiceManager(Func<IAuthService> _AuthSreviceFunc) : IServiceManager
    {
        public IAuthService AuthService => _AuthSreviceFunc.Invoke();
    }
}
