namespace Ambwei.Models
{
    public class User
    {
        public int user_id { get; set; } // pk user_id
        public string user_name { get; set; } // user_name (varchar(50))
        public string user_passwd { get; set; } // user_passwd (varchar(50))

        // Foreign key
        public int role_id { get; set; } // fk role_id

        // Navigation property
        public Role Role { get; set; }
    }
}
