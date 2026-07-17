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
    public void Laestab_is_separated_into_localAuthorityCode_and_establishmentNumber_by_LaestabValue()
    {
        // arrange and act
        var laestabValue = new LaestabValue("3214567");
        // assert
        laestabValue.LocalAuthorityCode.ShouldBe("321");
        laestabValue.EstablishmentNumber.ShouldBe("4567");
    }
}
