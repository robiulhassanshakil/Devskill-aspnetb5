using System;
using System.Collections.Generic;
using DataImporter.Importing.Entities;
using Microsoft.AspNetCore.Identity;

namespace DataImporter.Membership.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Group> Groups { get; set; }
    }
}
