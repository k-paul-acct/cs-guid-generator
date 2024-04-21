using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace NeoGuid.GuidGenerator;

public static class GuidGenerator
{
    private const byte VerFieldMask = 0x0F;
    private const byte VarFieldMask = 0x3F;
    private const long TicksMinStepV7 = 3;

    private static readonly object LockObj = new();

    private static long _lastUsedTicksV7 = 0 - TicksMinStepV7;

    internal static readonly long UnixEpochTicks = DateTime.UnixEpoch.Ticks;

    public static readonly Guid Nil = new(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
    public static readonly Guid Max = new(0xFFFFFFFF, 0xFFFF, 0xFFFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Guid NewV4()
    {
        return Guid.NewGuid();
    }

    public static Guid NewV7()
    {
        var ticks = DateTime.UtcNow.Ticks - UnixEpochTicks;

        lock (LockObj)
        {
            if (ticks <= _lastUsedTicksV7 || ticks < 0)
            {
                ticks = _lastUsedTicksV7 + TicksMinStepV7;
            }

            _lastUsedTicksV7 = ticks;
        }

        return NewV7(ticks);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Guid NewV7(DateTime timestamp)
    {
        return NewV7(timestamp.Ticks - UnixEpochTicks);
    }

    private static Guid NewV7(long ticks)
    {
        const double nanosFraction = 1.0 / TimeSpan.TicksPerMillisecond * (1 << 12);

        if (ticks < 0)
        {
            ticks = 0;
        }

        Span<byte> span = stackalloc byte[16];
        var millis = ticks / TimeSpan.TicksPerMillisecond;
        var nanos = (ushort)((ticks - millis * TimeSpan.TicksPerMillisecond) * nanosFraction);

        BinaryPrimitives.WriteInt64BigEndian(span, millis << 16);
        BinaryPrimitives.WriteUInt16BigEndian(span[6..], nanos);
        RandomNumberGenerator.Fill(span[8..]);

        ref var verByte = ref span[6];
        verByte = (byte)((verByte & VerFieldMask) | 0x70);
        ref var varByte = ref span[8];
        varByte = (byte)((varByte & VarFieldMask) | 0x80);

        return new Guid(span, true);
    }
}