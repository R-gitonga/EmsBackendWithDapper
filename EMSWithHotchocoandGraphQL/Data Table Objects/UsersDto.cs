using System.ComponentModel.DataAnnotations.Schema;

namespace EMSWithHotchocoandGraphQL.Data_Table_Objects
{
    public class UsersDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public string UserPassword { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? Created_at { get; set; } = DateTime.Now;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? Updated_at { get; set; } = DateTime.Now;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? LastLogin { get; set; } = DateTime.Now;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? RegistrationDate { get; set; } = DateTime.Now;

        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public int RoleId { get; set; }
        public UserRoleDto? Role { get; set; }
    }
}
