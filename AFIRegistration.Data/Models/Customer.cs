using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AFIRegistration.Data.Models
{
    public class Customer : CustomerDTO
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;
    }
}
