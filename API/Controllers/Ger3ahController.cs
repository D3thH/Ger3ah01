using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Error;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[Controller]/[action]")]
    public class Ger3ahController : ControllerBase
    {
        private readonly IGer3ahRepository _repo;
        public Ger3ahController(IGer3ahRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Ger3ahName>>> GetAllGer3ahNames(){
            
            var Ger3ahNames = await _repo.GetAllGer3ahNames();
            if (Ger3ahNames.Count() < 1)
            {
                return BadRequest("thier are no more names in the Ger3ah");
            }
            return Ok(Ger3ahNames);
        }

        [HttpGet]
        public ActionResult<List<User>> GetGer3ahHestory(string name){
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrWhiteSpace(name)) //need more checke if the name is waitspase
            {
                var logForTheEnterdName = _repo.GetGer3ahHestory(name);
                return Ok(logForTheEnterdName);
            }
            return BadRequest("you have to enter a valed Name");
        }

        [HttpGet]
        public ActionResult NamePicker(string name){
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrWhiteSpace(name)) //need more checke if the name is waitspase
            {
                var logForTheEnterdName = _repo.NamePicker(name);
                return Ok(logForTheEnterdName);
            }
            return NotFound(new ApiResponse(400));
        } 
        
    }
}