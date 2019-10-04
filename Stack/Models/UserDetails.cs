using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stack.Models
{
    public sealed class UserDetails
    {
        //UserDetails
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }

        public string ProfilePicUrl { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        private static readonly Lazy<UserDetails> lazy =
                new Lazy<UserDetails>(() => new UserDetails());

        public static UserDetails Instance { get { return lazy.Value; } }

        private UserDetails()
        {
        }
    }
}