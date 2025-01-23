using Microsoft.AspNetCore.Mvc;
using ContactManager.Domain;
using ContactManager.Service;
using ContactManagerAPI.Extensions;

namespace ContactManagerAPI.Controllers
{
	
	[ApiController]
	[Route("api/contacts")]
	public class ContactController : ControllerBase
	{
		private IContactService _contactService;

		public ContactController(IContactService contactService)
		{
			_contactService = contactService;
		}

		// GET api/contacts/getallcontacts
		[HttpGet("getallcontacts")]
		public async Task<ActionResult<GetAllResponse>> GetAllContacts()
		{
			var response = new GetAllResponse();

			try
			{
				response.Result = await _contactService.GetAllContactsAsync();
			}
			catch (Exception ex) 
			{ 
				response.HasError = true;
				response.Exception = ex.Message;
			}

			return Ok(response);
		}

		// GET api/contacts/getcontactbyid
		[HttpGet("getcontactbyid")]
		public async Task<ActionResult<GetByIdResponse>> GetContactById(string id)
		{
			var response = new GetByIdResponse();

			try
			{
				response.Result = await _contactService.GetContactByIdAsync(id);
			}
			catch (Exception ex)
			{
				response.HasError = true;
				response.Exception = ex;
			}

			return Ok(response);
		}

		// POST api/contacts/createcontact
		[HttpPost("createcontact")]
		public async Task<ActionResult<CreateResponse>> CreateContact([FromBody] Contact contact)
		{
			var response = new CreateResponse();

			try
			{
				response.Result = await _contactService.CreateContactAsync(contact);
			}
			catch (Exception ex)
			{
				response.HasError = true;
				response.Exception = ex;
			}

			return Ok(response);
		}

		// PUT api/contacts/updatecontact
		[HttpPut("updatecontact")]
		public async Task<ActionResult<UpdateResponse>> UpdateContact([FromBody] Contact contact)
		{
			var response = new UpdateResponse();

			try
			{
				response.Result = await _contactService.UpdateContactAsync(contact);
			}
			catch (Exception ex)
			{
				response.HasError = true;
				response.Exception = ex;
			}

			return Ok(response);
		}
	}
}