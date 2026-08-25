using System;
using System.Collections.Generic;
using System.Text;

namespace Yaqeen.Application.DTOs
{
    public class ErrorDetailsDto
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? DetailedError {  get; set; }

        public override string ToString()
        {
            return System.Text.Json.JsonSerializer.Serialize(this);
        }

    }
}
