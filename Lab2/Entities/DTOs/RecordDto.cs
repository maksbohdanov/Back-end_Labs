using System;

namespace WebApi.Entities.DTOs
{
    public class RecordDto
    {
        public int Id { get; set; }
        public decimal Sum { get; set; }
        public DateTime CreationTime { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
    }
}
