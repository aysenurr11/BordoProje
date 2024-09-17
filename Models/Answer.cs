namespace BordoProje.Models
{
    public class Answer
    {

        public Guid  Id { get; set; }
        public Guid EmployeeId { get; set; } // Foreign key
    

        public string? PreWeekBriefing { get; set; }
        public int TotalWorkHoursDuration { get; set; }

        public int ExistingTaskDuration { get; set; }

        public int AvailableWorkDuration { get; set; }

        public int IsNonWorkingWeek { get; set; }
        public Guid WeekId { get; set; } // Foreign key
        public Week? Week { get; set; } // Navigation propertyk

        public Employee? Employee { get; set; } // Navigation property
        public ICollection<WorkHour>? WorkHours { get; set; } // (1-n) ilişki

        public ICollection<ExistingTask>? ExistingTask { get; set; }



    }
}
