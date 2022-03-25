using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Entities
{
    public class BaseEntityWithDates<T> : IEntityWithDates<T>
    {
        [Key]
        public T Id { get; set; }
        public bool isDeleted { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public DateTime? LastModificationDate { get; set; }
    }
}
