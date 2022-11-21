using System;

namespace Lab1.Entities
{
    public class Record
    {
        public int Id { get; set; }
        public decimal Sum { get; set; }
        public DateTime CreationTime { get; set; }

        public virtual User User { get; set; }
        public int UserId { get; set; }

        public virtual Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
