using System;
using System.Collections.Generic;
using System.Text;

namespace MeteoMobile.Models
{
    public class UserModel
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
    }
}
