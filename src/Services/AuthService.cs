using System;
using System.Threading.Tasks;
using MSISDNWebClient.Models;

namespace MSISDNWebClient.Services
{
    /// <summary>
    /// Servicio de autenticación con mock de OTP
    /// </summary>
    public class AuthService
    {
        private readonly CryptoService _cryptoService;
        private readonly StorageService _storageService;
        private OTPRequest? _currentOTPRequest;

        public AuthService(CryptoService cryptoService, StorageService storageService)
        {
            _cryptoService = cryptoService;
            _storageService = storageService;
        }

        /// <summary>
        /// Inicia el proceso de autenticación enviando un OTP mock
        /// </summary>
        public Task<ApiResponse<OTPRequest>> RequestOTPAsync(string msisdn)
        {
            return Task.Run(() =>
            {
                try
                {
                    // Validar formato MSISDN
                    if (!_cryptoService.ValidateMSISDN(msisdn))
                    {
                        return ApiResponse<OTPRequest>.Error(
                            "Formato de número inválido. Use formato internacional: +34612345678");
                    }

                    // Normalizar MSISDN
                    var normalizedMsisdn = _cryptoService.NormalizeMSISDN(msisdn);

                    // Generar OTP mock
                    _currentOTPRequest = new OTPRequest(normalizedMsisdn);

                    // En una app real, aquí se enviaría el SMS
                    // Por ahora, devolvemos el código en el mensaje (solo para desarrollo)
                    return ApiResponse<OTPRequest>.Ok(
                        _currentOTPRequest,
                        $"Código OTP enviado (MOCK: {_currentOTPRequest.Code})");
                }
                catch (Exception ex)
                {
                    return ApiResponse<OTPRequest>.Error($"Error al solicitar OTP: {ex.Message}");
                }
            });
        }

        /// <summary>
        /// Verifica el código OTP ingresado
        /// </summary>
        public Task<ApiResponse<PersonaID>> VerifyOTPAsync(string code)
        {
            return Task.Run(async () =>
            {
                try
                {
                    if (_currentOTPRequest == null)
                    {
                        return ApiResponse<PersonaID>.Error("No hay solicitud de OTP activa");
                    }

                    if (!_currentOTPRequest.Verify(code))
                    {
                        var remainingAttempts = _currentOTPRequest.MaxAttempts - _currentOTPRequest.Attempts;
                        
                        if (_currentOTPRequest.IsExpired())
                            return ApiResponse<PersonaID>.Error("El código OTP ha expirado");
                        
                        if (!_currentOTPRequest.CanRetry())
                            return ApiResponse<PersonaID>.Error("Máximo de intentos alcanzado");

                        return ApiResponse<PersonaID>.Error(
                            $"Código incorrecto. Intentos restantes: {remainingAttempts}");
                    }

                    // Generar PersonaID
                    var personaId = await CreatePersonaIdentityAsync(_currentOTPRequest.MSISDN);
                    
                    return ApiResponse<PersonaID>.Ok(personaId, "Verificación exitosa");
                }
                catch (Exception ex)
                {
                    return ApiResponse<PersonaID>.Error($"Error al verificar OTP: {ex.Message}");
                }
            });
        }

        /// <summary>
        /// Crea una nueva identidad PersonaID completa con claves criptográficas
        /// </summary>
        private async Task<PersonaID> CreatePersonaIdentityAsync(string msisdn)
        {
            // Generar salt único
            var salt = _cryptoService.GenerateSalt();

            // Crear PersonaID
            var personaId = new PersonaID(msisdn, salt);

            // Generar par de claves RSA
            var (publicKey, privateKey) = _cryptoService.GenerateKeyPair();
            personaId.PublicKey = publicKey;

            // Cifrar clave privada con ID del dispositivo
            var deviceId = _storageService.GetDeviceId();
            var encryptedPrivateKey = _cryptoService.EncryptPrivateKey(privateKey, deviceId);
            personaId.PrivateKeyEncrypted = encryptedPrivateKey;

            // Marcar como verificado
            personaId.IsVerified = true;

            // Guardar en almacenamiento seguro
            await _storageService.SavePersonaIdentityAsync(personaId, encryptedPrivateKey, publicKey);
            await _storageService.SetVerifiedAsync(true);

            // Crear perfil inicial
            var profile = new UserProfile(personaId.Id, msisdn)
            {
                DisplayName = $"Persona {personaId.GetShortId()}"
            };
            await _storageService.SaveProfileAsync(profile);

            return personaId;
        }

        /// <summary>
        /// Verifica si el usuario ya está autenticado
        /// </summary>
        public async Task<bool> IsAuthenticatedAsync()
        {
            var identity = await _storageService.GetPersonaIdentityAsync();
            return identity != null && identity.IsVerified;
        }

        /// <summary>
        /// Cierra la sesión del usuario
        /// </summary>
        public async Task<bool> LogoutAsync()
        {
            try
            {
                await _storageService.ClearAllDataAsync();
                _currentOTPRequest = null;
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Obtiene la identidad actual del usuario
        /// </summary>
        public Task<PersonaID?> GetCurrentIdentityAsync()
        {
            return _storageService.GetPersonaIdentityAsync();
        }
    }
}
