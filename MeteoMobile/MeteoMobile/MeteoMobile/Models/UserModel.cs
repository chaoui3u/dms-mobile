using System;
using System.Collections.Generic;
using System.Text;

namespace MeteoMobile.Models
{
    public class UserModel
    {
        public Guid Id;

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Role { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
    }
}
