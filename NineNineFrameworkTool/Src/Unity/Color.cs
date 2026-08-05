using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace NineNineFrameworkTool.Unity
{
    internal struct Color
    {
        [JsonPropertyName("r")]
        public float R { get; set; }

        [JsonPropertyName("g")]
        public float G { get; set; }

        [JsonPropertyName("b")]
        public float B { get; set; }

        [JsonPropertyName("a")]
        public float A { get; set; }

        public static Color GetFromSystemColor(System.Drawing.Color color)
        {
            return new Color(color.R / 255f, color.G / 255f, color.B / 255f, color.A / 255f);
        }

        public static Color? GetFromStringNoPercents(string value)
        {
            string[] values = value.Split(" ");

            if (values.Length >= 3)
            {
                int r = int.Parse(values[0]);
                int g = int.Parse(values[1]);
                int b = int.Parse(values[2]);
                int a = 1;

                if (values.Length >= 4) a = int.Parse(values[3]);

                return new Color(r, g, b, a);
            }

            return null!;
        }

        public System.Drawing.Color GetAsSystemColor()
        {
            return System.Drawing.Color.FromArgb((int)(A * 255), (int)(R * 255), (int)(G * 255), (int)(B * 255));
        }

        public string GetAsStringNoPercents()
        {
            return $"{(int)(R * 255)} {(int)(G * 255)} {(int)(B * 255)} {(int)(A * 255)}";
        }

        public Color()
        {
            R = 1f;
            G = 1f;
            B = 1f;
            A = 1f;
        }

        public Color(float r, float g, float b, float a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            Color color = (Color)obj!;

            if (R != color.R || G != color.G || B != color.B || A != color.A) return false;

            return true;
        }

        public static bool operator ==(Color left, Color right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Color left, Color right)
        {
            return !left.Equals(right);
        }

        public override int GetHashCode()
        {
            return R.GetHashCode() ^ (G.GetHashCode() << 2) ^ (B.GetHashCode() >> 2) ^ (A.GetHashCode() >> 1);
        }
    }
}
