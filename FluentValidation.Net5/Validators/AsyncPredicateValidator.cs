namespace FluentValidation.Validators
{
	using Internal;
	using Resources;
	using System;
	using System.Threading;
	using System.Threading.Tasks;

	public class AsyncPredicateValidator : AsyncValidatorBase
	{
		private readonly Func<object, object, PropertyValidatorContext, CancellationToken, Task<bool>> predicate;

		public AsyncPredicateValidator(Func<object, object, PropertyValidatorContext, CancellationToken, Task<bool>> predicate)
			: base(() => Messages.PredicateError)
		{
			predicate.Guard("A predicate must be specified.");
			this.predicate = predicate;
		}

		protected override Task<bool> IsValidAsync(PropertyValidatorContext context, CancellationToken cancellation)
		{
			return predicate(context.Instance, context.PropertyValue, context, cancellation);
		}
	}
}