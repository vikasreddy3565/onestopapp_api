using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OneStopApp_Api.EntityFramework.Lookup;

namespace OneStopApp_Api.EntityFramework.Model
{
    [Table("Role", Schema = "dbo")]
    public class Role: BaseLookup
    {
    }
}
