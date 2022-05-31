using CategoryManager.Domain.Exceptions;
using CategoryManager.Interface.Service;
using CategoryManager.Web.Mapping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace CategoryManager.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryService _categoryService;

        public CategoryController(ILogger<CategoryController> logger, ICategoryService categoryService)
        {
            _logger = logger;
            _categoryService = categoryService;
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> Get()
        {
            var result = await _categoryService.GetAllCategoryAsyn();

            return Ok(JsonConvert.SerializeObject(result.ToResponseModel()));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string objectd)
        {
            try
            {
                using JsonDocument doc = JsonDocument.Parse(objectd);
                JsonElement root = doc.RootElement;
                var request = root.EnumerateObject();

                await _categoryService.CreateCategoryAsyn(request);

                return Ok();
            }
            catch (CategoryChildAlreadyLinkedToParentException linkedEx)
            {
                return BadRequest(linkedEx.Message);
            }
            catch (CategoryDepthMaxWasReachedExceprion maxDepthEx)
            {
                return BadRequest(maxDepthEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpDelete("name:{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            try
            {
                await _categoryService.DeleteCategoryAsync(name);

                return Ok();
            }
            catch (CategoryNotFoundExceprion wasNotFoundEx)
            {
                return BadRequest(wasNotFoundEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }
    }
}