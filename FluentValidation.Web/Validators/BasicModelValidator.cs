using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentValidation.Web.Validators
{
    public class BasicModelValidator : AbstractValidator<Models.BasicValidationModel>
    {
        public BasicModelValidator()
        {
            RuleFor(m => m.FullName)
                .NotEmpty()
                .WithMessage("The first name is required.");

            RuleFor(m => m.EmailAddress)
                .EmailAddress()
                .WithMessage("The email address does not appear to be valid");

            RuleFor(m => m.EmailAddress)
                .NotEmpty()
                .WithMessage("The email address is required.");

            RuleFor(m => m.CreditCard)
                .CreditCard()
                .WithMessage("The credit card is not valid");

            RuleFor(m => m.CreditCard)
                .NotEmpty()
                .WithMessage("The credit card is required.");


            RuleFor(m => m.Password)
               .NotEmpty()
               .WithMessage("Your password is required.");

            RuleFor(m => m.Password)
                .Length(4, 25)
                .WithMessage("Your password must be between 4 to 25 characters long.");

            RuleFor(m => m.ConfirmPassword)
                .Equal(m => m.Password)
                .WithMessage("Your passwords do not match");

            RuleFor(m => m.RegularExpression)
                .Matches("^[a-z0-9-]+$")
                .WithMessage("The text entered does not match the regular express ^[a-z0-9-]+$.");

            RuleFor(m => m.MinValue)
                .GreaterThanOrEqualTo(5)
                .WithMessage("The value must be at least 5.");

            RuleFor(m => m.MaxValue)
                .LessThanOrEqualTo(10)
                .WithMessage("The value must be less than 10.");

            RuleFor(m => m.Range)
                .InclusiveBetween(5, 10)
                .WithMessage("The value must be between 5 - 10.");


        }
    }
}
