namespace NeoGuid.GuidGenerator.Tests;

public class GuidV7Tests
{
    [Fact]
    public void TwoSequential()
    {
        var guid1 = GuidGenerator.NewV7();
        var guid2 = GuidGenerator.NewV7();

        Assert.True(guid2 > guid1);

        var extracted1 = guid1.ExtractDateTime();
        var extracted2 = guid2.ExtractDateTime();

        Assert.True(extracted2 >= extracted1);
    }

    [Fact]
    public void TwoCustom()
    {
        var dt = DateTime.UtcNow;
        var guid1 = GuidGenerator.NewV7(dt);
        var guid2 = GuidGenerator.NewV7(dt);

        var extracted1 = guid1.ExtractDateTime();
        var extracted2 = guid2.ExtractDateTime();

        Assert.True(extracted1 == extracted2);
    }

    [Fact]
    public void Custom_Then_Sequential_With_DayDiff()
    {
        var dt = DateTime.UtcNow + TimeSpan.FromDays(1);
        var guid1 = GuidGenerator.NewV7(dt);
        var guid2 = GuidGenerator.NewV7();

        Assert.True(guid2 < guid1);

        var extracted1 = guid1.ExtractDateTime();
        var extracted2 = guid2.ExtractDateTime();

        Assert.True(extracted2 < extracted1);
    }

    [Fact]
    public void Custom_With_NegativeTicks_DateTime_Equals_UnixEpoch()
    {
        var dt = DateTime.MinValue;
        var guid = GuidGenerator.NewV7(dt);
        var extracted = guid.ExtractDateTime();

        Assert.Equal(DateTime.UnixEpoch, extracted);
    }
}