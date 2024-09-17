namespace BordoProje.Models
{
    public class Employee
    {

        public Guid Id { get; set; }
        public string? Name { get; set; }

        public string? Email { get; set; }   
        public ICollection<Answer>? Answers { get; set; } // (1-n) ilişki
    }
}

