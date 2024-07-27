using ToDoList.Dto.ToDoItem;
using ToDoList.Model;

namespace ToDoList.Mapper
{
    public static class ToDoItemMapper
    {
        public static ToDoItem ToItemCreateDto(this ItemCreateDto itemModel)
        {
            return new ToDoItem
            {
                Title = itemModel.Title,
                Description = itemModel.Description,
                DueDate = itemModel.DueDate
            };
        }

    }
}

