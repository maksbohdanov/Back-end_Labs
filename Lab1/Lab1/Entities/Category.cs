using System.Collections.Generic;

namespace CostAccounting.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public bool? IsCustom { get; set; }

        public virtual User User { get; set; }
        public virtual int? UserId { get; set; }

        public virtual ICollection<Record> Records { get; set; }
    }
}
