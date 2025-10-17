using FluentValidation;

namespace IdentityServer.Application.User.Commands.CreateUser
{
    public class CreateSignupUserCommandValidator : AbstractValidator<CreateSignupUserCommand>
    {
        private string _roleName = string.Empty;
        public CreateSignupUserCommandValidator() 
        {
            RuleFor(v => v.Username).NotEmpty().WithMessage("مقدار نام کاربری الزامی می باشد");
            RuleFor(v => v.Password).NotEmpty().WithMessage("مقدار کلمه عبور الزامی می باشد");
            
        }
    }
}
