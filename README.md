# FluentValidation.Mvc6

This project is based off the original [FluentValidation](https://github.com/JeremySkinner/FluentValidation) by [JeremySkinner](https://github.com/JeremySkinner).

This is a port to MVC6 RC2 working in both `netcoreapp1.0` and `net46`

The `FluentValidation.Mvc` project has a basic validation including jQuery unobtrusive validation.

## MVC6 RC2 Registration
This extension plugs directly into the MVC6 Validation framework with a single line of code in the `Startup.cs` file.

Example:
```
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc().AddFluentValidation();
}
```
The method `AddFluentValidation` allows for specifying a `FluentValidationModelValidatorProvider` and\or a custom `IValidatorFactory`


jQuery Validators:
- Required
- Range (min, max)
- String Length
- Credit Card
- Email Address
- Regular Expressions
- Equal comparer
