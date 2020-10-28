using System.ComponentModel.DataAnnotations.Schema;
using OneStopApp_Api.EntityFramework.Lookup;

namespace OneStopApp_Api.EntityFramework.Model.Lookup
{
    public class BaseCodeLookup : BaseLookup
    {
        public string Code { get; set; }
    }
}