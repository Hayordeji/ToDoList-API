using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Dto.ToDoItem;
using ToDoList.Interface;
using ToDoList.Model;

namespace ToDoList.Repository
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly ApplicationDbContext _context;
        public ToDoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ToDoItem> CreateItem(ToDoItem item)
        {
            //add item to databse
            await _context.ToDoItems.AddAsync(item);

            //save changes
            _context.SaveChanges();
            return item;
        }

        public async Task<ToDoItem> DeleteItem(int id)
        {
            //find item to delete in database
            var itemToDelete = await _context.ToDoItems.FirstOrDefaultAsync(c => c.Id == id);

            //check if item is null
            if (itemToDelete == null)
            {
                return null;
            }

            //remove item from database
            _context.ToDoItems.Remove(itemToDelete);

            //save changes
            _context.SaveChanges();
            return itemToDelete;
        }

        public async Task<ToDoItem> GetItemById(int id)
        {
            //get item from database
            return await _context.ToDoItems.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<ToDoItem>> GetItems()
        {
            //get list of items in the database
           return await _context.ToDoItems.ToListAsync();
        }

        public async Task<bool> ItemExists(int id)
        {
            //check if item exists
            return await _context.ToDoItems.AnyAsync(c => c.Id == id);       
        }

        public async Task<ToDoItem> UpdateItem(int id, ItemUpdateDto updateModel)
        {
            //find item to update in database
            var itemToUpdate = await _context.ToDoItems.FirstOrDefaultAsync(c => c.Id == id);

            if (itemToUpdate == null)
            {
                return null;
            }

            itemToUpdate.Title = updateModel.Title;
            itemToUpdate.Description = updateModel.Description;
            itemToUpdate.DueDate = updateModel.DueDate;

            //save changes
            await _context.SaveChangesAsync();
            return itemToUpdate;
            
        }
    }
}
