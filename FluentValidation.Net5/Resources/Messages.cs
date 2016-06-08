using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace FluentValidation.Resources
{

    public static class Messages
    {
        class CurrentlyLoadedLanguage
        {
            public IResourceLanguage Language { get; set; }

            public string CultureId { get; set; }
        }

        internal delegate IResourceLanguage ResourceLanguageFactory();

        static CurrentlyLoadedLanguage _currentLanguage = null;


        internal static IResourceLanguage CurrentLanguage
        {
            get
            {
                string cultureId = defaultCultureId;
#if netcore
                cultureId = CultureInfo.CurrentCulture.Name.ToLowerInvariant();
#else
                cultureId = Thread.CurrentThread.CurrentCulture.Name.ToLower();
#endif
                if (_currentLanguage != null && _currentLanguage.CultureId.Equals(cultureId, StringComparison.OrdinalIgnoreCase))
                    return _currentLanguage.Language;

                _currentLanguage = new CurrentlyLoadedLanguage
                {
                    Language = loadLanguageResources(),
                    CultureId = cultureId
                };

                return _currentLanguage.Language;
            }
        }

        static string defaultCultureId = "en";

        static Dictionary<string, Lazy<IResourceLanguage>> languages = new Dictionary<string, Lazy<IResourceLanguage>>
        {
            { "en", new Lazy<IResourceLanguage>(() => new Languages.ResourcesEn()) }
        };

        public static CultureInfo DefaultCulture
        {
            get { return new CultureInfo(defaultCultureId); }
            set
            {
                if (value == null)
                    throw new NullReferenceException("value cannot be null");

                if (!languages.ContainsKey(value.Name.ToLowerInvariant()))
                    throw new Exception("The language selected could not be loaded.");
                _currentLanguage = null;
                defaultCultureId = value.Name;
            }
        }


        static IResourceLanguage loadLanguageResources()
        {
            string cultureId = defaultCultureId;
#if netcore
            cultureId = CultureInfo.CurrentCulture.Name.ToLowerInvariant();
#else
            cultureId = Thread.CurrentThread.CurrentCulture.Name.ToLower();
#endif
            if (languages.ContainsKey(cultureId))
                return languages[cultureId].Value;

            return languages[defaultCultureId].Value;
        }

        public static string CreditCardError { get { return CurrentLanguage.CreditCardError; } }

        public static string EmailError { get { return CurrentLanguage.EmailError; } }

        public static string EmptyError { get { return CurrentLanguage.EmptyError; } }

        public static string EqualError { get { return CurrentLanguage.EqualError; } }

        public static string ExactLengthError { get { return CurrentLanguage.ExactLengthError; } }

        public static string ExclusiveBetweenError { get { return CurrentLanguage.ExclusiveBetweenError; } }

        public static string GreaterThanError { get { return CurrentLanguage.GreaterThanError; } }

        public static string GreaterThanOrEqualError { get { return CurrentLanguage.GreaterThanOrEqualError; } }

        public static string InclusiveBetweenError { get { return CurrentLanguage.InclusiveBetweenError; } }

        public static string LengthError { get { return CurrentLanguage.LengthError; } }

        public static string LessThanError { get { return CurrentLanguage.LessThanError; } }

        public static string LessThanOrEqualError { get { return CurrentLanguage.LessThanOrEqualError; } }

        public static string NotEmptyError { get { return CurrentLanguage.NotEmptyError; } }

        public static string NotEqualError { get { return CurrentLanguage.NotEqualError; } }

        public static string NotNullError { get { return CurrentLanguage.NotNullError; } }

        public static string NullError { get { return CurrentLanguage.NullError; } }

        public static string PredicateError { get { return CurrentLanguage.PredicateError; } }

        public static string RegexError { get { return CurrentLanguage.RegexError; } }

        public static string ScalePrecisionError { get { return CurrentLanguage.ScalePrecisionError; } }

    }
}