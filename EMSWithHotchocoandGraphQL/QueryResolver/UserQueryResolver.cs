using EMSWithHotchocoandGraphQL.Data_Table_Objects;
using EMSWithHotchocoandGraphQL.Services;

namespace EMSWithHotchocoandGraphQL.QueryResolver
{
    [ExtendObjectType("Query")]
    public class UserQueryResolver
    {
        public UsersDto GetFirstPerson([Service] IUserService userService)
        {
            return userService.GetFirstPerson();
        }
        public List<UsersDto> GetAllPersons([Service] IUserService userService)
        {
            return userService.GetAllPersons();
        }
        public List<UsersDto> UserByFirstName([Service] IUserService userService, string userName)
        {
            return userService.GetByUserName(userName);
        }
        public List<UsersDto> GetUserRole([Service] IUserService userService)
        {
            return userService.GetUserRole();
        }

    }
}
