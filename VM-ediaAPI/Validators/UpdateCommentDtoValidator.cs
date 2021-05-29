using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using VM_ediaAPI.Data;
using VM_ediaAPI.Dtos;

namespace VM_ediaAPI.Validators
{
    public class UpdateCommentDtoValidator : AbstractValidator<JsonPatchDocument<UpdateCommentDto>>
    {
        public UpdateCommentDtoValidator(DataContext dataContext)
        {
            RuleFor(x => x.Operations).Custom((values, context) => {
                foreach(var value in values)
                {
                    if(value.value.ToString().Length > 200)
                    {
                        context.AddFailure("/content", "Wprowadzony komentarz jest dłuższy niż 200 znaków");
                    }
                    if(value.value.ToString().Length < 2)
                    {
                        context.AddFailure("/content", "Wprowadzony komentarz jest krótszy niż 2 znaki");
                    }
                }
            });
        }
    }
}