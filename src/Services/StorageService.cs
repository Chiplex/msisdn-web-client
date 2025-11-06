using System;
using System.Threading.Tasks;
using MSISDNWebClient.Models;

namespace MSISDNWebClient.Services
{
    /// <summary>
    /// Servicio de almacenamiento seguro local
    /// Usa Preferences de MAUI para datos simples
    /// </summary>
    public class StorageService
    {
        private const string PersonaIdKey = "msisdn_persona_id";
        private const string MSISDNKey = "msisdn_phone";
        private const string PrivateKeyKey = "msisdn_private_key";
        private const string PublicKeyKey = "msisdn_public_key";
        private const string SaltKey = "msisdn_salt";
        private const string IsVerifiedKey = "msisdn_verified";
        private const string ProfileKey = "msisdn_profile";
        private const string DeviceIdKey = "msisdn_device_id";

        /// <summary>
        /// Guarda la identidad PersonaID completa
        /// </summary>
        public Task SavePersonaIdentityAsync(PersonaID personaId, string privateKey, string publicKey)
        {
            return Task.Run(() =>
            {
                Preferences.Set(PersonaIdKey, personaId.Id);
                Preferences.Set(MSISDNKey, personaId.MSISDN);
                Preferences.Set(PrivateKeyKey, privateKey);
                Preferences.Set(PublicKeyKey, publicKey);
                Preferences.Set(SaltKey, personaId.Salt);
                Preferences.Set(IsVerifiedKey, personaId.IsVerified);
            });
        }

        /// <summary>
        /// Recupera la identidad PersonaID guardada
        /// </summary>
        public Task<PersonaID?> GetPersonaIdentityAsync()
        {
            return Task.Run(() =>
            {
                var id = Preferences.Get(PersonaIdKey, string.Empty);
                if (string.IsNullOrEmpty(id))
                    return null;

                return new PersonaID
                {
                    Id = id,
                    MSISDN = Preferences.Get(MSISDNKey, string.Empty),
                    Salt = Preferences.Get(SaltKey, string.Empty),
                    IsVerified = Preferences.Get(IsVerifiedKey, false),
                    PublicKey = Preferences.Get(PublicKeyKey, string.Empty),
                    PrivateKeyEncrypted = Preferences.Get(PrivateKeyKey, string.Empty)
                };
            });
        }

        /// <summary>
        /// Guarda el perfil de usuario
        /// </summary>
        public Task SaveProfileAsync(UserProfile profile)
        {
            return Task.Run(() =>
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(profile);
                Preferences.Set(ProfileKey, json);
            });
        }

        /// <summary>
        /// Recupera el perfil de usuario
        /// </summary>
        public Task<UserProfile?> GetProfileAsync()
        {
            return Task.Run(() =>
            {
                var json = Preferences.Get(ProfileKey, string.Empty);
                if (string.IsNullOrEmpty(json))
                    return null;

                return Newtonsoft.Json.JsonConvert.DeserializeObject<UserProfile>(json);
            });
        }

        /// <summary>
        /// Marca la identidad como verificada
        /// </summary>
        public Task SetVerifiedAsync(bool verified)
        {
            return Task.Run(() => Preferences.Set(IsVerifiedKey, verified));
        }

        /// <summary>
        /// Verifica si hay una identidad guardada
        /// </summary>
        public Task<bool> HasIdentityAsync()
        {
            return Task.Run(() => !string.IsNullOrEmpty(Preferences.Get(PersonaIdKey, string.Empty)));
        }

        /// <summary>
        /// Obtiene o genera un ID único del dispositivo
        /// </summary>
        public string GetDeviceId()
        {
            var deviceId = Preferences.Get(DeviceIdKey, string.Empty);
            if (string.IsNullOrEmpty(deviceId))
            {
                deviceId = Guid.NewGuid().ToString();
                Preferences.Set(DeviceIdKey, deviceId);
            }
            return deviceId;
        }

        /// <summary>
        /// Limpia todos los datos almacenados
        /// </summary>
        public Task ClearAllDataAsync()
        {
            return Task.Run(() =>
            {
                Preferences.Remove(PersonaIdKey);
                Preferences.Remove(MSISDNKey);
                Preferences.Remove(PrivateKeyKey);
                Preferences.Remove(PublicKeyKey);
                Preferences.Remove(SaltKey);
                Preferences.Remove(IsVerifiedKey);
                Preferences.Remove(ProfileKey);
            });
        }

        /// <summary>
        /// Obtiene la clave privada cifrada
        /// </summary>
        public Task<string?> GetPrivateKeyAsync()
        {
            return Task.Run(() =>
            {
                var key = Preferences.Get(PrivateKeyKey, string.Empty);
                return string.IsNullOrEmpty(key) ? null : key;
            });
        }
    }
}
