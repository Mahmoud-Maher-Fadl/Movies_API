namespace CRUD_API.Dtos
{
    public class Category_Dto
    {
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
