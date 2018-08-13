using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CA1AssignmentTask3.Models
{
    public class Talent
    {
        // Data annotations are used in the Talent model for Model Verification. 
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Symbols and numbers are not allowed.")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Symbols and numbers are not allowed.")]
        public string ShortName { get; set; }
        [Required]
        public string Reknown { get; set; }
        [Required]
        public string Bio { get; set; }
    }
}