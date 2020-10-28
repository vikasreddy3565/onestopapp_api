using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneStopApp_Api.EntityFramework.Model.Log
{
    [Table("EventLog", Schema = "dbo")]
    public class EventLog
    {
        [Column("ID")]
        public int Id { get; set; }

        [Column("EventID")]   
        public int? EventId { get; set; }

        public string LogLevel { get; set; }
        
        public string Message { get; set; }

        public DateTime CreatedTime { get; set; }
    }
}