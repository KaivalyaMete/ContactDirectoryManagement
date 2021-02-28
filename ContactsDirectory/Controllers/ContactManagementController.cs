using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ContactsDirectory.Repositories;
using DataAccessLayer;

namespace ContactsDirectory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactManagementController : ControllerBase
    {
        private readonly IContactManagement _contactManagement;
        public ContactManagementController(IContactManagement contactManagement)
        {
            _contactManagement = contactManagement;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllContacts()
        {
            try
            {
                return Ok(await _contactManagement.GetAllContactsData());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while retriving data from database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ContactInformation>> GetSingleContact(int Id)
        {
            try
            {
                var result = await _contactManagement.GetContact(Id);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while retriving data from database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ContactInformation>> CreateEmployee(ContactInformation contactInformation)
        {
            try
            {
                if (contactInformation == null)
                {
                    return BadRequest();
                }
                var CreatedContact = await _contactManagement.AddContact(contactInformation);
                return CreatedAtAction(nameof(GetSingleContact), new { id = CreatedContact.Id }, CreatedContact);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while retriving data from database");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ContactInformation>> UpdateContactData(int Id, ContactInformation contactInformation)
        {
            try
            {
                if (Id != contactInformation.Id)
                {
                    return BadRequest("Id is mismatched");
                }
                var ContactUpdateInfo = await _contactManagement.GetContact(Id);
                if (ContactUpdateInfo == null)
                {
                    return NotFound($"Employee Id= {Id} not found");
                }
                return await _contactManagement.UpdateContact(contactInformation);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while retriving data from database");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ContactInformation>> DeleteContactData(int Id)
        {
            try
            {
                var deleteContact = await _contactManagement.GetContact(Id);
                if (deleteContact == null)
                {
                    return NotFound($"Employee Id= {Id} not found");
                }
                return await _contactManagement.DeleteContact(Id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while retriving data from database");
            }
        }


    }
}
