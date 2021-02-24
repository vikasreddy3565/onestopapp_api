using System.ComponentModel.DataAnnotations.Schema;
using OneStopApp_Api.EntityFramework.Audit;

namespace OneStopApp_Api.EntityFramework.Model
{
    [Table("User", Schema = "dbo")]
    public class User : AuditLog
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        [ForeignKey("UserStatus")]
        public int? StatusId { get; set; }
        public virtual UserStatus UserStatus { get; set; }
    }
}
