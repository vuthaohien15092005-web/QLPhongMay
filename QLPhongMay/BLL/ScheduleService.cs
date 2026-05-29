using System;
using System.Collections.Generic;
using QLPhongMay.DAL;
using QLPhongMay.DTO;

namespace QLPhongMay.BLL
{
    public class ScheduleService
    {
        private readonly ScheduleRepository repository;

        public ScheduleService()
            : this(new ScheduleRepository())
        {
        }

        public ScheduleService(ScheduleRepository repository)
        {
            this.repository = repository;
        }

        public List<ScheduleListItem> GetByFilter(DateTime? fromDate, DateTime? toDate, int? dayOfWeek, string roomId, string shiftId, string classId, string status)
        {
            return this.repository.GetByFilter(fromDate, toDate, dayOfWeek, roomId, shiftId, classId, status);
        }

        public ScheduleListItem GetById(string scheduleId)
        {
            return this.repository.GetById(scheduleId);
        }

        public bool Exists(string scheduleId)
        {
            return this.repository.Exists(scheduleId);
        }

        public bool HasScheduleConflict(string scheduleId, string roomId, string shiftId, DateTime date)
        {
            return this.repository.HasScheduleConflict(scheduleId, roomId, shiftId, date);
        }

        public List<RoomSuggestionItem> GetAvailableRooms(DateTime date, string shiftId, int studentCount, string scheduleId)
        {
            return this.repository.GetAvailableRooms(date, shiftId, studentCount, scheduleId);
        }

        public void Add(ScheduleListItem item)
        {
            Validate(item, false);
            this.repository.Add(item);
        }

        public void Update(ScheduleListItem item)
        {
            Validate(item, true);
            this.repository.Update(item);
        }

        public void Delete(string scheduleId)
        {
            if (string.IsNullOrWhiteSpace(scheduleId))
            {
                throw new ArgumentException("Mã lịch không hợp lệ.");
            }

            this.repository.Delete(scheduleId);
        }

        public List<LookupItem> GetRooms()
        {
            return this.repository.GetRooms();
        }

        public List<LookupItem> GetShifts()
        {
            return this.repository.GetShifts();
        }

        public List<LookupItem> GetClasses()
        {
            return this.repository.GetClasses();
        }

        public List<LookupItem> GetUsers()
        {
            return this.repository.GetUsers();
        }

        private static void Validate(ScheduleListItem item, bool requireId)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            if (requireId && string.IsNullOrWhiteSpace(item.MaLich))
            {
                throw new ArgumentException("Mã lịch không hợp lệ.");
            }

            Require(item.MaPhong, "Phòng máy");
            Require(item.MaCa, "Ca học");
            Require(item.MaLop, "Lớp học");
            Require(item.TrangThai, "Trạng thái");
            if (item.NgayThucHanh == DateTime.MinValue)
            {
                throw new ArgumentException("Ngày thực hành không hợp lệ.");
            }
        }

        private static void Require(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(fieldName + " không được để trống.");
            }
        }
    }
}
