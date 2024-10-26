namespace Ambwei.Models
{
    public class Process
    {
        public int process_id { get; set; } // pk process_id
        public DateTime created_at { get; set; } // created_at
        public DateTime? finished_at { get; set; } // finished_at (nullable)

        // Foreign keys
        public int pacient_id { get; set; } // fk pacient_id
        public int user_id { get; set; } // fk user_id

        // Navigation properties
        public Pacient Paciente { get; set; }
        public User Usuario { get; set; }
    }
}
