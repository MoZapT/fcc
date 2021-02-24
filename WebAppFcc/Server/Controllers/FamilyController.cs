using WebAppFcc.Shared.Viewmodels;
using WebAppFcc.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppFcc.Shared.Enums;
using WebAppFcc.Shared.Interfaces.ViewBuilders;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using WebAppFcc.Shared.Interfaces.Managers;
using System.Linq;
using Newtonsoft.Json;

namespace WebAppFcc.Server.Controllers
{
    [ApiController]
    [Authorize(Roles = "User")]
    [Route("[controller]")]
    //[Localization]
    //[PreLoadAction]
    public class FamilyController : ControllerBase
    {
        private readonly IFccManager _mgrFcc;

        public FamilyController(IFccManager mgrFcc)
        {
            _mgrFcc = mgrFcc;
        }

        [HttpPost("person/add/{id}")]
        public async Task<IActionResult> AddPerson(Person person)
        {
            return Ok(await _mgrFcc.CreatePerson(person));
        }

        [HttpPut("person/update/{id}")]
        public async Task<IActionResult> UpdatePerson(Person person)
        {
            return Ok(await _mgrFcc.UpdatePerson(person));
        }

        [HttpGet("person/get/{id}")]
        public async Task<IActionResult> GetPerson(Guid id)
        {
            return Ok(await _mgrFcc.GetPerson(id));
        }

        [HttpGet("person/get-list")]
        public async Task<IActionResult> GetPersonList()
        {
            return Ok(await _mgrFcc.GetPersonList());
        }

        [HttpDelete("person/delete/{id}")]
        public async Task<IActionResult> DeletePerson(Guid id)
        {
            return Ok(await _mgrFcc.DeletePerson(id));
        }
    }
}