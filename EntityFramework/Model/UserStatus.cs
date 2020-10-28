using System.ComponentModel.DataAnnotations.Schema;
using OneStopApp_Api.EntityFramework.Lookup;

namespace OneStopApp_Api.EntityFramework.Model
{
    [Table("UserStatus", Schema = "dbo")]
    public class UserStatus : BaseLookup
    {
    }
}
