using LastTask.Model;

namespace LastTask.Service.User
{
    public interface IUserService
    {
        Task<Table.User> AddUser(UserRigModel m);
        string CreateToken(Table.User user);
        Task<Table.User> GetUser(string Email);
        Task<Table.Profile> createProfile(int id , UserModel model);
        int? GetCurrentLoggedIn();
        public void setsessionvalue(Table.User user);

    }
}
