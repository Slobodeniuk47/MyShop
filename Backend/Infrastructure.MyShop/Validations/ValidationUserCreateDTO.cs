using FluentValidation;
using Infrastructure.MyShop.Models.DTO.AccountDTO;

namespace Infrastructure.MyShop.Validations
{
    //Initialized in Program.cs
    //is not working!
    public class ValidationUserCreateDTO : AbstractValidator<UserCreateDTO>
    {
        public ValidationUserCreateDTO() 
        {
            RuleFor(c => c.Password == c.ConfirmPassword)
                .NotEmpty()
                .WithMessage("Passwords do not match");
            RuleFor(c => c.Image)
                .NotEmpty()
                 .WithMessage("Select a photo for the category");
        }
    }
}
