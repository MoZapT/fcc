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

        [HttpPost("person/add/photo/{person}")]
        public async Task<IActionResult> AddPhoto(PersonPhoto photo)
        {
            try
            {
                return Ok(await _mgrFcc.CreatePersonPhoto(photo));

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("person/add/document/{person}")]
        public async Task<IActionResult> AddDocument(PersonDocument document)
        {
            try
            {
                return Ok(await _mgrFcc.CreatePersonDocument(document));

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("person/add/{person}")]
        public async Task<IActionResult> AddPerson(Person person)
        {
            try
            {
                return Ok(await _mgrFcc.CreatePerson(person));

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }        
        }

        [HttpPut("person/update/{person}")]
        public async Task<IActionResult> UpdatePerson(Person person)
        {
            try
            {
                return Ok(await _mgrFcc.UpdatePerson(person));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("person/get/{id}")]
        public async Task<IActionResult> GetPerson(Guid id)
        {
            try
            {
                return Ok(await _mgrFcc.GetPerson(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("person/get-list/{skip?}/{take?}")]
        public async Task<IActionResult> GetPersonList(int skip = 0, int take = 10)
        {
            try
            {
                return Ok(await _mgrFcc.GetPersonList(skip, take));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("person/get-count")]
        public async Task<IActionResult> GetPersonCount()
        {
            try
            {
                return Ok(await _mgrFcc.PersonCount());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("person/delete/{id}")]
        public async Task<IActionResult> DeletePerson(Guid id)
        {
            try
            {
                return Ok(await _mgrFcc.DeletePerson(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}