using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CostAccounting.Entities
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public bool IsCustom { get; set; }

        public virtual User User { get; set; }
        public virtual int? UserId { get; set; }

        public virtual ICollection<Record> Records { get; set; }
    }
}
