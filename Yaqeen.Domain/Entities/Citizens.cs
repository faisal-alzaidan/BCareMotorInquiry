using System;
using System.Collections.Generic;
using System.Text;

namespace Yaqeen.Domain.Entities
{
    public class Citizens
    {
        public string NationalId { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; } 
        public string FullName { get; set; }=string.Empty;
        public string Gender {  get; set; }=string.Empty;
        public string Nationality {  get; set; }=string.Empty;
    }
}
