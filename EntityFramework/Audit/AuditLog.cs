using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneStopApp_Api.EntityFramework.Audit
{
    public class AuditLog
    {
        private DateTime _createdDate;
        [Column("CreatedDate")]
        public DateTime CreatedDate 
        { 
            get
            {
                return (_createdDate == null || _createdDate == DateTime.MinValue) ? DateTime.Now : _createdDate;
            }
            set
            {
                _createdDate = value;
            }
        }

        [Column("CreatedBy")]
        public int CreatedBy { get; set; }

        private DateTime? _modifiedDate;

        [Column("UpdatedDate")]
        public DateTime? ModifiedDate 
        {
           get
            {
                return  _modifiedDate;
            }
            set
            {
                _modifiedDate = value;
            }
        }

        [Column("UpdatedBy")]
        public int? ModifiedBy { get; set; }
    }
}
