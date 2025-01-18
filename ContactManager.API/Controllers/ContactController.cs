using Microsoft.AspNetCore.Mvc;
using ContactManager.Domain;

namespace ContactManagerAPI.Controllers
{
	
	[ApiController]
	[Route("api/contacts")]
	public class ContactController : ControllerBase
	{

		// GET api/contacts/getallcontacts
		[HttpGet("getallcontacts")]
		public ActionResult<IEnumerable<Contact>> GetAllContacts()
		{
			// Implementation would go here
			return Ok(new List<Contact>()); // Placeholder return
		}

		// GET api/contacts/getcontactbyid
		[HttpGet("getcontactbyid")]
		public ActionResult<Contact> GetContactById(string id)
		{
			// Implementation would go here
			return Ok(new Contact { Id = id }); // Placeholder return
		}

		// POST api/contacts/createcontact
		[HttpPost("createcontact")]
		public ActionResult<Contact> CreateContact([FromBody] Contact contact)
		{
			// Implementation would go here
			return CreatedAtAction(nameof(GetContactById), new { id = contact.Id }, contact);
		}

		// PUT api/contacts/updatecontact
		[HttpPut("updatecontact")]
		public ActionResult UpdateContact([FromBody] Contact contact)
		{
			// Implementation would go here
			return NoContent(); // Assuming update was successful
		}
	}
}