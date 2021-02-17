using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OneStopApp_Api.EntityFramework.Model
{
    [Table("Profile", Schema = "dbo")]
    public class Profile
    {
        [Key]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        [Column("CountryOfCitizenshipId")]
        [ForeignKey("Country")]
        public int? CountryId { get; set; }
        public bool? HasMiddleName { get; set; }
        public virtual Country Country { get; set; }
        public virtual User User { get; set; }
    }
}
