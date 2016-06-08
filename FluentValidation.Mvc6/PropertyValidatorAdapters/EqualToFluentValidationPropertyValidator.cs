namespace FluentValidation.Mvc6.PropertyValidatorAdapters
{
    using Internal;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
    using System.Reflection;
    using Validators;

    //using System.ComponentModel.DataAnnotations;

    internal class EqualToFluentValidationPropertyValidator : FluentValidationPropertyValidator
    {
        public EqualToFluentValidationPropertyValidator(ModelMetadata metaData, PropertyRule rule, IPropertyValidator validator)
            : base(metaData, rule, validator)
        {
            ShouldValidate = false;
        }

        /// <summary>
        /// Gets the equal validator
        /// </summary>
        protected EqualValidator EqualValidator => Validator as EqualValidator;

        /// <summary>
        /// Add client validation
        /// </summary>
        /// <param name="context"></param>
        protected override void AddClientValidation(ClientModelValidationContext context)
        {
            var propertyToCompare = EqualValidator.MemberToCompare as PropertyInfo;
            if (propertyToCompare != null)
            {
                // If propertyToCompare is not null then we're comparing to another property.
                // If propertyToCompare is null then we're either comparing against a literal value, a field or a method call.
                // We only care about property comparisons in this case.

                var comparisonDisplayName = ValidatorOptions.DisplayNameResolver(Rule.TypeToValidate, propertyToCompare, null) ?? propertyToCompare.Name.SplitPascalCase();

                var formatter = new MessageFormatter()
                    .AppendPropertyName(Rule.GetDisplayName())
                    .AppendArgument("ComparisonValue", comparisonDisplayName);

                string message = formatter.BuildMessage(EqualValidator.ErrorMessageSource.GetString());


                MergeClientAttribute(context, "data-val", "true");
                MergeClientAttribute(context, "data-val-equalto", message);
                MergeClientAttribute(context, "data-val-equalto-other", propertyToCompare.Name);

            }
        }
    }
}
