using FluentValidation.Attributes;
using FluentValidation.Web.Validators;
using System.ComponentModel.DataAnnotations;

namespace FluentValidation.Web.Models
{
    /// <summary>
    /// Basic Validation model
    /// </summary>
    [Validator(typeof(BasicModelValidator))]
    public class BasicValidationModel
    {
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Display(Name = "Credit Card")]
        public string CreditCard { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Regular Expression")]
        public string RegularExpression { get; set; }

        [Display(Name = "Min Value")]
        public int MinValue { get; set; }

        [Display(Name = "Max Value")]
        public int MaxValue { get; set; }

        [Display(Name = "Value Range")]
        public int Range { get; set; }
    }
}
