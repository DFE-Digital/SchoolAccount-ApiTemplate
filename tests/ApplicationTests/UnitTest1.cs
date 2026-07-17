using Shouldly;

namespace ApplicationTests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        "test".ShouldBe("test");
    }
}
