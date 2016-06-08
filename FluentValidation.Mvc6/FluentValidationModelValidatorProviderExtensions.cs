using System;
using Microsoft.Extensions.DependencyInjection;

namespace FluentValidation.Mvc6
{
    /// <summary>
    /// Static extensions class for the fluent validation model validator provider
    /// </summary>
    public static class FluentValidationModelValidatorProviderExtensions
    {
        /// <summary>
        /// Adds the fluent validation options to the MvcBuilder pipline.
        /// </summary>
        /// <param name="mvcBuilder"></param>
        /// <param name="configurationExpression"></param>
        /// <returns></returns>
        public static IMvcBuilder AddFluentValidation(this IMvcBuilder mvcBuilder, Action<FluentValidationModelValidatorProvider> configurationExpression = null)
        {
            return AddFluentValidation(mvcBuilder, null, configurationExpression);
        }

        /// <summary>
        /// Adds the fluent validation options to the MvcBuilder pipline.
        /// </summary>
        /// <param name="mvcBuilder"></param>
        /// <param name="configurationExpression"></param>
        /// <returns></returns>
        public static IMvcBuilder AddFluentValidation(this IMvcBuilder mvcBuilder, IValidatorFactory validatorFactory, Action<FluentValidationModelValidatorProvider> configurationExpression = null)
        {
            configurationExpression = configurationExpression ?? delegate { };

            var provider = new FluentValidationModelValidatorProvider(validatorFactory);
            configurationExpression(provider);



            //register the mvc optios
            mvcBuilder.AddMvcOptions(o =>
            {
                o.ModelValidatorProviders.Clear();
                o.ModelValidatorProviders.Add(provider);

            });
            

            //register the mvc view options
            mvcBuilder.AddViewOptions(o =>
            {
                o.ClientModelValidatorProviders.Clear();
                o.ClientModelValidatorProviders.Add(provider);

            });

            

            return mvcBuilder;
        }
    }
}
