using FluentValidation;

namespace EnergyHub.Application.Commands.Customer
{
    public class CreateCustomerValidator : AbstractValidator<CreateCustomerRequest>
    {
        public CreateCustomerValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.ClientId).NotEmpty();
            RuleFor(x => x.ClientSecret).NotEmpty();
            RuleFor(x => x.CreatedOn).NotNull();
            RuleFor(x => x.UpdatedOn).NotNull();
        }
    }
}
