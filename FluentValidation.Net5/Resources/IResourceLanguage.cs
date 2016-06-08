namespace FluentValidation.Resources
{
    public interface IResourceLanguage
    {
        string CreditCardError { get; }

        string EmailError { get; }

        string EmptyError { get; }

        string EqualError { get; }

        string ExactLengthError { get; }

        string ExclusiveBetweenError { get; }

        string GreaterThanError { get; }

        string GreaterThanOrEqualError { get; }

        string InclusiveBetweenError { get; }

        string LengthError { get; }

        string LessThanError { get; }

        string LessThanOrEqualError { get; }

        string NotEmptyError { get; }

        string NotEqualError { get; }

        string NotNullError { get; }

        string NullError { get; }

        string PredicateError { get; }

        string RegexError { get; }

        string ScalePrecisionError { get; }
    }
}
