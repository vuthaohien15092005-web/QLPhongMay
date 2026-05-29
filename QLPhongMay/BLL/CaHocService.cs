using System;
using System.Collections.Generic;
using QLPhongMay.DAL;
using QLPhongMay.Models;

namespace QLPhongMay.BLL
{
    public class CaHocService
    {
        private readonly CaHocRepository repository;

        public CaHocService()
            : this(new CaHocRepository())
        {
        }

        public CaHocService(CaHocRepository repository)
        {
            this.repository = repository;
        }

        public List<CaHoc> GetAll()
        {
            return this.repository.GetAll();
        }

        public CaHoc GetById(int maCa)
        {
            return this.repository.GetById(maCa);
        }

        public void Create(string tenCa, TimeSpan gioBatDau, TimeSpan gioKetThuc)
        {
            Validate(tenCa, gioBatDau, gioKetThuc);
            this.repository.Create(new CaHoc
            {
                TenCa = tenCa.Trim(),
                GioBatDau = gioBatDau,
                GioKetThuc = gioKetThuc
            });
        }

        public bool Update(int maCa, string tenCa, TimeSpan gioBatDau, TimeSpan gioKetThuc)
        {
            if (maCa <= 0)
            {
                throw new ArgumentException("Mã ca không hợp lệ.");
            }

            Validate(tenCa, gioBatDau, gioKetThuc);
            return this.repository.Update(new CaHoc
            {
                MaCa = maCa,
                TenCa = tenCa.Trim(),
                GioBatDau = gioBatDau,
                GioKetThuc = gioKetThuc
            });
        }

        public bool Delete(int maCa)
        {
            if (maCa <= 0)
            {
                throw new ArgumentException("Mã ca không hợp lệ.");
            }

            if (this.repository.IsUsedInSchedule(maCa))
            {
                throw new InvalidOperationException("Không thể xóa ca học đang được sử dụng trong lịch thực hành.");
            }

            return this.repository.Delete(maCa);
        }

        private static void Validate(string tenCa, TimeSpan gioBatDau, TimeSpan gioKetThuc)
        {
            if (string.IsNullOrWhiteSpace(tenCa))
            {
                throw new ArgumentException("Tên ca không được để trống.");
            }

            if (gioBatDau == gioKetThuc)
            {
                throw new ArgumentException("Giờ kết thúc phải khác giờ bắt đầu.");
            }
        }
    }
}
