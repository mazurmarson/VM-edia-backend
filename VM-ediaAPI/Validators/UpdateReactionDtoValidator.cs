using System.Linq;
using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using VM_ediaAPI.Data;
using VM_ediaAPI.Dtos;

namespace VM_ediaAPI.Validators
{
    public class UpdateReactionDtoValidator : AbstractValidator<JsonPatchDocument<UpdateReactionDto>>
    {
        public UpdateReactionDtoValidator(DataContext dataContext)
        {
            RuleFor(x => x.Operations).Custom((values, context) =>
             {
                 var NotValidPathValue = values.Where(x => x.path != "/isPositive").Any();
                if (NotValidPathValue)
                     {
                     context.AddFailure("Wrong property", "Nie ma takiej właściwości");
                     }

            });
        }
    }
}