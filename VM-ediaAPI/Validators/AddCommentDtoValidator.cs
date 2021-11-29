using System.Linq;
using FluentValidation;
using VM_ediaAPI.Data;
using VM_ediaAPI.Dtos;

namespace VM_ediaAPI.Validators
{
    public class AddCommentDtoValidator : AbstractValidator<AddCommentDto>
    {
        public AddCommentDtoValidator(DataContext dataContext)
        {
            RuleFor(x => x.Content).NotEmpty().MinimumLength(2).MaximumLength(200);
            RuleFor(x => x.PostId).Custom((value, context) => {
                var postIsExist = dataContext.Posts.Any(x => x.Id == value);
                if(!postIsExist)
                {
                    context.AddFailure("PostId", "Podany post nie istnieje");
                }
            });
        }
    }
}