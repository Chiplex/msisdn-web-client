using System;
using System.Security.Cryptography;
using System.Text;

namespace MSISDNWebClient.Models
{
    /// <summary>
    /// Representa la identidad descentralizada generada a partir del MSISDN
    /// Formato: SHA-256(MSISDN + Timestamp + Salt)
    /// </summary>
    public class PersonaID
    {
        public string Id { get; set; } = string.Empty;
        public string MSISDN { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string Salt { get; set; } = string.Empty;
        public bool IsVerified { get; set; }
        public string PublicKey { get; set; } = string.Empty;
        public string PrivateKeyEncrypted { get; set; } = string.Empty;

        public PersonaID() { }

        public PersonaID(string msisdn, string salt)
        {
            MSISDN = msisdn;
            Salt = salt;
            CreatedAt = DateTime.UtcNow;
            Id = GeneratePersonaId(msisdn, CreatedAt, salt);
            IsVerified = false;
        }

        /// <summary>
        /// Genera el PersonaID usando SHA-256
        /// </summary>
        private static string GeneratePersonaId(string msisdn, DateTime timestamp, string salt)
        {
            var input = $"{msisdn}{timestamp:yyyy-MM-ddTHH:mm:ss.fffZ}{salt}";
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToHexString(bytes).ToLower();
        }

        /// <summary>
        /// Formato corto del PersonaID para mostrar en UI
        /// </summary>
        public string GetShortId()
        {
            if (string.IsNullOrEmpty(Id) || Id.Length < 16)
                return Id;
            
            return $"{Id[..8]}...{Id[^8..]}";
        }

        /// <summary>
        /// Valida el formato del PersonaID
        /// </summary>
        public bool IsValidFormat()
        {
            return !string.IsNullOrEmpty(Id) 
                && Id.Length == 64 
                && System.Text.RegularExpressions.Regex.IsMatch(Id, "^[a-f0-9]{64}$");
        }
    }
}
