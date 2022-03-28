using FluentValidation;
using UserApi.Constants;

namespace UserApi.Models
{
    public class UpdateUserModel
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class UpdateUserModelValidator : AbstractValidator<UpdateUserModel>
    {
        public UpdateUserModelValidator()
        {
            RuleFor(model => model.UserName).NotNull().NotEmpty().Matches(FieldConstants.EMAIL_PATTERN);
            RuleFor(model => model.FirstName).NotNull().NotEmpty().MaximumLength(FieldConstants.MAX_NAME_LEN).Must(fv => !string.IsNullOrWhiteSpace(fv));
            RuleFor(model => model.LastName).NotNull().NotEmpty().MaximumLength(FieldConstants.MAX_NAME_LEN).Must(fv => !string.IsNullOrWhiteSpace(fv));
        }
    }
}
