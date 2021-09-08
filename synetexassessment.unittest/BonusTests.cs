using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SynetecAssessmentApi.Persistence;
using SynetecAssessmentApi.Services;
using System.Linq;
using System.Threading.Tasks;

namespace synetexassessment.unittest
{
    [TestClass]
    public class BonusTests
    {
        private AppDbContext _dbContext;

        public BonusTests()
        {

            var dbContextOptionBuilder = new DbContextOptionsBuilder<AppDbContext>();
            dbContextOptionBuilder.UseInMemoryDatabase(databaseName: "HrDb");

            _dbContext = new AppDbContext(dbContextOptionBuilder.Options);
        }

        [TestMethod]
        public async Task TestBonuswithValidData()
        {
            // Arrange
            int TotalSalariesAmount = await _dbContext.Employees.SumAsync(x => x.Salary);
            int BonusPoolAmount = 250000;
            int EmpId = 1;

            var Emp = await _dbContext.Employees.Where(x => x.Id == EmpId).FirstOrDefaultAsync();


            if (Emp != null)
            {
                var EmpSal = Emp.Salary;
                var bonusPercentage = (decimal)EmpSal / (decimal)TotalSalariesAmount;

                var expectedBonus = (int)(BonusPoolAmount * bonusPercentage);

                var bonusPoolService = new BonusPoolService();
                // Act
                var actualBonusPoolAmount = await bonusPoolService.CalculateAsync(BonusPoolAmount, EmpId);

                //Assert
                Assert.AreEqual(expectedBonus, actualBonusPoolAmount, "Bonus Calculated correctly");
                Assert.AreNotEqual(expectedBonus, actualBonusPoolAmount, "Bonus Calculated Incorrect");

            }
        }
    }
}
