using Microsoft.AspNetCore.Mvc;
using SynetecAssessmentApi.Dtos;
using SynetecAssessmentApi.Services;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Controllers
{
    [Route("api/[controller]")]
    public class BonusPoolController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bonusPoolService = new BonusPoolService();

            return Ok(await bonusPoolService.GetEmployeesAsync());
        }

        [HttpPost()]
        public async Task<IActionResult> CalculateBonus([FromBody] CalculateBonusDto request)
        {
            if (request.TotalBonusPoolAmount > 0 && request.SelectedEmployeeId > 0)
            {
                var bonusPoolService = new BonusPoolService();
                return Ok(await bonusPoolService.CalculateAsync(
                    request.TotalBonusPoolAmount,
                    request.SelectedEmployeeId));
            }
            else
            {
                return Ok(new BonusPoolCalculatorResultDto { ResultSuccess= false, Message = "Invalid data request, Please try with valid data" });
            }
        }
    }
}
