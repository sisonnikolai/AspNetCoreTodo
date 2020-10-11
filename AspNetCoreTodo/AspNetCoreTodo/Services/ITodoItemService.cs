using AspNetCoreTodo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreTodo.Services
{
    public interface ITodoItemService
    {
        Task<TodoItem[]> GetIncompleteItemsAsync(ApplicationUser user);
        Task<bool> AddItemAsync(TodoItem newItem);
        Task<bool> MarkDoneAsync(Guid id);
    }
}
