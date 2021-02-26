using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneStopApp_Api.EntityFramework.Model
{
    [Table("ClientValidation", Schema = "dbo")]
    public class ClientValidation
    {
        [Key]
        public string Name { get; set; }
        public string Value { get; set; }
    }
}