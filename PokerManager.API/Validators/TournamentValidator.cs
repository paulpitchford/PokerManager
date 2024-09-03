using FluentValidation;
using PokerManager.API.Models;

namespace PokerManager.API.Validators
{
    public class TournamentValidator : AbstractValidator<Tournament>
    {
        public TournamentValidator()
        {
            RuleFor(t => t.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(t => t.StartDate)
                .NotEmpty().WithMessage("Start date is required.")
                .GreaterThan(DateTime.Now).WithMessage("Start date must be in the future.");

            RuleFor(t => t.MaxPlayers)
                .GreaterThan(1).WithMessage("Maximum players must be at least 2.")
                .LessThanOrEqualTo(1000).WithMessage("Maximum players cannot exceed 1000.");

            RuleFor(t => t.BuyIn)
                .GreaterThanOrEqualTo(0).WithMessage("Buy-in must be non-negative.");

            RuleFor(t => t.Status)
                .NotEmpty().WithMessage("Status is required.")
                .MaximumLength(20).WithMessage("Status must not exceed 20 characters.")
                .Must(BeAValidStatus).WithMessage("Status must be one of: Scheduled, InProgress, Completed, Cancelled");
        }

        private bool BeAValidStatus(string status)
        {
            return new[] { "Scheduled", "InProgress", "Completed", "Cancelled" }.Contains(status);
        }
    }
}