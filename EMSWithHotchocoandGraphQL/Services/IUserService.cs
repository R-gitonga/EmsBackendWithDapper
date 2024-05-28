using EMSWithHotchocoandGraphQL.Data_Table_Objects;

namespace EMSWithHotchocoandGraphQL.Services
{
    public interface IUserService
    {
        UsersDto GetFirstPerson();
        List<UsersDto> GetAllPersons();
        List<UsersDto> GetByUserName(string userName);
        List<UsersDto> GetUserRole();
        int SaveUser(UsersDto users);
        string BulkDeleteUsers(List<int> userIds);
    }
}
