using FluentValidation.Internal;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace FluentValidation.Mvc6.PropertyValidatorAdapters
{
    internal class RequiredFluentValidationPropertyValidator : FluentValidationPropertyValidator
    {
        /// <summary>
        /// Required Fluent Validation Property Validator
        /// </summary>
        /// <param name="metaData"></param>
        /// <param name="rule"></param>
        /// <param name="validator"></param>
        public RequiredFluentValidationPropertyValidator(ModelMetadata metaData, PropertyRule rule, IPropertyValidator validator) :
            base(metaData, rule, validator)
        {
            bool isNonNullableValueType = !TypeAllowsNullValue(metaData.ModelType);
            // bool nullWasSpecified = model == null;


            ShouldValidate = true; //&& nullWasSpecified;
        }

        /// <summary>
        /// Adds client validation
        /// </summary>
        /// <param name="context"></param>
        protected override void AddClientValidation(ClientModelValidationContext context)
        {
            var formatter = new MessageFormatter().AppendPropertyName(Rule.GetDisplayName());
            var message = formatter.BuildMessage(Validator.ErrorMessageSource.GetString());

            MergeClientAttribute(context, "data-val", "true");
            MergeClientAttribute(context, "data-val-required", message);
        }
        
        /// <summary>
        /// Gets the is required flag
        /// </summary>
        public override bool IsRequired
        {
            get { return true; }
        }

    }
}
