namespace NeoGuid.GuidGenerator.Tests;

public class VersionTests
{
    [Fact]
    public void Version_V1()
    {
        var guid = new Guid("12345678-1234-1234-a123-123456789123");
        var version = guid.Version();

        Assert.Equal(GuidVersion.V1, version);
    }

    [Fact]
    public void Version_V2()
    {
        var guid = new Guid("12345678-1234-2234-a123-123456789123");
        var version = guid.Version();

        Assert.Equal(GuidVersion.V2, version);
    }

    [Fact]
    public void Version_V3()
    {
        var guid = new Guid("12345678-1234-3234-a123-123456789123");
        var version = guid.Version();

        Assert.Equal(GuidVersion.V3, version);
    }

    [Fact]
    public void Version_V4()
    {
        var guid = GuidGenerator.NewV4();
        var version = guid.Version();

        Assert.Equal(GuidVersion.V4, version);

        guid = new Guid("12345678-1234-4234-a123-123456789123");
        version = guid.Version();

        Assert.Equal(GuidVersion.V4, version);
    }

    [Fact]
    public void Version_V5()
    {
        var guid = new Guid("12345678-1234-5234-a123-123456789123");
        var version = guid.Version();

        Assert.Equal(GuidVersion.V5, version);
    }

    [Fact]
    public void Version_V6()
    {
        var guid = new Guid("12345678-1234-6234-a123-123456789123");
        var version = guid.Version();

        Assert.Equal(GuidVersion.V6, version);
    }

    [Fact]
    public void Version_V7()
    {
        var guid = GuidGenerator.NewV7();
        var version = guid.Version();

        Assert.Equal(GuidVersion.V7, version);

        guid = new Guid("12345678-1234-7234-a123-123456789123");
        version = guid.Version();

        Assert.Equal(GuidVersion.V7, version);
    }

    [Fact]
    public void Version_V8()
    {
        var guid = new Guid("12345678-1234-8234-a123-123456789123");
        var version = guid.Version();

        Assert.Equal(GuidVersion.V8, version);
    }

    [Fact]
    public void Version_Unknown()
    {
        var guid = new Guid("12345678-1234-f234-a123-123456789123");
        var version = guid.Version();

        Assert.Equal(GuidVersion.Unknown, version);
    }

    [Fact]
    public void Version_Nil()
    {
        var guid = GuidGenerator.Nil;
        var version = guid.Version();

        Assert.Equal(GuidVersion.Nil, version);

        guid = new Guid("00000000-0000-0000-0000-000000000000");
        version = guid.Version();

        Assert.Equal(GuidVersion.Nil, version);
    }

    [Fact]
    public void Version_Max()
    {
        var guid = GuidGenerator.Max;
        var version = guid.Version();

        Assert.Equal(GuidVersion.Max, version);

        guid = new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff");
        version = guid.Version();

        Assert.Equal(GuidVersion.Max, version);
    }
}