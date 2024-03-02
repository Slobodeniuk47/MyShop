using FluentValidation;
using Infrastructure.MyShop.Models.DTO.CategoryDTO;

namespace Infrastructure.MyShop.Validations
{
    //Initialized in Program.cs
    //is not working!
    public class ValidationCategoryCreateDTO : AbstractValidator<CategoryCreateDTO>
    {
        public ValidationCategoryCreateDTO()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Enter the name category");
            RuleFor(c => c.Image)
                .NotEmpty()
                .WithMessage("Select a photo for the category");
        }
    }
}
