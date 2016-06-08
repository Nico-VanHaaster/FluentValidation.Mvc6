
namespace FluentValidation.Mvc6.PropertyValidatorAdapters
{
    using Internal;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
    using System;
    using System.Globalization;
    using Validators;
    internal class MaxFluentValidationPropertyValidator : AbstractComparisonFluentValidationPropertyValidator<LessThanOrEqualValidator>
    {
        #region cTor
        /// <summary>
        /// Max Fluent Validation Property Validator
        /// </summary>
        /// <param name="metaData"></param>
        /// <param name="rule"></param>
        /// <param name="validator"></param>
        public MaxFluentValidationPropertyValidator(ModelMetadata metaData, PropertyRule rule, IPropertyValidator validator)
            : base(metaData, rule, validator)
        {
            ShouldValidate = false;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the minimum value
        /// </summary>
        protected override object MinValue
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the maximum value
        /// </summary>
        protected override object MaxValue
        {
            get { return AbstractComparisonValidator.ValueToCompare; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adds a client validation rule
        /// </summary>
        /// <param name="context"></param>
        protected override void AddClientValidation(ClientModelValidationContext context)
        {
            var formatter = new MessageFormatter().AppendPropertyName(Rule.GetDisplayName());
            var message = formatter.BuildMessage(Validator.ErrorMessageSource.GetString());
            string minValue = Convert.ToString(MaxValue, CultureInfo.InvariantCulture);
            if (!string.IsNullOrWhiteSpace(minValue))
            {
                MergeClientAttribute(context, "data-val", "true");
                MergeClientAttribute(context, "data-val-range", message);
                MergeClientAttribute(context, "data-val-range-max", minValue);
            }
        }
        #endregion
    }
}
