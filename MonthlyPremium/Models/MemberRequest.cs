namespace MonthlyPremium.Models
{
    public class MemberRequest
    {
        public string Name { get; set; }
        public int AgeNextBirthDay { get; set; }
        public string DateOfBirth { get; set; }
        public string Occupation { get; set; }
        public double DeatchSumInsured { get; set; }
    }
}
