
namespace FluentValidation.Mvc6.PropertyValidatorAdapters
{
    using Validators;
    using Internal;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
    using System;
    using System.Globalization;

    internal abstract class AbstractComparisonFluentValidationPropertyValidator<TValidator> : FluentValidationPropertyValidator
        where TValidator : AbstractComparisonValidator
    {
        /// <summary>
        /// Abstract comparizone fluent property validator
        /// </summary>
        /// <param name="metaData"></param>
        /// <param name="rule"></param>
        /// <param name="validator"></param>
        public AbstractComparisonFluentValidationPropertyValidator(ModelMetadata metaData, PropertyRule rule, IPropertyValidator validator) :
            base(metaData, rule, validator)
        {
            ShouldValidate = false;
        }

        /// <summary>
        /// Gets the min value
        /// </summary>
        protected abstract object MinValue { get; }

        /// <summary>
        /// Gets the maximum value
        /// </summary>
        protected abstract object MaxValue { get; }

        /// <summary>
        /// Gets the abstract comparison validator
        /// </summary>
        protected TValidator AbstractComparisonValidator => (TValidator)Validator;

        /// <summary>
        /// Adds a client validation rule
        /// </summary>
        /// <param name="context"></param>
        protected override void AddClientValidation(ClientModelValidationContext context)
        {
            var formatter = new MessageFormatter().AppendPropertyName(Rule.GetDisplayName());
            var message = formatter.BuildMessage(Validator.ErrorMessageSource.GetString());

            string max = Convert.ToString(MaxValue, CultureInfo.InvariantCulture);
            string min = Convert.ToString(MinValue, CultureInfo.InvariantCulture);

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