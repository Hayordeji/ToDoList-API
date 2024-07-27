namespace ToDoList.Dto.ToDoItem
{
    public class ItemUpdateDto
    {
        public string Title { get; set; }
        public string? Description { get; set; } = string.Empty;
        public DateTime? DueDate { get; set; }
    }
}
