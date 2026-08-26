using System;
using System.Collections.Generic;
using System.Text;

namespace Yaqeen.Domain.Entities
{
    public class MaritalStatus
    {
        public int Id { get; set; }
        public string NameAr { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;

        public ICollection<Citizens> Citizens { get; set; } = new List<Citizens>();
    }
}
