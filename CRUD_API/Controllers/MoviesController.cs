using CRUD_API.Data.Entities;
using CRUD_API.Dtos;
using CRUD_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly ICategoryService _categoryService;

        private List<string>allowedExtensions=new List<string>{".png",".jpg" };
        private long maxAllowedPosterSize=1572864;

        public MoviesController(IMovieService movieService, ICategoryService categoryService)
        {
            _movieService = movieService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var movies =await _movieService.GetAll();
            return Ok(movies);
        }
        [HttpGet("GetByID/{id}")]
        public async Task<IActionResult>GetByIDAsync(int id)
        {
            var movie =await _movieService.GetByIdAsync(id);
            if(movie==null)
                return BadRequest($"There Is No Movie with Id = {id}");
            return Ok(movie);
        }
        [HttpGet("GetByCategoryId/{id}")]
        public async Task<IActionResult> GetByCategoryIdAsync(byte id)
        {
            var movies =await _movieService.GetAll(id);
            return Ok(movies);
        }


        [HttpPost]
        public async Task<IActionResult> PostAsync([FromForm]Movie_Dto dto)
        {
            if (!allowedExtensions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return BadRequest("You Can upload files within this extensions only [png,jpg]");
            if (dto.Poster.Length > maxAllowedPosterSize)
                return BadRequest("Yoy can upload files with maximum size 1MB ");
            var isvalidCategory = await _categoryService.IsValidCategory(dto.CategoryId);
            if (!isvalidCategory)
                return BadRequest($"The Category Id you Entered '{dto.CategoryId}' not Exist");
            using var datastream=new MemoryStream();
            await dto.Poster.CopyToAsync(datastream);   
            var movie = new Movie
            {
                CategoryId = dto.CategoryId,
                Title = dto.Title,
                Year = dto.Year,
                Rate = dto.Rate,
                StoryLine = dto.StoryLine,
                Poster = datastream.ToArray(),
                
            };
            _movieService.PostAsync(movie);
            return Ok(movie);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>Delete(int id)
        {

            var movie =await _movieService.GetByIdAsync(id);
            if (movie == null)
                return NotFound($"There Is No Movies With Id = {id}");
            _movieService.Delete(movie);
            return Ok(movie);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id,[FromForm] Movie_Dto dto)
        {
            var movie = await _movieService.GetByIdAsync(id);
            if (movie == null)
                return NotFound($"There Is No Movies With Id = {id}");

            var isvalidCategory = await _categoryService.IsValidCategory(dto.CategoryId);
            if (!isvalidCategory)
                return BadRequest($"The Category Id you Entered '{dto.CategoryId}' not Exist");

            if (dto.Poster!=null)
            {
                if (!allowedExtensions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                    return BadRequest("You Can upload files within this extensions only [png,jpg]");
                if (dto.Poster.Length > maxAllowedPosterSize)
                    return BadRequest("Yoy can upload files with maximum size 1MB ");
                using var datastream = new MemoryStream();
                await dto.Poster.CopyToAsync(datastream);
                movie.Poster = datastream.ToArray();

            } 
            movie.CategoryId = dto.CategoryId;
            movie.Title = dto.Title;
            movie.Year = dto.Year;
            movie.Rate = dto.Rate;
            movie.StoryLine = dto.StoryLine;
            _movieService.Update(movie);
            return Ok(movie);
        }
    }
}
