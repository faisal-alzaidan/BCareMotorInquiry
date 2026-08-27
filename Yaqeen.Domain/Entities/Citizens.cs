using System;
using System.Collections.Generic;
using System.Text;
using Yaqeen.Domain.Enums;

namespace Yaqeen.Domain.Entities
{
    public class Citizens
    {
        public string NationalId { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; } 
        public string FullName { get; set; }=string.Empty;
        public Gender Gender {  get; set; }
        public string Nationality {  get; set; }=string.Empty;
        public int MaritalStatusId { get; set; }
        public MaritalStatus MaritalStatus { get; set; } = null!;
        public ICollection<Address> Addresses { get; set; } = new List<Address>();
    }
}
