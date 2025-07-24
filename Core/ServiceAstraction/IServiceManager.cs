using ServiceAbstraction;

namespace Services
{
  public interface IServiceManager
    {
        IAuthService AuthService { get; }
    }
}
