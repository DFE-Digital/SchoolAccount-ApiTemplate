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
        // arrange
        string localAuthorityCode = "321";
        string establishmentNo = "4567";
        string laestab = localAuthorityCode + establishmentNo;
        // act
        var laestabValue = new LaestabValue(laestab);
        // assert
        laestabValue.LocalAuthorityCode.ShouldBe(localAuthorityCode);
        laestabValue.EstablishmentNo.ShouldBe(establishmentNo);
    }
}
