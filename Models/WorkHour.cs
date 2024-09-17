namespace BordoProje.Models
{
    public class WorkHour
    {
        public Guid Id { get; set; }
        public Guid AnswerId { get; set; } // Foreign key
        public Answer? Answer { get; set; } // Navigation property

        public short Duration { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public int Day { get; set; }
    }



}

