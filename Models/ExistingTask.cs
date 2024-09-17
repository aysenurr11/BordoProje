namespace BordoProje.Models
{
    public class ExistingTask
    {


        public Guid Id { get; set; }
        public Guid AnswerId { get; set; } // Foreign key
        public Answer? Answer { get; set; } // Navigation property

        public short Duration { get; set; }

        public string? Title { get; set; }
    }
}


