using System;
using System.Collections.Generic;
using System.Text;

namespace Motor.Inquiry.Application.DTOs
{

    // Common class for all API responses, including success and error responses.
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
    
    }
}
