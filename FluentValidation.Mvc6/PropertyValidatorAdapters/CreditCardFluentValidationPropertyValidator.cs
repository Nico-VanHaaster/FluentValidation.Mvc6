

namespace FluentValidation.Mvc6.PropertyValidatorAdapters
{
    using Internal;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
    using Validators;

    /// <summary>
    /// Credit card fluent validation property validator
    /// </summary>
    internal class CreditCardFluentValidationPropertyValidator : FluentValidationPropertyValidator
    {
        public CreditCardFluentValidationPropertyValidator(ModelMetadata metaData, PropertyRule rule, IPropertyValidator validator) 
            : base(metaData, rule, validator)
        {
            ShouldValidate = true;
        }

        protected virtual CreditCardValidator CreditCardValidator => Validator as CreditCardValidator;

        protected override void AddClientValidation(ClientModelValidationContext context)
        {
            var formatter = new MessageFormatter().AppendPropertyName(Rule.GetDisplayName());
            string message = formatter.BuildMessage(CreditCardValidator.ErrorMessageSource.GetString());

            MergeClientAttribute(context, "data-val", "true");
            MergeClientAttribute(context, "data-val-creditcard", message);

        }
    }

}
