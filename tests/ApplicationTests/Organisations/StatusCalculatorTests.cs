using Application.Organisations.GetByLaestab;
using NSubstitute;
using SharedKernel;
using Shouldly;

namespace ApplicationTests.Organisations;

public class StatusCalculatorTests
{
    private readonly IDateTimeProvider _dateTimeProvider = Substitute.For<IDateTimeProvider>();

    [Fact]
    public void Organisation_is_open_during_school_hours()
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
    public void Organisation_is_open_at_exact_opening_time()
    {
        // arrange
        var eightAm = new DateTime(2026, 2, 11, 8, 0, 0, DateTimeKind.Utc);
        _dateTimeProvider.UtcNow.Returns(eightAm);
        var sc = new StatusCalculator(_dateTimeProvider);
        // act
        OrgStatus result = sc.GetOpenStatus();
        // assert
        result.ShouldBe(OrgStatus.Open);
    }

    [Fact]
    public void Organisation_is_closed_at_exact_closing_time()
    {
        // arrange
        var eightAm = new DateTime(2026, 2, 11, 15, 30, 0, DateTimeKind.Utc);
        _dateTimeProvider.UtcNow.Returns(eightAm);
        var sc = new StatusCalculator(_dateTimeProvider);
        // act
        OrgStatus result = sc.GetOpenStatus();
        // assert
        result.ShouldBe(OrgStatus.Closed);
    }

    [Fact]
    public void Organisation_is_closed_outside_of_school_hours()
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
    public void Organisation_takes_into_account_daylight_savings_for_open()
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
    public void Organisation_takes_into_account_daylight_savings_for_closed()
    {
        // arrange
        // 6:59 AM UTC = 7:59 AM BST
        var beforeOpeningBST = new DateTime(2026, 7, 15, 6, 59, 0, DateTimeKind.Utc);
        _dateTimeProvider.UtcNow.Returns(beforeOpeningBST);
        var sc = new StatusCalculator(_dateTimeProvider);
        // act
        OrgStatus result = sc.GetOpenStatus();
        // assert
        result.ShouldBe(OrgStatus.Closed);
    }
}
