using System;
using System.Collections.Generic;
using System.Text;

namespace Yaqeen.Application.DTOs
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
    
}
}
