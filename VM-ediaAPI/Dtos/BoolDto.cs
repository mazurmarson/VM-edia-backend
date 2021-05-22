namespace VM_ediaAPI.Dtos
{
    public class BoolDto
    {
        public bool IsPositive {get; set;}
        public BoolDto(bool State)
        {
            IsPositive = State;
        }
    }
}