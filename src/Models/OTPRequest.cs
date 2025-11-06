using System;

namespace MSISDNWebClient.Models
{
    /// <summary>
    /// Representa una solicitud de verificación OTP
    /// </summary>
    public class OTPRequest
    {
        public string RequestId { get; set; } = Guid.NewGuid().ToString();
        public string MSISDN { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAt { get; set; }
        public OTPStatus Status { get; set; } = OTPStatus.Pending;
        public int Attempts { get; set; } = 0;
        public int MaxAttempts { get; set; } = 3;

        public OTPRequest() 
        {
            ExpiresAt = DateTime.UtcNow.AddMinutes(5);
        }

        public OTPRequest(string msisdn, int validityMinutes = 5)
        {
            MSISDN = msisdn;
            RequestedAt = DateTime.UtcNow;
            ExpiresAt = RequestedAt.AddMinutes(validityMinutes);
            Code = GenerateMockOTP();
        }

        /// <summary>
        /// Genera un código OTP mock de 6 dígitos
        /// En producción, esto vendría del servicio SMS
        /// </summary>
        private static string GenerateMockOTP()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        /// <summary>
        /// Verifica si el código OTP es válido
        /// </summary>
        public bool Verify(string inputCode)
        {
            Attempts++;

            if (Status != OTPStatus.Pending)
                return false;

            if (DateTime.UtcNow > ExpiresAt)
            {
                Status = OTPStatus.Expired;
                return false;
            }

            if (Attempts > MaxAttempts)
            {
                Status = OTPStatus.Failed;
                return false;
            }

            if (Code == inputCode)
            {
                Status = OTPStatus.Verified;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Indica si el OTP está expirado
        /// </summary>
        public bool IsExpired() => DateTime.UtcNow > ExpiresAt;

        /// <summary>
        /// Indica si aún se pueden hacer intentos
        /// </summary>
        public bool CanRetry() => Attempts < MaxAttempts && !IsExpired();
    }

    public enum OTPStatus
    {
        Pending,
        Verified,
        Expired,
        Failed
    }
}
