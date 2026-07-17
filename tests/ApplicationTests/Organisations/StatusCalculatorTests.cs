using Application.Organisations.GetByLaestab;
using NSubstitute;
using SharedKernel;
using Shouldly;

namespace ApplicationTests.Organisations;

public class StatusCalculatorTests
{
    [Fact]
    public void Should_Return_Open_During_Working_Hours()
    {
        IDateTimeProvider dateTimeProvider = Substitute.For<IDateTimeProvider>();
        var eightAm = new DateTime(2026, 2, 11, 8, 1, 0, DateTimeKind.Utc);
        dateTimeProvider.UtcNow.Returns(eightAm);
        var sc = new StatusCalculator(dateTimeProvider);
        OrgStatus result = sc.GetOpenStatus();
        result.ShouldBe(OrgStatus.Open);
    }
    
    [Fact]
    public void Should_Return_Closed_Outside_of_Working_Hours()
    {
        IDateTimeProvider dateTimeProvider = Substitute.For<IDateTimeProvider>();
        var tenPmUtc = new DateTime(2026, 2, 11, 3, 31, 0, DateTimeKind.Utc);
        dateTimeProvider.UtcNow.Returns(tenPmUtc); 
        var sc = new StatusCalculator(dateTimeProvider);
        OrgStatus result = sc.GetOpenStatus();
        result.ShouldBe(OrgStatus.Closed);
    }
    
    [Fact]
    public void Should_Account_For_Daylight_Saving_Time_At_8_30_Am_BST()
    {
        // 8:30 AM BST = 7:30 AM UTC 
        IDateTimeProvider dateTimeProvider = Substitute.For<IDateTimeProvider>();
        var eightThirtyAmBST = new DateTime(2026, 7, 15, 7, 30, 0, DateTimeKind.Utc);
        dateTimeProvider.UtcNow.Returns(eightThirtyAmBST);
        var sc = new StatusCalculator(dateTimeProvider);
        OrgStatus result = sc.GetOpenStatus();
        result.ShouldBe(OrgStatus.Open);
    }
    
    [Fact]
    public void Should_Be_Closed_Before_8_30_Am_BST()
    {
        // 7:15 AM UTC = 8:15 AM BST
        IDateTimeProvider dateTimeProvider = Substitute.For<IDateTimeProvider>();
        var beforeOpeningBST = new DateTime(2026, 7, 15, 7, 15, 0, DateTimeKind.Utc);
        dateTimeProvider.UtcNow.Returns(beforeOpeningBST);
        var sc = new StatusCalculator(dateTimeProvider);
        OrgStatus result = sc.GetOpenStatus();
        result.ShouldBe(OrgStatus.Closed);
    }
    
}
