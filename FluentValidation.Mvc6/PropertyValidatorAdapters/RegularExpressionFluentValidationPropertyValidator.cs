

namespace FluentValidation.Mvc6.PropertyValidatorAdapters
{
    using Internal;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
    using Validators;

    /// <summary>
    /// Regular expression fluent validation property validator
    /// </summary>
    internal class RegularExpressionFluentValidationPropertyValidator : FluentValidationPropertyValidator
    {
        #region cTor
        /// <summary>
        /// regular expression fluent property validator
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="rule"></param>
        /// <param name="validator"></param>
        public RegularExpressionFluentValidationPropertyValidator(ModelMetadata metadata, PropertyRule rule, IPropertyValidator validator)
            : base(metadata, rule, validator)
        {
            ShouldValidate = false;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the regex core validator
        /// </summary>
        protected IRegularExpressionValidator RegexValidator => (IRegularExpressionValidator)Validator;
        #endregion

        #region Methods
        /// <summary>
        /// Adds the client validation
        /// </summary>
        /// <param name="context"></param>
        protected override void AddClientValidation(ClientModelValidationContext context)
        {
            var formatter = new MessageFormatter().AppendPropertyName(Rule.GetDisplayName());
            string message = formatter.BuildMessage(RegexValidator.ErrorMessageSource.GetString());

            MergeClientAttribute(context, "data-val", "true");
            MergeClientAttribute(context, "data-val-regex", message);
            MergeClientAttribute(context, "data-val-regex-pattern", RegexValidator.Expression);
        }
        #endregion
    }
}
