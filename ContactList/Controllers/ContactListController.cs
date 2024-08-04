using Microsoft.AspNetCore.Mvc;
using ContactList.Business;
using Contactlist.Model;

namespace ContactList.Controllers
{
    [Route("api/[Controller]/[action]")]
    [ApiController]
    public class ContactListController : Controller
    {
        private readonly IContactService _contactService;

        public ContactListController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public IActionResult GetList()
        {
            var contactlist = _contactService.GetAllContacts();
            return Ok(contactlist);
        }

        [HttpGet("{id}")]
        public IActionResult GetbyId(int id)
        {
            var contact = _contactService.GetContactById(id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        [HttpPost]
        public IActionResult CreateList(Contact contact)
        {
            if (contact == null)
            {
                return BadRequest("Invalid contact Data");
            }
            _contactService.CreateContact(contact);
            return CreatedAtAction(nameof(GetbyId), new { id = contact.Id }, contact);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, Contact contact)
        {
            try
            {
                _contactService.UpdateContact(id, contact);
                return Ok("Contact edited successfully");
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            try
            {
                _contactService.DeleteContact(id);
                return Ok("Contact deleted successfully");
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] string? name, [FromQuery] string? email, [FromQuery] string? phoneNumber)
        {
            var result = _contactService.SearchContacts(name, email, phoneNumber);
            if (result == null || !result.Any())
            {
                return NotFound("Contacts not found");
            }
            return Ok(result);
        }
    }
}
