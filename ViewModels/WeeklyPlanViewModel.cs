namespace BordoProje.ViewModels
{
    public class WeeklyPlanViewModel
    {
        public Guid EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public string? EmployeeEmail { get; set; }

        // Answer bilgileri
        public string? PreWeekBriefing { get; set; }
        public int TotalWorkHoursDuration { get; set; }
        public int ExistingTaskDuration { get; set; }
        public int AvailableWorkDuration { get; set; }
        public bool IsNonWorkingWeek { get; set; }

        // Week bilgileri
        public Guid WeekId { get; set; }
        public string? WeekTitle { get; set; }
        public DateTime WeekStartDate { get; set; }

        // Örnek bir tanım
        public TimeSpan StartTime { get; set; }


        // WorkHours bilgileri
        public List<WorkHourViewModel>? WorkHours { get; set; }

        // ExistingTask bilgileri
        public List<WeeklyPlanExistingTaskViewModel>? ExistingTasks { get; set; }

        public class WeeklyPlanExistingTaskViewModel
        {
            public Guid Id { get; set; }
            public string? Title { get; set; }
            public short Duration { get; set; }
        }

        public class WorkHourViewModel
        {
            public Guid Id { get; set; }
            public TimeSpan StartTime { get; set; }
            public TimeSpan EndTime { get; set; }
            public int Day { get; set; }
            public short Duration { get; set; }
        }
    }
}

//namespace BordoProje.ViewModels
//{
//    public class WorkHourViewModel
//    {

//        public Guid Id { get; set; }
//        public TimeSpan StartTime { get; set; }
//        public TimeSpan EndTime { get; set; }
//        public int Day { get; set; }
//        public short Duration { get; set; }
//    }
//}

