using System;
using System.Runtime.CompilerServices;

namespace NeoGuid.GuidGenerator
{
    public static class GuidExtensions
    {
        public static GuidVersion Version(this Guid guid)
        {
            var offset = BitConverter.IsLittleEndian ? 7 : 6;
            ref var guidRef = ref Unsafe.As<Guid, byte>(ref guid);
            var version = Unsafe.Add(ref guidRef, offset) >> 4;

            if (version >= 1 && version <= 8)
            {
                return (GuidVersion)version;
            }

            if (guid == GuidGenerator.Nil)
            {
                return GuidVersion.Nil;
            }

            if (guid == GuidGenerator.Max)
            {
                return GuidVersion.Max;
            }

            return GuidVersion.Unknown;
        }

        public static DateTime ExtractDateTime(this Guid guid)
        {
            if (!guid.TryExtractDateTime(out var dateTime))
            {
                throw new ArgumentException("Cannot extract DateTime from Guid of version other than 7.");
            }

            return dateTime;
        }

        public static bool TryExtractDateTime(this Guid guid, out DateTime dateTime)
        {
            if (guid.Version() != GuidVersion.V7)
            {
                dateTime = default;
                return false;
            }

            var raw = Unsafe.As<Guid, ulong>(ref guid);
            var millis = BitConverter.IsLittleEndian ? ((long)(uint)raw << 16) | (ushort)(raw >> 32) : (long)(raw >> 16);
            var ticks = millis * TimeSpan.TicksPerMillisecond + GuidGenerator.UnixEpochTicks;
            dateTime = new DateTime(ticks, DateTimeKind.Utc);
            return true;
        }
    }
}