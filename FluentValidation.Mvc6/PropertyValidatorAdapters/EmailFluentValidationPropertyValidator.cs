using FluentValidation.Internal;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;


namespace FluentValidation.Mvc6.PropertyValidatorAdapters
{
    internal class EmailFluentValidationPropertyValidator : FluentValidationPropertyValidator
    {
        #region Properties
        /// <summary>
        /// Gets the email validator
        /// </summary>
        protected IEmailValidator EmailValidator => (IEmailValidator)Validator;
        #endregion

        /// <summary>
        /// Email flient validation property validator
        /// </summary>
        /// <param name="metaData"></param>
        /// <param name="rule"></param>
        /// <param name="validator"></param>
        public EmailFluentValidationPropertyValidator(ModelMetadata metaData, PropertyRule rule, IPropertyValidator validator) 
            : base(metaData, rule, validator)
        {
            ShouldValidate = false;
        }

        /// <summary>
        /// Overrides the add client validation to add the email rule
        /// </summary>
        /// <param name="context"></param>
        protected override void AddClientValidation(ClientModelValidationContext context)
        {
            var formatter = new MessageFormatter().AppendPropertyName(Rule.GetDisplayName());
            string message = formatter.BuildMessage(EmailValidator.ErrorMessageSource.GetString());

            MergeClientAttribute(context, "data-val", "true");
            MergeClientAttribute(context, "data-val-email", message);

        }


    }
}
