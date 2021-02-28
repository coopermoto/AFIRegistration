using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AFIRegistration.Data.Models
{
    public class CustomerDTO
    {
        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Surname { get; set; }

        [Column(TypeName = "nchar(9)")]
        public string PolicyNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [Column(TypeName = "nvarchar(256)")]
        public string EmailAddress { get; set; }
    }
}
