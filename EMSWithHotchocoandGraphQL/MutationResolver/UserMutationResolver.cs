using EMSWithHotchocoandGraphQL.Data_Table_Objects;
using EMSWithHotchocoandGraphQL.Services;

namespace EMSWithHotchocoandGraphQL.MutationResolver
{
    [ExtendObjectType("Mutation")]
    public class UserMutationResolver
    {
        public int SaveUser(UsersDto user, [Service] IUserService userService)
        {
            return userService.SaveUser(user);
        }
        public string BulkDeleteUsers(List<int> userIds, [Service] IUserService userService)
        {
            return userService.BulkDeleteUsers(userIds);
        }
    }
}
