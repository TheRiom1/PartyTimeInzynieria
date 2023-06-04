using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Data.Base;
using WebApplication3.Models;

namespace WebApplication3.Data.Services
{
   public interface IRoomsService:IEntityBaseRepository<Room>
    {
    }
}
