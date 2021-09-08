namespace SynetecAssessmentApi.Dtos
{
    public class BonusPoolCalculatorResultDto : BaseResponseDTO
    {
        public int Amount { get; set; }
        public EmployeeDto Employee { get; set; }
    }
}
