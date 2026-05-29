using System;
using System.Collections.Generic;
using QLPhongMay.DAL;
using QLPhongMay.DTO;

namespace QLPhongMay.BLL
{
    public class ComputerService
    {
        private readonly ComputerRepository repository;

        public ComputerService()
            : this(new ComputerRepository())
        {
        }

        public ComputerService(ComputerRepository repository)
        {
            this.repository = repository;
        }

        public List<ComputerListItem> GetComputerList()
        {
            return this.repository.GetComputerList();
        }

        public List<ComputerLookupItem> GetRoomLookup()
        {
            return this.repository.GetRoomLookup();
        }

        public List<ComputerLookupItem> GetRamLookup()
        {
            return this.repository.GetRamLookup();
        }

        public List<ComputerLookupItem> GetCpuLookup()
        {
            return this.repository.GetCpuLookup();
        }

        public List<ComputerLookupItem> GetMonitorLookup()
        {
            return this.repository.GetMonitorLookup();
        }

        public List<ComputerLookupItem> GetOperatingSystemLookup()
        {
            return this.repository.GetOperatingSystemLookup();
        }

        public void CreateComputer(ComputerEditItem item)
        {
            ValidateComputer(item, false);
            this.repository.CreateComputer(item);
        }

        public void UpdateComputer(ComputerEditItem item)
        {
            ValidateComputer(item, true);
            this.repository.UpdateComputer(item);
        }

        public void DeleteComputer(int maMay)
        {
            if (maMay <= 0)
            {
                throw new ArgumentException("Mã máy không hợp lệ.");
            }

            this.repository.DeleteComputer(maMay);
        }

        private static void ValidateComputer(ComputerEditItem item, bool requireId)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            if (requireId && item.MaMay <= 0)
            {
                throw new ArgumentException("Mã máy không hợp lệ.");
            }

            Require(item.TenMay, "Tên máy");
            Require(item.TinhTrang, "Tình trạng");
            if (item.MaPhong <= 0)
            {
                throw new ArgumentException("Vui lòng chọn phòng máy.");
            }

            Require(item.TenRam, "RAM");
            Require(item.TenCPU, "Bộ CPU");
            Require(item.TenManHinh, "Màn hình");
            Require(item.TenHDH, "Hệ điều hành");
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
