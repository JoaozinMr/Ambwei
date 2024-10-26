namespace Ambwei.Models
{
    public class Task
    {
        public int task_id { get; set; } // pk task_id
        public DateTime created_at { get; set; } // created_at
        public DateTime? finished_at { get; set; } // finished_at (nullable)
        public string task_reason { get; set; } // task_reason (varchar(100))

        // Foreign keys
        public int? user_id { get; set; } // fk user_id (nullable)
        public int process_id { get; set; } // fk process_id
        public int location_id { get; set; } // fk location_id

        // Navigation properties
        public User Usuario { get; set; }
        public Process Processo { get; set; }
        public Location Local { get; set; }
    }
}
