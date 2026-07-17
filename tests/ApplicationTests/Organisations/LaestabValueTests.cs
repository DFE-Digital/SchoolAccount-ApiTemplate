using Application.Organisations.GetByLaestab;
using Shouldly;

namespace ApplicationTests.Organisations;

public class LaestabValueTests
{
    /*
      The LaestabValue is only ever run against sanitised strings of 7 digits,
      no further test are necessary
    */
    [Fact]
    public void Should_Split_Laestab_Into_LocalAuthorityCode_And_EstablishmentNumber()
    {
        // arrange and act
        var laestabValue = new LaestabValue("3214567");
        // assert
        laestabValue.LocalAuthorityCode.ShouldBe("321");
        laestabValue.EstablishmentNumber.ShouldBe("4567");
    }
}
