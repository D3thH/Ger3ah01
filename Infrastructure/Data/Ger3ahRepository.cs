using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class Ger3ahRepository : IGer3ahRepository
    {
        private readonly Ger3ahContext _context;
        private readonly IMapper _mapper;
        public Ger3ahRepository(Ger3ahContext context,
        IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<Ger3ahName>> GetAllGer3ahNames()
        {
            var Ger3ahNames = await _context.Ger3ahNames.Where(x => !x.IsTaken).ToListAsync();
            return Ger3ahNames;

        }

        public async Task<List<Ger3ahLog>> GetGer3ahHestory(string name)
        {
            var searcherId = _context.Users.Where(x => x.NameAR == name).FirstOrDefault().Id;
            var Ger3ahNames = await _context.Ger3ahLogs.Where(x => x.UserId == searcherId).ToListAsync();
            //Ger3ahNames.Where(n => n.PickedName == name).ToList();

            return Ger3ahNames;
        }

        public Ger3ahOutputDto NamePicker(string name)
        {
            try
            {
                var Picker = _context.Users.Where(x => x.NameAR == name).FirstOrDefault();
                if (Picker == null)
                    return new Ger3ahOutputDto { Errors = "the name that was entered  does not exist in the system" };
                if (_context.PickerChrecer.Any(x => x.UserId == Picker.Id && x.IsAlreadyPickedName))
                    return new Ger3ahOutputDto { Errors = " the picer is Already Picked a Name so he cant Picke anther name" };
                var allNames = _context.Ger3ahNames.Include(x => x.User).Where(x => x.UserId != Picker.Id && !x.IsTaken).ToList();
                if (allNames.Count() == 0)
                    return new Ger3ahOutputDto { Errors = "There are no names at the ger3ah" };
                var PickedName = new Ger3ahName();
                Random R = new Random();
                PickedName = allNames.ElementAt(R.Next(0, allNames.Count()));
                // Ger3ahLog addingThePickerNameAndPickedNameToLog = new Ger3ahLog
                // {
                //     UserId = Picker.Id,
                //     PickerName = Picker.Name,
                //     PickedName = PickedName.name,
                //     CreatedDate = new DateTime()
                // };

                // var Ger3ahNamesNeededToUpdate = _context.Ger3ahNames.Where(x => x.UserId =؛= PickedName.UserId).FirstOrDefault();
                // Ger3ahNamesNeededToUpdate.IsTaken = true;

                // var PickerChrecerNeededToUpdate = _context.PickerChrecer.Where(x => x.UserId == Picker.Id).FirstOrDefault();
                // PickerChrecerNeededToUpdate.IsAlreadyPickedName = true;

                // _context.Ger3ahLogs.Add(addingThePickerNameAndPickedNameToLog);
                // _context.Ger3ahNames.Update(Ger3ahNamesNeededToUpdate);
                // _context.PickerChrecer.Update(PickerChrecerNeededToUpdate);
                
                return _mapper.Map<Ger3ahName, Ger3ahOutputDto>(PickedName);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}