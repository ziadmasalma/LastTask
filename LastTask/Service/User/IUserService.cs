using LastTask.Model;

namespace LastTask.Service.User
{
    public interface IUserService
    {
        Task<Table.User> AddUser(UserRigModel m);
        string CreateToken(string userName);
        Task<Table.User> GetUser(string Email);
    }
}
