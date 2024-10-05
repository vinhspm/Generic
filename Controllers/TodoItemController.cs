using Generic.Models;
using Generic.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Generic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        private IGenericRepository<TodoItem> genericRepository;

        public TodoItemController(IGenericRepository<TodoItem> repository)
        {
            genericRepository = repository;
        }

        [HttpGet("")]
        public ActionResult GetAll()
        {
            var model = genericRepository.GetAll();

            return Ok(model);
        }

        [HttpGet("{id}")]
        public ActionResult<TodoItem> GetById(int id)
        {
            var model = genericRepository.GetById(id);

            if (model == null)
            {
                return NotFound();
            }

            return model;
        }

        [HttpPost]
        public ActionResult<TodoItem> CreateItem(TodoItem item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            genericRepository.Insert(item);

            return item;
        }

        [HttpPut]
        public ActionResult<TodoItem> UpdateItem(TodoItem updatedItem)
        {
            var existingItem = genericRepository.GetById(updatedItem.Id);

            if (existingItem == null)
            {
                return NotFound();
            }
            existingItem.Name = updatedItem.Name;
            existingItem.IsComplete = updatedItem.IsComplete;

            genericRepository.Update(existingItem);

            return existingItem;
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var productToRemove = genericRepository.GetById(id);
            if (productToRemove == null)
            {
                return NotFound();
            }

            genericRepository.Delete(id);

            return NoContent();
        }
    }
}
