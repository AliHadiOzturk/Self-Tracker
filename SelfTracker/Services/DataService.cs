using SelfTracker.Helpers;
using SelfTracker.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SelfTracker.Services
{
    public class DataService
    {
        public DayService DayService { get; set; }
        public MonthService MonthService { get; set; }
        public YearService YearService { get; set; }
        public EventService EventService { get; set; }
        public DataService()
        {
            this.DayService = DependencyService.Get<DayService>();
            this.MonthService = DependencyService.Get<MonthService>();
            this.YearService = DependencyService.Get<YearService>();
            this.EventService = DependencyService.Get<EventService>();
        }

        public async Task<int> CreateRelationsAndSaveEvent(Event _event, DateTime date)
        {
            if (_event != null)
            {
                var year = await YearService.GetByYear(date.Year);
                if (year == null)
                    year = await YearService.Save(new YearModel() { Year = date.Year });

                var month = await MonthService.GetByMonthAndYearId(date.Month, year.Id);
                if (month == null)
                    month = await MonthService.Save(new MonthModel() { Month = date.Month, YearId = year.Id });

                var day = await DayService.GetByDayAndMonthId(date.Day, month.Id);
                if (day == null)
                    day = await DayService.Save(new DayModel() { Day = date.Day, MonthId = month.Id, Date = date });

                _event.DayId = day.Id;
                var eventId = await EventService.Save(_event);
                return eventId;
            }
            else return 0;
        }


        public async Task<List<DataWrapper>> GetEvents(DateTime selectedDate)
        {
            List<DataWrapper> response = new List<DataWrapper>();
            //var events = await EventService.GetAll();
            //foreach (var item in events)
            //{
            //    var day = await DayService.GetById(item.DayId);
            //}
            var year = await YearService.GetByYear(selectedDate.Year);
            if (year != null)
            {
                var prevMonth = await MonthService.GetByMonthAndYearId(selectedDate.AddMonths(-1).Month, year.Id);
                if (prevMonth != null)
                {
                    var days = await DayService.GetLasyDaysByMonthId(prevMonth.Id);
                    foreach (var day in days)
                    {
                        var events = await EventService.GetAllByDayId(day.Id);
                        if (events.Count > 0)
                            response.Add(new DataWrapper(day, events));
                    }
                }
                var nextMonth = await MonthService.GetByMonthAndYearId(selectedDate.AddMonths(1).Month, year.Id);
                if (nextMonth != null)
                {
                    var days = await DayService.GetFirstDaysByMonthId(nextMonth.Id);
                    foreach (var day in days)
                    {
                        var events = await EventService.GetAllByDayId(day.Id);
                        if (events.Count > 0)
                            response.Add(new DataWrapper(day, events));
                    }
                }
                var month = await MonthService.GetByMonthAndYearId(selectedDate.Month, year.Id);
                if (month != null)
                {
                    var days = await DayService.GetByMonthId(month.Id);
                    foreach (var day in days)
                    {
                        var events = await EventService.GetAllByDayId(day.Id);
                        if (events.Count > 0)
                            response.Add(new DataWrapper(day, events));
                    }
                }
            }

            return response;
        }
    }


    public class DataWrapper
    {
        public DataWrapper(DayModel day, List<Event> events)
        {
            Day = day;
            Events = events;
        }
        public DataWrapper()
        {

        }

        public DayModel Day { get; set; }
        public List<Event> Events { get; set; }
    }
}
