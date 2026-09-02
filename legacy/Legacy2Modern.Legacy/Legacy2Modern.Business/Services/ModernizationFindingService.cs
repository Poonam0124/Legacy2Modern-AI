using System.Collections.Generic;
using Legacy2Modern.Business.Models;
using Legacy2Modern.Business.Repositories;

namespace Legacy2Modern.Business.Services
{
    public class ModernizationFindingService
    {
        private readonly IModernizationFindingRepository _repository;

        public ModernizationFindingService()
        {
            _repository = new ModernizationFindingRepository();
        }

        public List<ModernizationFinding> GetLegacyFindings()
        {
            return _repository.GetAll();
        }
    }
}