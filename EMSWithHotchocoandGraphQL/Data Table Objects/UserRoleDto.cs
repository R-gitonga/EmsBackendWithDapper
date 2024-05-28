namespace EMSWithHotchocoandGraphQL.Data_Table_Objects
{
    public class UserRoleDto
    {
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
        public ICollection<UsersDto>? Users { get; set; }
    }
}
