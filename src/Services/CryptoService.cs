using System;
using System.Security.Cryptography;
using System.Text;

namespace MSISDNWebClient.Services
{
    /// <summary>
    /// Servicio de criptografía para generación de claves y cifrado
    /// </summary>
    public class CryptoService
    {
        private const int KeySize = 256;
        private const int SaltSize = 32;

        /// <summary>
        /// Genera un salt aleatorio
        /// </summary>
        public string GenerateSalt()
        {
            var salt = new byte[SaltSize];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }

        /// <summary>
        /// Genera un par de claves RSA (pública y privada)
        /// </summary>
        public (string publicKey, string privateKey) GenerateKeyPair()
        {
            using var rsa = RSA.Create(KeySize * 8);
            var publicKey = Convert.ToBase64String(rsa.ExportRSAPublicKey());
            var privateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());
            return (publicKey, privateKey);
        }

        /// <summary>
        /// Cifra la clave privada con una contraseña derivada del dispositivo
        /// </summary>
        public string EncryptPrivateKey(string privateKey, string deviceId)
        {
            try
            {
                var key = DeriveKey(deviceId);
                using var aes = Aes.Create();
                aes.Key = key;
                aes.GenerateIV();

                using var encryptor = aes.CreateEncryptor();
                var privateKeyBytes = Encoding.UTF8.GetBytes(privateKey);
                var encrypted = encryptor.TransformFinalBlock(privateKeyBytes, 0, privateKeyBytes.Length);

                // Combinar IV + datos cifrados
                var result = new byte[aes.IV.Length + encrypted.Length];
                Buffer.BlockCopy(aes.IV, 0, result, 0, aes.IV.Length);
                Buffer.BlockCopy(encrypted, 0, result, aes.IV.Length, encrypted.Length);

                return Convert.ToBase64String(result);
            }
            catch (Exception ex)
            {
                throw new CryptoException("Error al cifrar la clave privada", ex);
            }
        }

        /// <summary>
        /// Descifra la clave privada
        /// </summary>
        public string DecryptPrivateKey(string encryptedPrivateKey, string deviceId)
        {
            try
            {
                var key = DeriveKey(deviceId);
                var encryptedBytes = Convert.FromBase64String(encryptedPrivateKey);

                using var aes = Aes.Create();
                aes.Key = key;

                // Extraer IV
                var iv = new byte[16];
                Buffer.BlockCopy(encryptedBytes, 0, iv, 0, iv.Length);
                aes.IV = iv;

                // Extraer datos cifrados
                var cipherText = new byte[encryptedBytes.Length - iv.Length];
                Buffer.BlockCopy(encryptedBytes, iv.Length, cipherText, 0, cipherText.Length);

                using var decryptor = aes.CreateDecryptor();
                var decrypted = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);

                return Encoding.UTF8.GetString(decrypted);
            }
            catch (Exception ex)
            {
                throw new CryptoException("Error al descifrar la clave privada", ex);
            }
        }

        /// <summary>
        /// Genera un hash SHA-256 de un string
        /// </summary>
        public string ComputeSHA256(string input)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToHexString(bytes).ToLower();
        }

        /// <summary>
        /// Deriva una clave a partir del ID del dispositivo
        /// </summary>
        private byte[] DeriveKey(string deviceId)
        {
            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(deviceId));
        }

        /// <summary>
        /// Valida el formato de un MSISDN
        /// </summary>
        public bool ValidateMSISDN(string msisdn)
        {
            if (string.IsNullOrWhiteSpace(msisdn))
                return false;

            // Formato E.164: +[código país][número]
            // Ejemplo: +34612345678 (España)
            var cleanMsisdn = msisdn.Trim().Replace(" ", "").Replace("-", "");
            
            if (!cleanMsisdn.StartsWith("+"))
                return false;

            if (cleanMsisdn.Length < 8 || cleanMsisdn.Length > 15)
                return false;

            return long.TryParse(cleanMsisdn[1..], out _);
        }

        /// <summary>
        /// Normaliza un MSISDN al formato estándar
        /// </summary>
        public string NormalizeMSISDN(string msisdn)
        {
            if (string.IsNullOrWhiteSpace(msisdn))
                return string.Empty;

            var cleaned = msisdn.Trim().Replace(" ", "").Replace("-", "");
            
            if (!cleaned.StartsWith("+"))
                cleaned = "+" + cleaned;

            return cleaned;
        }
    }

    public class CryptoException : Exception
    {
        public CryptoException(string message) : base(message) { }
        public CryptoException(string message, Exception innerException) : base(message, innerException) { }
    }
}
