using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IGer3ahRepository
    {
        Task<GetNumberOfPeboleInGer3ahDto> GetAllGer3ahNames();
        Task<List<GetTheHestoryOfTheGer3ahDto>> GetGer3ahHestory(string name);
        Ger3ahOutputDto NamePicker(string name, string email);
        void ReBuildTheGer3ah();
        RemvedNameStatus RemoveNameFromGer3ah(string name);
        void sendEnailToThePicer(string to, Ger3ahName PickedName,User Picker);
    }
}