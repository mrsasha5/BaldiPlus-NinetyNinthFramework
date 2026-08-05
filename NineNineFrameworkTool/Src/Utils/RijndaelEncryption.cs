using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System.Security.Cryptography;
using System.Text;

namespace NineNineFrameworkTool.Utils
{
    internal class RijndaelEncryption
    {
        private const int BufferKeySize = 32;
        private const int BlockSize = 256;
        private const int Iterations = 1000;

        private PaddedBufferedBlockCipher _cipher = null!;

        private void InitCipher(byte[] key, byte[] iv, bool encryption)
        {
            RijndaelEngine engine = new RijndaelEngine(BlockSize);
            CbcBlockCipher blockCipher = new CbcBlockCipher(engine);

            _cipher = new PaddedBufferedBlockCipher(blockCipher, new Pkcs7Padding());

            KeyParameter keyParam = new KeyParameter(key);
            ParametersWithIV parameters = new ParametersWithIV(keyParam, iv);

            _cipher.Init(encryption, parameters);
        }

        public string Encrypt(string value, string password)
        {
            byte[] encrypted = Encrypt(Encoding.UTF8.GetBytes(value), password);
            return Convert.ToBase64String(encrypted);
        }

        public string Decrypt(string value, string password)
        {
            byte[] decrypted = Decrypt(Convert.FromBase64String(value), password);
            return Encoding.UTF8.GetString(decrypted);
        }

        private byte[] Encrypt(byte[] value, string password)
        {
            Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(password, BufferKeySize, Iterations, HashAlgorithmName.SHA1);
            byte[] salt = new byte[BufferKeySize];
            salt = deriveBytes.Salt;
            byte[] iv = GetIV();

            InitCipher(deriveBytes.GetBytes(BufferKeySize), iv, encryption: true);

            byte[] textBuffer = new byte[_cipher.GetOutputSize(value.Length)];
            int length = _cipher.ProcessBytes(value, 0, value.Length, textBuffer, 0);
            length += _cipher.DoFinal(textBuffer, length);

            byte[] dest = new byte[length];
            Array.Copy(textBuffer, 0, dest, 0, length);

            List<byte> result = new List<byte>(salt);
            result.AddRange(iv);
            result.AddRange(dest);

            return result.ToArray();
        }

        private byte[] Decrypt(byte[] value, string password)
        {
            byte[] salt = new byte[BufferKeySize];
            byte[] iv = new byte[BufferKeySize];
            byte[] cipherText = new byte[value.Length - 64];
            Array.Copy(value, 0, salt, 0, salt.Length);
            Array.Copy(value, BufferKeySize, iv, 0, iv.Length);
            Array.Copy(value, BufferKeySize * 2, cipherText, 0, cipherText.Length);

            Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA1);

            InitCipher(deriveBytes.GetBytes(BufferKeySize), iv, encryption: false);

            byte[] textBuffer = new byte[_cipher.GetOutputSize(cipherText.Length)];
            int length = _cipher.ProcessBytes(cipherText, 0, cipherText.Length, textBuffer, 0);
            length += _cipher.DoFinal(textBuffer, length);

            byte[] result = new byte[length];
            Array.Copy(textBuffer, 0, result, 0, length);

            return result;
        }

        private byte[] GetIV()
        {
            SecureRandom random = new SecureRandom();
            byte[] iv = new byte[BlockSize / 8]; // bits to bytes
            random.NextBytes(iv);

            return iv;
        }
    }
}