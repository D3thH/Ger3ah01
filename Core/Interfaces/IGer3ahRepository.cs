using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IGer3ahRepository
    {
         Task<List<Ger3ahName>> GetAllGer3ahNames();
         Task<List<Ger3ahLog>> GetGer3ahHestory(string name);
         Ger3ahName NamePicker(string name);
    }
}