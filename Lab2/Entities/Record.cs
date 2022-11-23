using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CostAccounting.Entities
{
    public class Record
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public decimal Sum { get; set; }
        public DateTime CreationTime { get; set; }

        public virtual User User { get; set; }
        public int UserId { get; set; }

        public virtual Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
