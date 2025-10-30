using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonthlyPremium.Models;
using System.ComponentModel;

namespace MonthlyPremium.Controllers
{
    public class PremiumController : ControllerBase
    {
        private static readonly List<Occupation> OccupationsList = new List<Occupation>
        {
            new Occupation { OccupationId = 1, OccupationName = "Cleaner", Rating = "Light Manual", Factor = 11.50 },
            new Occupation { OccupationId = 2, OccupationName = "Doctor", Rating = "Professional", Factor = 1.5 },
            new Occupation { OccupationId = 3, OccupationName = "Author", Rating = "White Collar", Factor = 2.25 },
            new Occupation { OccupationId = 4, OccupationName = "Farmer", Rating = "Heavy Manual", Factor = 31.75 },
            new Occupation { OccupationId = 5, OccupationName = "Mechanic", Rating = "Heavy Manual", Factor = 31.75 },
            new Occupation { OccupationId = 6, OccupationName = "Florist", Rating = "Light Manual", Factor = 11.50 },
            new Occupation { OccupationId = 7, OccupationName = "Other", Rating = "Heavy Manual", Factor = 31.75 }
        };

        [HttpGet("api/premium/occupations")]
        public IActionResult GetAllOccupations()
        {
            return Ok(OccupationsList);
        }



        [HttpGet("api/premium/calculate")]
        public IActionResult CalculatePremium([FromQuery] Models.MemberRequest memberRequest)
        {
            if (memberRequest == null)
                return BadRequest("Invalid request data");

            if (string.IsNullOrWhiteSpace(memberRequest.Name) ||
                memberRequest.AgeNextBirthDay <= 0 ||
                memberRequest.DeatchSumInsured <= 0 ||
                string.IsNullOrWhiteSpace(memberRequest.Occupation))
            {
                return BadRequest("All fields are mandatory and must have valid values.");
            }

            var occupation = OccupationsList.FirstOrDefault(o =>
                o.OccupationName.Equals(memberRequest.Occupation, System.StringComparison.OrdinalIgnoreCase));

            if (occupation == null)
                return BadRequest("Occupation not found");

            double premium = (memberRequest.DeatchSumInsured * occupation.Factor * memberRequest.AgeNextBirthDay) / (1000 * 12);

            return Ok(new
            {
                Name = memberRequest.Name,
                Occupation = occupation.OccupationName,
                Rating = occupation.Rating,
                Factor = occupation.Factor,
                MonthlyPremium = premium
            });
        }
    }
}
