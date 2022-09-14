using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[Controller]/[action]")]
    public class Ger3ahController : ControllerBase
    {
        private readonly Ger3ahContext _context;
        public Ger3ahController(Ger3ahContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllGer3ahNames(){
            
            var Ger3ahNames = await _context.Ger3ahNames.ToListAsync();
            if (Ger3ahNames.Count() < 1)
            {
                return BadRequest("thier are no more names in the Ger3ah");
            }
            return Ok(Ger3ahNames);
        }

        [HttpGet]
        public ActionResult<List<User>> GetGer3ahHestory(string name){
            if (name != null) //need more checke if the name is waitspase
            {
                var logForTheEnterdName = _context.Ger3ahLogs.Where(n => n.PickerName == name).ToList();
                return Ok(logForTheEnterdName);
            }
            return BadRequest("thier in no log for u name");
        }

        [HttpGet]
        public string NamePicker(){
            return "this will pick a name and return 200";
        } 
        
    }
}