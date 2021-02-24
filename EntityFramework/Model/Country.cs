using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OneStopApp_Api.EntityFramework.Model.Lookup;

namespace OneStopApp_Api.EntityFramework.Model
{
    [Table("Country", Schema = "dbo")]
    public class Country : BaseCodeLookup
    {
        public int SortOrder { get; set; }
         public string NiceName { get; set; }
    }
}