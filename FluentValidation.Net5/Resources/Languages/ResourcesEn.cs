namespace FluentValidation.Resources.Languages
{
    internal class ResourcesEn : IResourceLanguage
    {
        public string CreditCardError
        {
            get
            {
                return "'{PropertyName}' is not a valid credit card number.";
            }
        }

        public string EmailError
        {
            get
            {
                return "'{PropertyName}' is not a valid email address.";
            }
        }

        public string EmptyError
        {
            get
            {
                return "'{PropertyName}' should be empty.";
            }
        }

        public string EqualError
        {
            get
            {
                return "'{PropertyName}' should be equal to '{ComparisonValue}'.";
            }
        }

        public string ExactLengthError
        {
            get
            {
                return "'{PropertyName}' must be {MaxLength} characters in length. You entered {TotalLength} characters.";
            }
        }

        public string ExclusiveBetweenError
        {
            get
            {
                return "'{PropertyName}' must be between {From} and {To} (exclusive). You entered {Value}.";
            }
        }

        public string GreaterThanError
        {
            get
            {
                return "'{PropertyName}' must be greater than '{ComparisonValue}'.";
            }
        }

        public string GreaterThanOrEqualError
        {
            get
            {
                return "'{PropertyName}' must be greater than or equal to '{ComparisonValue}'.";
            }
        }

        public string InclusiveBetweenError
        {
            get
            {
                return "'{PropertyName}' must be between {From} and {To}. You entered {Value}.";
            }
        }

        public string LengthError
        {
            get
            {
                return "'{PropertyName}' must be between {MinLength} and {MaxLength} characters. You entered {TotalLength} characters.";
            }
        }

        public string LessThanError
        {
            get
            {
                return "'{PropertyName}' must be less than '{ComparisonValue}'.";
            }
        }

        public string LessThanOrEqualError
        {
            get
            {
                return "'{PropertyName}' must be less than or equal to '{ComparisonValue}'.";
            }
        }

        public string NotEmptyError
        {
            get
            {
                return "'{PropertyName}' should not be empty.";
            }
        }

        public string NotEqualError
        {
            get
            {
                return "'{PropertyName}' should not be equal to '{ComparisonValue}'.";
            }
        }

        public string NotNullError
        {
            get
            {
                return "'{PropertyName}' must not be empty.";
            }
        }

        public string NullError
        {
            get
            {
                return "'{PropertyName}' must be empty.";
            }
        }

        public string PredicateError
        {
            get
            {
                return "The specified condition was not met for '{PropertyName}'.";
            }
        }

        public string RegexError
        {
            get
            {
                return "'{PropertyName}' is not in the correct format.";
            }
        }

        public string ScalePrecisionError
        {
            get
            {
                return "'{PropertyName}' may not be more than {expectedPrecision} digits in total, with allowance for {expectedScale} decimals. {digits} digits and {actualScale} decimals were found.";
            }
        }
    }
}
