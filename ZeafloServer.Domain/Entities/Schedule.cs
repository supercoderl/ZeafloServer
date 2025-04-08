using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Entities
{
    public class Schedule : Entity<Guid>
    {
        [Column("schedule_id")]
        public Guid ScheduleId { get; private set; }

        [Column("user_id")]
        public Guid UserId { get; private set; }

        [Column("city_id")]
        public Guid CityId { get; private set; }

        [Column("title")]
        public string Title { get; private set; }

        [Column("trip_duration_id")]
        public Guid TripDurationId { get; private set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; private set; }

        [Column("note")]
        public string? Note { get; private set; }

        [ForeignKey("UserId")]
        [InverseProperty("Schedules")]
        public virtual User? User { get; set; }

        [ForeignKey("CityId")]
        [InverseProperty("Schedules")]
        public virtual City? City { get; set; }

        [ForeignKey("TripDurationId")]
        [InverseProperty("Schedules")]
        public virtual TripDuration? TripDuration { get; set; }

        public Schedule(
            Guid scheduleId,
            Guid userId,
            Guid cityId,
            string title,
            Guid tripDurationId,
            DateTime createdAt,
            string? note
        ) : base(scheduleId)
        {
            ScheduleId = scheduleId;
            UserId = userId;
            CityId = cityId;
            Title = title;
            TripDurationId = tripDurationId;
            CreatedAt = createdAt;
            Note = note;
        }

        public void SetUserId(Guid userId) { UserId = userId; }
        public void SetCityId(Guid cityId) { CityId = cityId; }
        public void SetTitle(string title) { Title = title; }
        public void SetTripDurationId(Guid tripDurationId) { TripDurationId = tripDurationId; }
        public void SetCreatedAt(DateTime createdAt) { CreatedAt = createdAt; }
        public void SetNote(string? note) { Note = note; }
    }
}
