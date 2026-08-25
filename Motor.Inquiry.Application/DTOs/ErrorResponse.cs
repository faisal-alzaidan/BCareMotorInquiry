using System;
using System.Collections.Generic;
using System.Text;

namespace Motor.Inquiry.Application.DTOs
{
    public class ErrorResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string? Error { get; set; }
        public string? StackTrace { get; set; }
    }
}
