namespace Ambwei.Models
{
    public class Pacient
    {
        public int pacient_id { get; set; } // pk pacient_id
        public string pacient_nome { get; set; } // pacient_nome (varchar(100))
        public string pacient_cpf { get; set; } // pacient_cpf (char(11))
        public int prontuario { get; set; } // prontuario (int(10))
    }
}
