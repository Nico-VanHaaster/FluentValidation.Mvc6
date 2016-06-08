using FluentValidation.Internal;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace FluentValidation.Mvc6.PropertyValidatorAdapters
{
    /// <summary>
    /// Basic fluent validation property validator
    /// </summary>
    public class FluentValidationPropertyValidator : IModelValidator, IClientModelValidator
    {
        #region cTor
        /// <summary>
        /// Base Fluent Property validator
        /// </summary>
        /// <param name="modelValidationContext"></param>
        /// <param name="rule"></param>
        /// <param name="validator"></param>
        public FluentValidationPropertyValidator(ModelMetadata metaData, PropertyRule rule, IPropertyValidator validator)
        {
            Validator = validator;

            // Build a new rule instead of the one passed in.
            // We do this as the rule passed in will not have the correct properties defined for standalone validation.
            // We also want to ensure we copy across the CustomPropertyName and RuleSet, if specified. 
            Rule = new PropertyRule(null, x => metaData.PropertyGetter, null, null, metaData.ModelType, null)
            {
                PropertyName = metaData.PropertyName,
                DisplayName = rule == null ? null : rule.DisplayName,
                RuleSet = rule == null ? null : rule.RuleSet
            };
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the is required flag
        /// </summary>
        public virtual bool IsRequired { get { return false; } }


        /// <summary>
        /// Gets the Property validator
        /// </summary>
        public IPropertyValidator Validator { get; private set; }

        /// <summary>
        /// Gets the property rule to validate
        /// </summary>
        public PropertyRule Rule { get; private set; }

        /*
		 This might seem a bit strange, but we do *not* want to do any work in these validators.
		 They should only be used for metadata purposes.
		 This is so that the validation can be left to the actual FluentValidationModelValidator.
		 The exception to this is the Required validator - these *do* need to run standalone
		 in order to bypass MVC's "A value is required" message which cannot be turned off.
		 Basically, this is all just to bypass the bad design in ASP.NET MVC. Boo, hiss. 
		*/
        protected bool ShouldValidate { get; set; }


        #endregion

        #region Methods

        /// <summary>
        /// Implements the RC2 Client validation context
        /// </summary>
        /// <param name="context"></param>
        /// <remarks>
        
        /// </remarks>
        public void AddValidation(ClientModelValidationContext context)
        {
            AddClientValidation(context);
        }

        /// <summary>
        /// Adds the client validation rules
        /// </summary>
        /// <param name="context"></param>
        protected virtual void AddClientValidation(ClientModelValidationContext context)
        {

            //todo: nothing.
        }

        /// <summary>
        /// Validates the model (Server side)
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext validationContext)
        {
            if (ShouldValidate)
            {
                var fakeRule = new PropertyRule(null, x => validationContext.Model, null, null, validationContext.ModelMetadata.ModelType, null)
                {
                    PropertyName = validationContext.ModelMetadata.PropertyName,
                    DisplayName = Rule == null ? null : Rule.DisplayName,
                };

                var fakeParentContext = new ValidationContext(validationContext.Container);
                var context = new PropertyValidatorContext(fakeParentContext, fakeRule, validationContext.ModelMetadata.PropertyName);
                var result = Validator.Validate(context);

                foreach (var failure in result)
                {
                    yield return new ModelValidationResult(validationContext.ModelMetadata.PropertyName, failure.ErrorMessage);
                }
            }
        }
        #endregion

        #region Helpers

        /// <summary>
        /// Helper to determine if type allows for null values
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected bool TypeAllowsNullValue(Type type)
        {
            return (!type.GetTypeInfo().IsValueType || Nullable.GetUnderlyingType(type) != null);
        }

        /// <summary>
        /// Helper to merge attributes for client validation
        /// </summary>
        /// <param name="attributes"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected bool MergeClientAttribute(ClientModelValidationContext context, string key, string value)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context), $"The parameter {context} is required.");

            var attributes = context.Attributes;
            if (attributes.ContainsKey(key))
            {
                return false;
            }
            attributes.Add(key, value);
            return true;
        }

        #endregion
    }
}
