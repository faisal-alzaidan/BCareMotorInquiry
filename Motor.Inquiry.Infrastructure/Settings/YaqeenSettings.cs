using System;
using System.Collections.Generic;
using System.Text;

namespace Motor.Inquiry.Infrastructure.Settings
{
    public class YaqeenSettings
    {
        public string BaseUrl { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
        public YaqeenEndpoints Endpoints { get; set; } = new();
    }

    public class YaqeenEndpoints
    {
        public string VehicleBySequence { get; set; } = string.Empty;
        public string VehicleByPlate { get; set; } = string.Empty;
        public string ValidateCitizen { get; set; } = string.Empty;
    }
}
