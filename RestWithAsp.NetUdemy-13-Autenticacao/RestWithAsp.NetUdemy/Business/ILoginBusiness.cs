using RestWithAsp.NetUdemy.Model;

namespace RestWithAsp.NetUdemy.Business
{
    public interface ILoginBusiness
    {
        object FindByLogin(User user);
    }
}
