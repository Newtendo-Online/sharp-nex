using System;

namespace nex
{
    public class Version : IComparable<Version>, IEquatable<Version>
    {
        public int Major { get; init; }
        public int Minor { get; init; }
        public int Patch { get; init; }
        public string GameSpecificPatch { get; init; }

        private readonly System.Version _sysVersion;

        public Version(int major, int minor, int patch, string gameSpecificPatch = "")
        {
            Major = major;
            Minor = minor;
            Patch = patch;
            GameSpecificPatch = gameSpecificPatch ?? string.Empty;
            _sysVersion = new System.Version(major, minor, Math.Max(0, patch));
        }

        public static bool TryParse(string? versionString, out Version? result)
        {
            result = null;
            if (string.IsNullOrWhiteSpace(versionString)) return false;

            ReadOnlySpan<char> span = versionString.AsSpan().Trim();
            if (span.Length > 0 && (span[0] == 'v' || span[0] == 'V'))
            {
                span = span[1..];
            }

            if (System.Version.TryParse(span.ToString(), out var parsed))
            {
                result = new Version(parsed.Major, parsed.Minor, Math.Max(0, parsed.Build));
                return true;
            }
            return false;
        }

        public bool GreaterOrEqual(string compare) => 
            TryParse(compare, out var other) && this >= other!;

        public bool LessOrEqual(string compare) => 
            TryParse(compare, out var other) && this <= other!;

        public int CompareTo(Version? other)
        {
            if (other is null) return 1;
            return _sysVersion.CompareTo(other._sysVersion);
        }

        public static bool operator >(Version left, Version right) => left.CompareTo(right) > 0;
        public static bool operator <(Version left, Version right) => left.CompareTo(right) < 0;
        public static bool operator >=(Version left, Version right) => left.CompareTypeSafe(right, (c) => c >= 0);
        public static bool operator <=(Version left, Version right) => left.CompareTypeSafe(right, (c) => c <= 0);
        public static bool operator ==(Version left, Version right) => Equals(left, right);
        public static bool operator !=(Version left, Version right) => !Equals(left, right);

        private bool CompareTypeSafe(Version? other, Func<int, bool> condition) =>
            other is not null && condition(CompareTo(other));

        public override bool Equals(object? obj) => obj is Version other && _sysVersion.Equals(other._sysVersion);
        public bool Equals(Version? other) => other is not null && _sysVersion.Equals(other._sysVersion);
        public override int GetHashCode() => _sysVersion.GetHashCode();

        public override string ToString() => string.IsNullOrEmpty(GameSpecificPatch)
            ? $"v{Major}.{Minor}.{Patch}"
            : $"v{Major}.{Minor}.{Patch}-{GameSpecificPatch}";
    }
}