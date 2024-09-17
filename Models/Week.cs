namespace BordoProje.Models
{
    public class Week
    {
        public int? WeekID { get; set; }
        public DateTime? StartDate { get; set; }

        public string? Title { get; set; }
        public ICollection<Answer>? Answers { get; set; } // (1-n) ilişki
    }
}