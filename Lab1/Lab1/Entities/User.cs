using System.Collections.Generic;

namespace Lab1.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Record> Records { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}
