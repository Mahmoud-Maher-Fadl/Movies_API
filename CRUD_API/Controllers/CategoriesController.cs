
using CRUD_API.Dtos;
using CRUD_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var categories = await _categoryService.GetAll();
            return Ok(categories);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(Category_Dto dto)
        {
            var category = new Category
            {
                Name = dto.Name,
            };
            await _categoryService.Add(category);
            
            return Ok(category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>UpdateAsync(byte id, [FromBody]Category_Dto dto)
        {
            var category=await _categoryService.GetById(id);
            if (category==null)
               return NotFound($"There Is No Category With Id = {id}");
            category.Name=dto.Name;
            _categoryService.Update(category);
            return Ok(category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteAsync(byte id)
        {
            var category = await _categoryService.GetById(id);
            if (category == null)
                return NotFound($"There Is No Category With Id = {id}");
            _categoryService.Delete(category);  
            return Ok(category);

        }
    }
}
