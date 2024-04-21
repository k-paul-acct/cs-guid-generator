namespace NeoGuid.GuidGenerator.Tests;

public class ExtractDateTimeTests
{
    [Fact]
    private void ExtractDateTime_V7_Utc()
    {
        var dto = DateTimeOffset.Parse("2024-01-02T12:15:45.1230000+00:00");
        var dt = dto.UtcDateTime;
        var guid = GuidGenerator.NewV7(dt);
        var extracted = guid.ExtractDateTime();

        Assert.Equal(dt, extracted);
    }

    [Fact]
    private void ExtractDateTime_V7_Shifted()
    {
        var dto = DateTimeOffset.Parse("2024-01-02T12:15:45.1230000+04:00");
        var dt = dto.UtcDateTime;
        var guid = GuidGenerator.NewV7(dt);
        var extracted = guid.ExtractDateTime();

        Assert.Equal(dt, extracted);
    }

    [Fact]
    private void ExtractDateTime_V7_MinDateTime()
    {
        var dt = DateTime.MinValue;
        var guid = GuidGenerator.NewV7(dt);
        var extracted = guid.ExtractDateTime();

        Assert.Equal(DateTime.UnixEpoch, extracted);
    }

    [Fact]
    private void ExtractDateTime_V7_UnixEpoch()
    {
        var dt = DateTime.UnixEpoch;
        var guid = GuidGenerator.NewV7(dt);
        var extracted = guid.ExtractDateTime();

        Assert.Equal(dt, extracted);
    }

    [Fact]
    private void ExtractDateTime_V7_MaxDateTime()
    {
        var dt = DateTime.MaxValue;
        var guid = GuidGenerator.NewV7(dt);
        var extracted = guid.ExtractDateTime();

        Assert.Equal(dt, extracted, TimeSpan.FromMilliseconds(1));
    }

    [Fact]
    private void ExtractDateTime_V7_UtcNow()
    {
        var dt = DateTime.UtcNow;
        var guid = GuidGenerator.NewV7(dt);
        var extracted = guid.ExtractDateTime();

        Assert.Equal(dt, extracted, TimeSpan.FromMilliseconds(1));
    }

    [Fact]
    private void ExtractDateTime_Throw_When_NotV7()
    {
        var guid = GuidGenerator.NewV4();

        Assert.Throws<ArgumentException>(() => guid.ExtractDateTime());
    }

    [Fact]
    private void TryExtractDateTime_Returns_False_When_NotV7()
    {
        var guid = GuidGenerator.NewV4();

        Assert.False(guid.TryExtractDateTime(out _));
    }

    [Fact]
    private void TryExtractDateTime_Returns_True_When_V7()
    {
        var guid = GuidGenerator.NewV7();

        Assert.True(guid.TryExtractDateTime(out _));
    }
}