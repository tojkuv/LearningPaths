using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HybridTodoApp.Todo;

public class TodoItem
{
    public string? Title { get; set; } = string.Empty;
    public bool IsDone { get; set; } = false;
}