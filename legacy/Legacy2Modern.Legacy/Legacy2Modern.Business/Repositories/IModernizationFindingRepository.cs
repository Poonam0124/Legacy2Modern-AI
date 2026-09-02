using System.Collections.Generic;
using Legacy2Modern.Business.Models;

namespace Legacy2Modern.Business.Repositories
{
    public interface IModernizationFindingRepository
    {
        List<ModernizationFinding> GetAll();
    }
}