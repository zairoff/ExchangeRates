using ExchangeRates.Contracts;
using FluentValidation;

namespace ExchangeRates.Api.Validators
{
    public class ConvertArgsValidator : AbstractValidator<ConvertArgs>
    {
        public ConvertArgsValidator()
        {
            this.RuleFor(x => x.Value)
                .NotEmpty()
                .GreaterThan(0);

            this.RuleFor(x => x.From)
                .NotEmpty()
                .Length(3);

            this.RuleFor(x => x.To)
                .NotEmpty()
                .Length(3);
        }
    }
}
