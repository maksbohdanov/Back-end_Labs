using System.Collections.Generic;

namespace WebApi.Entities.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<int> Records { get; set; }
        public virtual ICollection<int> Categories { get; set; }
    }
}
