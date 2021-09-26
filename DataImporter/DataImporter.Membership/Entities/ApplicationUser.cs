using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace DataImporter.Membership.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [Required,
         MaxLength(200, ErrorMessage = "Name should be less than 200 Character")]
        public string FirstName { get; set; }
        [Required,
         MaxLength(200, ErrorMessage = "Name should be less than 200 Character")]
        public string LastName { get; set; }
    }
}
