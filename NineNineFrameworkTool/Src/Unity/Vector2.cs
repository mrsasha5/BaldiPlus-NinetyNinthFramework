using System.Globalization;
using System.Text.Json.Serialization;

namespace NineNineFrameworkTool.Unity
{
    internal struct Vector2
    {
        [JsonPropertyName("x")]
        public float X;

        [JsonPropertyName("y")]
        public float Y;

        public static Vector2? GetFromString(string value)
        {
            string[] coordinates = value.Split(" ");
            try
            {
                float x = float.Parse(coordinates[0], CultureInfo.InvariantCulture.NumberFormat);
                float y = float.Parse(coordinates[1], CultureInfo.InvariantCulture.NumberFormat);
                return new Vector2(x, y);
            }
            catch { return null; }
        }

        public string GetAsString()
        {
            return $"{X.ToString(CultureInfo.InvariantCulture.NumberFormat)} {Y.ToString(CultureInfo.InvariantCulture.NumberFormat)}";
        }

        public Vector2(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            return X == ((Vector2)obj).X && Y == ((Vector2)obj).Y;
        }

        public static bool operator ==(Vector2 left, Vector2 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vector2 left, Vector2 right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            return ToString(CultureInfo.InvariantCulture.NumberFormat);
        }

        public string ToString(IFormatProvider formatProvider)
        {
            return string.Format("{0}, {1}", X.ToString(formatProvider), Y.ToString(formatProvider));
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ (Y.GetHashCode() << 2);
        }
    }
}