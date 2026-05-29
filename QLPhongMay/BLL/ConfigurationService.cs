using System;
using System.Collections.Generic;
using QLPhongMay.DAL;
using QLPhongMay.DTO;

namespace QLPhongMay.BLL
{
    public class ConfigurationService
    {
        private readonly ConfigurationRepository repository;

        public ConfigurationService()
            : this(new ConfigurationRepository())
        {
        }

        public ConfigurationService(ConfigurationRepository repository)
        {
            this.repository = repository;
        }

        public List<ConfigLookupItem> GetItems(ConfigCategory category)
        {
            return this.repository.GetItems(category);
        }

        public void AddItem(ConfigCategory category, string name)
        {
            ValidateName(name, GetTable(category).DisplayName);
            this.repository.AddItem(category, name);
        }

        public void UpdateItem(ConfigCategory category, int id, string name)
        {
            ValidateId(id);
            ValidateName(name, GetTable(category).DisplayName);
            this.repository.UpdateItem(category, id, name);
        }

        public void DeleteItem(ConfigCategory category, int id)
        {
            ValidateId(id);
            this.repository.DeleteItem(category, id);
        }

        public ConfigTable GetTable(ConfigCategory category)
        {
            return ConfigurationRepository.GetTable(category);
        }

        private static void ValidateId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Mã danh mục không hợp lệ.");
            }
        }

        private static void ValidateName(string name, string displayName)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(displayName + " không được để trống.");
            }
        }
    }
}
