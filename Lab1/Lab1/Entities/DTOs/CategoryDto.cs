namespace WebApi.Entities.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public bool? IsCustom { get; set; }
        public virtual int? UserId { get; set; }
    }
}
