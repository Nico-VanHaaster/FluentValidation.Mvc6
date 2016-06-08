namespace FluentValidation.Mvc6.PropertyValidatorAdapters
{
    using Internal;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
    using Resources;
    using System;
    using System.Globalization;
    using Validators;

    public class StringLengthFluentValidationPropertyValidator : FluentValidationPropertyValidator
    {
        public StringLengthFluentValidationPropertyValidator(ModelMetadata metaData, PropertyRule rule, IPropertyValidator validator)
            : base(metaData, rule, validator)
        {
            ShouldValidate = false;
        }

        protected virtual ILengthValidator LengthValidator => (ILengthValidator)Validator;

        protected override void AddClientValidation(ClientModelValidationContext context)
        {
            string min = Convert.ToString(LengthValidator.Min, CultureInfo.InvariantCulture);
            string max = Convert.ToString(LengthValidator.Max, CultureInfo.InvariantCulture);

            var formatter = new MessageFormatter()
               .AppendPropertyName(Rule.GetDisplayName())
               .AppendArgument("MinLength", min)
               .AppendArgument("MaxLength", max);

            string message = LengthValidator.ErrorMessageSource.GetString();
            if (LengthValidator.ErrorMessageSource.ResourceType == typeof(Messages))
            {
                // If we're using the default resources then the mesage for length errors will have two parts, eg:
                // '{PropertyName}' must be between {MinLength} and {MaxLength} characters. You entered {TotalLength} characters.
                // We can't include the "TotalLength" part of the message because this information isn't available at the time the message is constructed.
                // Instead, we'll just strip this off by finding the index of the period that separates the two parts of the message.
                message = message.Substring(0, message.IndexOf(".") + 1);
            }

            message = formatter.BuildMessage(message);

            MergeClientAttribute(context, "data-val", "true");
            MergeClientAttribute(context, "data-val-length", message);
            if (!string.IsNullOrWhiteSpace(min))
                MergeClientAttribute(context, "data-val-length-min", min);
            if(!string.IsNullOrWhiteSpace(max))
                MergeClientAttribute(context, "data-val-length-max", max);
        }
    }
}
