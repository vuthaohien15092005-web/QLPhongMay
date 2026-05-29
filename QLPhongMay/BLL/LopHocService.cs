using System;
using System.Collections.Generic;
using QLPhongMay.DAL;
using QLPhongMay.Models;

namespace QLPhongMay.BLL
{
    public class LopHocService
    {
        private readonly LopHocRepository repository;

        public LopHocService()
            : this(new LopHocRepository())
        {
        }

        public LopHocService(LopHocRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<LopHoc> GetAll()
        {
            return this.repository.GetAll();
        }

        public LopHoc GetById(int maLop)
        {
            return this.repository.GetById(maLop);
        }

        public bool ExistsByTenLop(string tenLop, int? excludedMaLop = null)
        {
            return this.repository.ExistsByTenLop(tenLop, excludedMaLop);
        }

        public void Create(LopHoc lopHoc)
        {
            Validate(lopHoc, false);
            if (this.repository.ExistsByTenLop(lopHoc.TenLop))
            {
                throw new InvalidOperationException("Tên lớp đã tồn tại.");
            }

            this.repository.Create(lopHoc);
        }

        public void Update(LopHoc lopHoc)
        {
            Validate(lopHoc, true);
            if (this.repository.ExistsByTenLop(lopHoc.TenLop, lopHoc.MaLop))
            {
                throw new InvalidOperationException("Tên lớp đã tồn tại.");
            }

            this.repository.Update(lopHoc);
        }

        public void Delete(int maLop)
        {
            if (maLop <= 0)
            {
                throw new ArgumentException("Mã lớp không hợp lệ.");
            }

            this.repository.Delete(maLop);
        }

        private static void Validate(LopHoc lopHoc, bool requireId)
        {
            if (lopHoc == null)
            {
                throw new ArgumentNullException("lopHoc");
            }

            if (requireId && lopHoc.MaLop <= 0)
            {
                throw new ArgumentException("Mã lớp không hợp lệ.");
            }

            if (string.IsNullOrWhiteSpace(lopHoc.TenLop))
            {
                throw new ArgumentException("Tên lớp không được để trống.");
            }

            if (lopHoc.SiSo < 0)
            {
                throw new ArgumentException("Sĩ số không được âm.");
            }
        }
    }
}
