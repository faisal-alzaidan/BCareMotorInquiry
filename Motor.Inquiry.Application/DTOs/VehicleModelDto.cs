using System.Text.Json.Serialization;

namespace Motor.Inquiry.Application.DTOs
{
    public class VehicleModelDto

    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

    }
}