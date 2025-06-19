using System;
using System.Collections;
using UnityEngine;

namespace AbstractionServer
{
    /// <summary>
    /// AuthService provides methods to register and authenticate users via API.
    /// </summary>
    public static class AuthService
    {
        [Serializable]
        public class LoginRequest
        {
            public string username;
            public string password;

            public LoginRequest(string u, string p)
            {
                username = u;
                password = p;
            }
        }

        [Serializable]
        public class AuthResponse
        {
            public string access_token;
        }

        /// <summary>
        /// Attempts to register and then login a user. If the user already exists, proceeds to login.
        /// </summary>
        public static IEnumerator RegisterAndLogin(string username, string password, Action<string> onSuccess, Action<string> onError)
        {
            var user = new LoginRequest(username, password);

            // Try to register the user
            yield return AbstractionApiClient.Post<LoginRequest, string>(
                "/auth/register",
                user,
                _ =>
                {
                    Debug.Log("📝 Registration successful");
                },
                err =>
                {
                    Debug.LogWarning("⚠️ Registration failed (possibly already exists): " + err);
                }
            );

            // Then try to login
            yield return AbstractionApiClient.Post<LoginRequest, AuthResponse>(
                "/auth/login",
                user,
                res =>
                {
                    AbstractionApiClient.Token = res.access_token;
                    onSuccess?.Invoke(res.access_token);
                },
                onError
            );
        }
    }
}
