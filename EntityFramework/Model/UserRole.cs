using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneStopApp_Api.EntityFramework.Model
{
    [Table("UserRole", Schema = "dbo")]
    public class UserRole
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public virtual User Users { get; set; }
        public virtual Role Roles { get; set; }
    }
}
