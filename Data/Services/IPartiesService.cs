using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Data.Base;
using WebApplication3.Data.ViewModels;
using WebApplication3.Models;

namespace WebApplication3.Data.Services
{
    public interface IPartiesService:IEntityBaseRepository<Party>
    {
        Task<Party> GetPartyByIdAsync(int id);
        Task<NewPartyDropdownsVM> GetNewPartyDropdownsValues();
        Task AddNewPartyAsync(NewPartyVM data);
       // decimal CalculateCost(NewPartyVM data);
        Task UpdatePartyAsync(NewPartyVM data);
    }
}
