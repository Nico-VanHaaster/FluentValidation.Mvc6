
namespace FluentValidation.Mvc6.PropertyValidatorAdapters
{
    using Internal;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
    using System;
    using System.Globalization;
    using Validators;
    using Resources;

    internal class RangeFluentValidationPropertyValidator : FluentValidationPropertyValidator
    {
        /// <summary>
        /// Range Fluent Valition Property Validator
        /// </summary>
        /// <param name="metaData"></param>
        /// <param name="rule"></param>
        /// <param name="validator"></param>
        public RangeFluentValidationPropertyValidator(ModelMetadata metaData, PropertyRule rule, IPropertyValidator validator) : base(metaData, rule, validator)
        {
            ShouldValidate = true;
        }

        /// <summary>
        /// Gets the range valitor
        /// </summary>
        InclusiveBetweenValidator RangeValidator => (InclusiveBetweenValidator)Validator;
       
        /// <summary>
        /// Adds the client valiation
        /// </summary>
        /// <param name="context"></param>
        protected override void AddClientValidation(ClientModelValidationContext context)
        {
            var formatter = new MessageFormatter()
                .AppendPropertyName(Rule.GetDisplayName())
                .AppendArgument("From", RangeValidator.From)
                .AppendArgument("To", RangeValidator.To);

            string message = RangeValidator.ErrorMessageSource.GetString();

            if (RangeValidator.ErrorMessageSource.ResourceType == typeof(Messages))
            {
                // If we're using the default resources then the mesage for length errors will have two parts, eg:
                // '{PropertyName}' must be between {From} and {To}. You entered {Value}.
                // We can't include the "Value" part of the message because this information isn't available at the time the message is constructed.
                // Instead, we'll just strip this off by finding the index of the period that separates the two parts of the message.

                message = message.Substring(0, message.IndexOf(".") + 1);
            }

            message = formatter.BuildMessage(message);

            string max = Convert.ToString(RangeValidator.To, CultureInfo.InvariantCulture);
            string min = Convert.ToString(RangeValidator.From, CultureInfo.InvariantCulture);

            MergeClientAttribute(context, "data-val", "true");
            MergeClientAttribute(context, "data-val-range", message);
            if (!string.IsNullOrWhiteSpace(min))
            {
                MergeClientAttribute(context, "data-val-range-min", min);
            }
            if (!string.IsNullOrWhiteSpace(max))
            {
                MergeClientAttribute(context, "data-val-range-max", max);
            }

        }
    }
}
