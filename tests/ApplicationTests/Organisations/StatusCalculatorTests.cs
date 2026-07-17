using Application.Organisations.GetByLaestab;
using NSubstitute;
using SharedKernel;
using Shouldly;

namespace ApplicationTests.Organisations;

public class StatusCalculatorTests
{
    private readonly IDateTimeProvider _dateTimeProvider = Substitute.For<IDateTimeProvider>();

    [Fact]
    public void GetOpenStatus_Should_Return_Open_During_Working_Hours()
    {
        // arrange
        var eightAm = new DateTime(2026, 2, 11, 8, 1, 0, DateTimeKind.Utc);
        _dateTimeProvider.UtcNow.Returns(eightAm);
        var sc = new StatusCalculator(_dateTimeProvider);
        // act
        OrgStatus result = sc.GetOpenStatus();
        // assert
        result.ShouldBe(OrgStatus.Open);
    }

    [Fact]
    public void GetOpenStatus_Should_Return_Closed_Outside_of_Working_Hours()
    {
        // arrange
        var tenPmUtc = new DateTime(2026, 2, 11, 3, 31, 0, DateTimeKind.Utc);
        _dateTimeProvider.UtcNow.Returns(tenPmUtc);
        var sc = new StatusCalculator(_dateTimeProvider);
        // act
        OrgStatus result = sc.GetOpenStatus();
        // assert
        result.ShouldBe(OrgStatus.Closed);
    }

    [Fact]
    public void GetOpenStatus_Should_Return_Open_At_8_30am_BST()
    {
        // arrange
        // 8:30 AM BST = 7:30 AM UTC 
        var eightThirtyAmBST = new DateTime(2026, 7, 15, 7, 30, 0, DateTimeKind.Utc);
        _dateTimeProvider.UtcNow.Returns(eightThirtyAmBST);
        var sc = new StatusCalculator(_dateTimeProvider);
        // act
        OrgStatus result = sc.GetOpenStatus();
        // assert
        result.ShouldBe(OrgStatus.Open);
    }

    [Fact]
    public void GetOpenStatus_Should_Return_Closed_Before_8_00am_BST()
    {
        // arrange
        // 7:15 AM UTC = 8:15 AM BST
        var beforeOpeningBST = new DateTime(2026, 7, 15, 6, 59, 0, DateTimeKind.Utc);
        _dateTimeProvider.UtcNow.Returns(beforeOpeningBST);
        var sc = new StatusCalculator(_dateTimeProvider);
        // act
        OrgStatus result = sc.GetOpenStatus();
        // assert
        result.ShouldBe(OrgStatus.Closed);
    }
}
