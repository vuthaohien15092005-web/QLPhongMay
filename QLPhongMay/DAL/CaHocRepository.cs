using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using QLPhongMay.Models;

namespace QLPhongMay.DAL
{
    public class CaHocRepository
    {
        public List<CaHoc> GetAll()
        {
            using (AppDbContext db = new AppDbContext())
            {
                return db.CaHocs.AsNoTracking()
                    .OrderBy(item => item.GioBatDau)
                    .ThenBy(item => item.MaCa)
                    .ToList();
            }
        }

        public CaHoc GetById(int maCa)
        {
            using (AppDbContext db = new AppDbContext())
            {
                return db.CaHocs.FirstOrDefault(item => item.MaCa == maCa);
            }
        }

        public void Create(CaHoc caHoc)
        {
            using (AppDbContext db = new AppDbContext())
            {
                db.CaHocs.Add(caHoc);
                db.SaveChanges();
            }
        }

        public bool Update(CaHoc caHoc)
        {
            using (AppDbContext db = new AppDbContext())
            {
                CaHoc existing = db.CaHocs.FirstOrDefault(item => item.MaCa == caHoc.MaCa);
                if (existing == null)
                {
                    return false;
                }

                existing.TenCa = caHoc.TenCa;
                existing.GioBatDau = caHoc.GioBatDau;
                existing.GioKetThuc = caHoc.GioKetThuc;
                db.SaveChanges();
                return true;
            }
        }

        public bool Delete(int maCa)
        {
            using (AppDbContext db = new AppDbContext())
            {
                CaHoc existing = db.CaHocs.FirstOrDefault(item => item.MaCa == maCa);
                if (existing == null)
                {
                    return false;
                }

                db.CaHocs.Remove(existing);
                db.SaveChanges();
                return true;
            }
        }

        public bool IsUsedInSchedule(int maCa)
        {
            using (AppDbContext db = new AppDbContext())
            {
                return db.LichThucHanhs.Any(item => item.MaCa == maCa);
            }
        }
    }
}
