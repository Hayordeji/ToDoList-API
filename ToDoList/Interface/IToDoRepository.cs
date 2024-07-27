using ToDoList.Dto.ToDoItem;
using ToDoList.Model;

namespace ToDoList.Interface
{
    public interface IToDoRepository
    {
        public Task<List<ToDoItem>> GetItems();
        public Task<ToDoItem> GetItemById(int id);
        public Task<bool> ItemExists(int id);
        public Task<ToDoItem> CreateItem(ToDoItem item);

        public Task<ToDoItem> UpdateItem(int id, ItemUpdateDto updateModel);

        public Task<ToDoItem> DeleteItem(int id);
    }
}
