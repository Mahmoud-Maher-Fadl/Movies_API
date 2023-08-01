using System.Collections.Immutable;

namespace CRUD_API.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAll();
        Task<Category>Add(Category category);
        Task<Category> GetById(byte id);
        Category Update(Category category);
        Category Delete(Category category);
        Task<bool> IsValidCategory(byte id);


    }
}
