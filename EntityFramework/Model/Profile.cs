using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OneStopApp_Api.EntityFramework.Model
{
    [Table("Profile", Schema = "dbo")]
    public class Profile
    {
        [Column("userid")]
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        [Column("CountryOfCitizenshipId")]
        [ForeignKey("Country")]
        public int? CountryId { get; set; }
        public virtual Country Country { get; set; }
        public bool? IsDomestic { get; set; }
        [Column("HasMiddleName")]
        public bool? NoMiddleName { get; set; }
    }
}
