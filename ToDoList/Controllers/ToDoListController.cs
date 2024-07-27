using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Data;
using ToDoList.Dto.ToDoItem;
using ToDoList.Interface;
using ToDoList.Mapper;
using ToDoList.Model;

namespace ToDoList.Controllers
{
    [Route("api/ToDoList")]
    [ApiController]
    public class ToDoListController : ControllerBase
    {

        private readonly IToDoRepository _toDoRepository;
        public ToDoListController(IToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var items = await _toDoRepository.GetItems();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem([FromRoute] int id) 
        {
            //validation to check if request is correct
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //validation to check if the item exists
            if (!await _toDoRepository.ItemExists(id))
            {
                return BadRequest("Item does not exist");
            }

            //fetches the item requested
            var item = await _toDoRepository.GetItemById(id);

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] ItemCreateDto itemModel)
        {
            //validation to check if request is correct
            if (!ModelState.IsValid)
            {
                return BadRequest("There was error in creating");
            }

            //create new item
            var newItem = itemModel.ToItemCreateDto();
            await _toDoRepository.CreateItem(newItem);
            return Ok(newItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem([FromBody] ItemUpdateDto updateModel, [FromRoute] int id)
        {
            //validation to check if request is correct
            if (!ModelState.IsValid)
            {
                return BadRequest("Wrong Input");
            }

            //update item
            var itemToUpdate = await _toDoRepository.UpdateItem(id, updateModel);

            //check if item is null
            if (itemToUpdate == null)
            {
                return NotFound();
            }

            return Ok(itemToUpdate);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            //validation to check if request is correct
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            //check if item exists
            if (!await _toDoRepository.ItemExists(id))
            {
                return NotFound();
            }


            //delete item
            var itemToDelete = await _toDoRepository.DeleteItem(id);
            return Ok(itemToDelete);


        }

    }
}
