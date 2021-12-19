using System;

namespace ChatApp.Models
{
    public class SignedUserModel
    {
        public DateTime CreatedOn => DateTime.Now;
        public string FormattedCreatedOn => CreatedOn.ToString("hh:mm:ss");
        public string Login {get; set; }
    }
}