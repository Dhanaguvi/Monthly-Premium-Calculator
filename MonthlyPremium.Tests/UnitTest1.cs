namespace MonthlyPremium.Tests;
using Xunit;
using MonthlyPremium.Controllers;
using MonthlyPremium.Models;
using Microsoft.AspNetCore.Mvc;

public class UnitTest1
{
    private readonly PremiumController _controller;

    public UnitTest1()
    {
        _controller = new PremiumController();
    }
    [Fact]
    public void GetAllOccupationsTest()
    {
        // Return all the occupations defined in the controller
        var response = _controller.GetAllOccupations();
        var successResponse = Assert.IsType<OkObjectResult>(response);
        var occupations = Assert.IsAssignableFrom<IEnumerable<Occupation>>(successResponse.Value);
        Assert.Equal(7, occupations.Count()); 
    }

    [Fact]
    public void CalculatePremium_InvalidInput()
    {
        // Test with invalid input data returns BadRequest
        var invalidRequest = new MemberRequest
        {
            Name = "",
            AgeNextBirthDay = 0,
            DeatchSumInsured = 0,
            Occupation = ""
        };

        var response = _controller.CalculatePremium(invalidRequest);
        Assert.IsType<BadRequestObjectResult>(response);
    }

    [Fact]
    public void CalculatePremium_RequestIsNull()
    {
        // Test when the request is null returns BadRequest
        var response = _controller.CalculatePremium(null);
        Assert.IsType<BadRequestObjectResult>(response);
    }

    private static decimal ExtractPremium(object? value)
    {
        // help method to extract premium value from various response types
        if (value == null)
            throw new ArgumentNullException(nameof(value));

        if (value is decimal d)
            return d;

        if (value is IConvertible)
            return Convert.ToDecimal(value);

        var prop = value.GetType().GetProperty("MonthlyPremium");
        if (prop != null)
            return Convert.ToDecimal(prop.GetValue(value));

        throw new InvalidOperationException("Cannot extract premium value.");
    }

    [Fact]
    public void CalculatePremium_ValidInput()
    {
        // Test with valid input data returns a positive premium value
        var controller = new PremiumController();
        var request = new MemberRequest
        {
            Name = "Dhana",
            AgeNextBirthDay = 35,
            DeatchSumInsured = 100000,
            Occupation = "Doctor"
        };

        var response = controller.CalculatePremium(request) as OkObjectResult;

        Assert.NotNull(response);
        var premium = ExtractPremium(response.Value);
        Assert.True(premium > 0);
    }

    [Fact]
    public void CalculatePremiums_ForDifferentOccupations()
    {
        // Test that premiums differ for different occupations with same age and sum insured
        var controller = new PremiumController();
        var request1 = new MemberRequest
        {
            Name = "Dhana",
            AgeNextBirthDay = 30,
            DeatchSumInsured = 50000,
            Occupation = "Doctor"
        };
        var request2 = new MemberRequest
        {
            Name = "Sekar",
            AgeNextBirthDay = 30,
            DeatchSumInsured = 50000,
            Occupation = "Cleaner"
        };

        var response1 = controller.CalculatePremium(request1) as OkObjectResult;
        var response2 = controller.CalculatePremium(request2) as OkObjectResult;

        Assert.NotNull(response2);
        Assert.NotNull(response2);
        var premium1 = ExtractPremium(response1.Value);
        var premium2 = ExtractPremium(response2.Value);
        Assert.NotEqual(premium1, premium2);
    }

    [Fact]
    public void CalculatePremium_DifferentAge()
    {
        // Test that premiums differ for same occupation and sum insured but different ages
        var controller = new PremiumController();
        var request1 = new MemberRequest
        {
            Name = "Dhana",
            AgeNextBirthDay = 25,
            DeatchSumInsured = 100000,
            Occupation = "Doctor"
        };
        var request2 = new MemberRequest
        {
            Name = "Sekar",
            AgeNextBirthDay = 45,
            DeatchSumInsured = 100000,
            Occupation = "Doctor"
        };

        var response1 = controller.CalculatePremium(request1) as OkObjectResult;
        var response2 = controller.CalculatePremium(request2) as OkObjectResult;

        Assert.NotNull(response1);
        Assert.NotNull(response2);
        var premium1 = ExtractPremium(response1.Value);
        var premium2 = ExtractPremium(response2.Value);
        Assert.NotEqual(premium1, premium2);
    }
}