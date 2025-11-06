using System;

namespace MSISDNWebClient.Models
{
    /// <summary>
    /// Respuesta genérica de la API
    /// </summary>
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public string? ErrorCode { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public static ApiResponse<T> Ok(T data, string message = "Success")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static ApiResponse<T> Error(string message, string? errorCode = null)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                ErrorCode = errorCode
            };
        }
    }

    /// <summary>
    /// Endpoints de la API mock
    /// </summary>
    public static class ApiEndpoints
    {
        public const string BaseUrl = "https://msisdn-web.test/api";
        public const string Auth = "/auth";
        public const string Persona = "/persona";
        public const string VerifyOTP = "/auth/verify";
        public const string GetPersona = "/persona/{0}";
        public const string UpdatePersona = "/persona/{0}";
    }
}
