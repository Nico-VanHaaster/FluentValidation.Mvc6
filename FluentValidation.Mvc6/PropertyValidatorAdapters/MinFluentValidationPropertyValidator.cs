

namespace FluentValidation.Mvc6.PropertyValidatorAdapters
{
    using Internal;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
    using System;
    using System.Globalization;
    using Validators;
    internal class MinFluentValidationPropertyValidator : AbstractComparisonFluentValidationPropertyValidator<GreaterThanOrEqualValidator>
    {
        #region cTor
        /// <summary>
        /// Min Fluient validation property validator
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="propertyDescription"></param>
        /// <param name="validator"></param>
        public MinFluentValidationPropertyValidator(ModelMetadata metadata, PropertyRule propertyDescription, IPropertyValidator validator)
            : base(metadata, propertyDescription, validator)
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
        /// Adds the client validation for the minimum rule
        /// </summary>
        /// <param name="context"></param>
        protected override void AddClientValidation(ClientModelValidationContext context)
        {
            var formatter = new MessageFormatter().AppendPropertyName(Rule.GetDisplayName());
            var message = formatter.BuildMessage(Validator.ErrorMessageSource.GetString());

            string minValue = Convert.ToString(MaxValue, CultureInfo.InvariantCulture);
            if(!string.IsNullOrWhiteSpace(minValue))
            {
                MergeClientAttribute(context, "data-val", "true");
                MergeClientAttribute(context, "data-val-range", message);
                MergeClientAttribute(context, "data-val-range-min", minValue);
            }
        }

        
        #endregion
    }
}
