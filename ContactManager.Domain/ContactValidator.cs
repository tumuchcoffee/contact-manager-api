using ContactManager.Domain;
using FluentValidation;

public class ContactValidator : AbstractValidator<Contact>
{
	public ContactValidator()
	{
		// Validate that FirstName is not null, empty, or whitespace
		RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.");
	}
}