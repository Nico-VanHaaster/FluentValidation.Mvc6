#region License
// Copyright (c) Jeremy Skinner (http://www.jeremyskinner.co.uk)
// 
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
// 
// http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License.
// 
// The latest version of this file can be found at https://github.com/jeremyskinner/FluentValidation
#endregion

using System.Threading;

namespace FluentValidation.Validators
{
    using Resources;
    using Results;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public abstract class PropertyValidator : IPropertyValidator
    {
        private readonly List<Func<object, object, object>> customFormatArgs = new List<Func<object, object, object>>();
        private IStringSource errorSource;
        private IStringSource originalErrorSource;
        private IStringSource errorCodeSource;
        private IStringSource originalErrorCodeSource;

        public virtual bool IsAsync
        {
            get { return false; }
        }

        public Func<object, object> CustomStateProvider { get; set; }

        public ICollection<Func<object, object, object>> CustomMessageFormatArguments
        {
            get { return customFormatArgs; }
        }

        protected PropertyValidator(string errorMessageResourceName, Type errorMessageResourceType)
        {
            originalErrorSource = errorSource = new LocalizedStringSource(errorMessageResourceType, errorMessageResourceName, new FallbackAwareResourceAccessorBuilder());
        }

        protected PropertyValidator(string errorMessage)
        {
            originalErrorSource = errorSource = new StaticStringSource(errorMessage);
        }

        protected PropertyValidator(Expression<Func<string>> errorMessageResourceSelector)
        {
            originalErrorSource = errorSource = LocalizedStringSource.CreateFromExpression(errorMessageResourceSelector, new FallbackAwareResourceAccessorBuilder());
        }

        public IStringSource ErrorMessageSource
        {
            get { return errorSource; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                errorSource = value;

                if (value is LocalizedStringSource)
                {
                    originalErrorSource = value;
                }
            }
        }

        public virtual IEnumerable<ValidationFailure> Validate(PropertyValidatorContext context)
        {
            context.MessageFormatter.AppendPropertyName(context.PropertyDescription);
            context.MessageFormatter.AppendArgument("PropertyValue", context.PropertyValue);

            if (!IsValid(context))
            {
                return new[] { CreateValidationError(context) };
            }

            return Enumerable.Empty<ValidationFailure>();
        }

        public virtual Task<IEnumerable<ValidationFailure>> ValidateAsync(PropertyValidatorContext context, CancellationToken cancellation)
        {
            context.MessageFormatter.AppendPropertyName(context.PropertyDescription);
            context.MessageFormatter.AppendArgument("PropertyValue", context.PropertyValue);

            return
                IsValidAsync(context, cancellation)
                .Then(
                    valid => valid ? Enumerable.Empty<ValidationFailure>() : new[] { CreateValidationError(context) }.AsEnumerable(),
                    runSynchronously: true
                );
        }

        protected abstract bool IsValid(PropertyValidatorContext context);

        protected virtual Task<bool> IsValidAsync(PropertyValidatorContext context, CancellationToken cancellation)
        {
            return TaskHelpers.FromResult(IsValid(context));
        }

        /// <summary>
        /// Creates an error validation result for this validator.
        /// </summary>
        /// <param name="context">The validator context</param>
        /// <returns>Returns an error validation result.</returns>
        protected virtual ValidationFailure CreateValidationError(PropertyValidatorContext context)
        {
            Func<PropertyValidatorContext, string> errorBuilder = context.Rule.MessageBuilder ?? BuildErrorMessage;
            var error = errorBuilder(context);

            var failure = new ValidationFailure(context.PropertyName, error, context.PropertyValue);
            failure.FormattedMessageArguments = context.MessageFormatter.AdditionalArguments;
            failure.FormattedMessagePlaceholderValues = context.MessageFormatter.PlaceholderValues;
            failure.ResourceName = errorSource.ResourceName;
            failure.ErrorCode = (errorCodeSource != null) ? errorCodeSource.GetString() : originalErrorSource.ResourceName;

            if (CustomStateProvider != null)
            {
                failure.CustomState = CustomStateProvider(context.Instance);
            }

            return failure;
        }

        string BuildErrorMessage(PropertyValidatorContext context)
        {
            context.MessageFormatter.AppendAdditionalArguments(
                customFormatArgs.Select(func => func(context.Instance, context.PropertyValue)).ToArray()
                );

            string error = context.MessageFormatter.BuildMessage(errorSource.GetString());
            return error;
        }

        public IStringSource ErrorCodeSource
        {
            get { return errorCodeSource; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                errorCodeSource = value;
            }
        }
    }
}