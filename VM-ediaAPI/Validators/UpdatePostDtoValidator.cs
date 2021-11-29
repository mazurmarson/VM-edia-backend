using System.Linq;
using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using VM_ediaAPI.Data;
using VM_ediaAPI.Dtos;

namespace VM_ediaAPI.Validators
{
    public class UpdatePostDtoValidator : AbstractValidator<JsonPatchDocument<UpdatePostDto>>
    {
        public UpdatePostDtoValidator(DataContext dataContext)
        {
            RuleFor(x=> x.Operations).Custom((values, context) => {
                var NotValidPath = values.Where(x => x.path != "/description").Any();
                if(NotValidPath)
                {
                    context.AddFailure("Wrong property", "Nie ma takiej właściwości");
                }
                foreach(var value in values)
                {
                    if(value.value.ToString().Length > 200)
                    {
                        context.AddFailure("/description", "Wprowadzony opis jest dłuższy niż 250 znaków");
                    }
                }
            });
        }
    }
}