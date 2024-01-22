using FluentValidation;
using Infrastructure.MyShop.Models.DTO.CategoryDTO;

namespace Web.MyShop.Validations
{
    public class ValidationCategoryCreateDTO : AbstractValidator<CategoryCreateDTO>
    {
        public ValidationCategoryCreateDTO()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Вкажіть назву категорії");
            RuleFor(c => c.Image)
                .NotEmpty()
                .WithMessage("Оберіть фото для категорії");
        }
    }
}
