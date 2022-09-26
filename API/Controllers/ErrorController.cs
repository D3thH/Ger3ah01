using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Error;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    //[Route("errors/{code}")]
    public class ErrorController : ControllerBase
    {
        public IActionResult Eroor(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}