using BordoProje.Data;
using BordoProje.Models;
using BordoProje.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BordoProje.Controllers
{
    public class WeeklyPlanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WeeklyPlanController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(Guid employeeId, Guid? weekId)
        {
            var employee = _context.Employees.Find(employeeId);
            if (employee == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var answers = _context.Answers
                .Where(a => a.EmployeeId == employeeId && (!weekId.HasValue || a.WeekId == weekId.Value))
                .ToList();

            var existingTaskViewModel = answers
                .SelectMany(a => a.ExistingTask ?? Enumerable.Empty<ExistingTask>())
                .Select(et => new WeeklyPlanViewModel.WeeklyPlanExistingTaskViewModel
                {
                    Id = et.Id,
                    Title = et.Title ?? "No Title",
                    Duration = et.Duration,
                }).ToList();

            var workHourViewModels = answers
                .SelectMany(a => a.WorkHours ?? Enumerable.Empty<WorkHour>())
                .Select(wh => new WeeklyPlanViewModel.WorkHourViewModel
                {
                    Id = wh.Id,
                    StartTime = wh.StartTime,
                    EndTime = wh.EndTime,
                    Day = wh.Day,
                    Duration = wh.Duration
                }).ToList();

            var weeklyPlanViewModel = new WeeklyPlanViewModel
            {
                EmployeeId = employee.Id,
                EmployeeName = employee.Name,
                EmployeeEmail = employee.Email,
                ExistingTasks = existingTaskViewModel,
                WorkHours = workHourViewModels,
                PreWeekBriefing = answers.FirstOrDefault()?.PreWeekBriefing,
                TotalWorkHoursDuration = answers.Sum(a => a.WorkHours?.Sum(wh => wh.Duration) ?? 0),
                ExistingTaskDuration = answers.Sum(a => a.ExistingTask?.Sum(et => et.Duration) ?? 0),
                AvailableWorkDuration = answers.Sum(a => a.WorkHours?.Sum(wh => wh.Duration) ?? 0) - answers.Sum(a => a.ExistingTask?.Sum(et => et.Duration) ?? 0),
                IsNonWorkingWeek = answers.All(a => a.WorkHours == null || !a.WorkHours.Any())
            };

            return View(weeklyPlanViewModel);
        }


        [HttpPost]
        public IActionResult SaveWorkHours(WeeklyPlanViewModel model)
        {
            if (model.IsNonWorkingWeek)
            {
                // Eğer çalışılmayan hafta ise, mevcut iş saatlerini ve görevleri temizleyebilirsiniz.
                var existingAnswers = _context.Answers
                    .Where(a => a.EmployeeId == model.EmployeeId && a.WeekId == model.WeekId)
                    .ToList();

                foreach (var answer in existingAnswers)
                {
                    _context.WorkHours.RemoveRange(answer.WorkHours);
                    _context.ExistingTasks.RemoveRange(answer.ExistingTask);
                    _context.Answers.Remove(answer);
                }

                _context.SaveChanges();
            }
            else
            {
                // Mevcut iş saatlerini güncelle
                var existingAnswers = _context.Answers
                    .Where(a => a.EmployeeId == model.EmployeeId && a.WeekId == model.WeekId)
                    .ToList();

                foreach (var answer in existingAnswers)
                {
                    _context.WorkHours.RemoveRange(answer.WorkHours);
                    _context.ExistingTasks.RemoveRange(answer.ExistingTask);
                    _context.Answers.Remove(answer);
                }

                var newAnswer = new Answer
                {
                    Id = Guid.NewGuid(),
                    EmployeeId = model.EmployeeId,
                 //   WeekId = model.WeekID ?? Guid.NewGuid(),
                    PreWeekBriefing = model.PreWeekBriefing,
                    WorkHours = model.WorkHours
                        .Where(wh => wh.StartTime != TimeSpan.Zero && wh.EndTime != TimeSpan.Zero)
                        .Select(wh => new WorkHour
                        {
                            Id = Guid.NewGuid(),
                            StartTime = wh.StartTime,
                            EndTime = wh.EndTime,
                            Day = wh.Day,
                            Duration = (short)(wh.EndTime - wh.StartTime).TotalHours
                        }).ToList(),
                    ExistingTask = model.ExistingTasks
                        .Select(et => new ExistingTask
                        {
                            Id = Guid.NewGuid(),
                            Title = et.Title,
                            Duration = et.Duration
                        }).ToList()
                };

                _context.Answers.Add(newAnswer);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", new { employeeId = model.EmployeeId, weekId = model.WeekId });
        }
    }
}
