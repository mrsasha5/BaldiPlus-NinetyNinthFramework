using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.Json.Serialization;

namespace NineNineFrameworkTool.Unity
{
    internal struct Color
    {
        private float _r, _g, _b, _a;

        [JsonPropertyName("r")]
        public float R { get { return _r; } set { _r = float.Clamp(value, 0f, 1f); } }

        [JsonPropertyName("g")]
        public float G { get { return _g; } set { _g = float.Clamp(value, 0f, 1f); } }

        [JsonPropertyName("b")]
        public float B { get { return _b; } set { _b = float.Clamp(value, 0f, 1f); } }

        [JsonPropertyName("a")]
        public float A { get { return _a; } set { _a = float.Clamp(value, 0f, 1f); } }

        public static Color GetFromSystemColor(System.Drawing.Color color)
        {
            return new Color(color.R / 255f, color.G / 255f, color.B / 255f, color.A / 255f);
        }

        public static Color? GetFromStringNoPercents(string value)
        {
            if (string.IsNullOrEmpty(value)) return null;

            string[] values = value.Split(" ");

            if (values.Length >= 3)
            {
                try
                {
                    int r = int.Parse(values[0]);
                    int g = int.Parse(values[1]);
                    int b = int.Parse(values[2]);
                    int a = 255;

                    if (values.Length >= 4) a = int.Parse(values[3]);

                    return new Color(r / 255f, g / 255f, b / 255f, a / 255f);
                }
                catch { return null; }
            }

            return null;
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
