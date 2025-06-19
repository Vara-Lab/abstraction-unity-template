# Unity REST API Template: Auth & Command Client

A Unity project template for connecting to backend APIs with support for **authentication (register/login)** and **command execution**, designed for scalable use in Web3 or Web2 environments.

---

## ✨ Features

- ✅ Modular architecture with `AuthService` and `ApiClient`
- ✅ Support for `POST` and `GET` requests with typed responses 
- ✅ Token-based authentication (`Bearer` header)
- ✅ Full login + register flow with session handling
- ✅ Command execution for logic (e.g., `/command/green`)
- ✅ Namespaced, reusable, and easily extendable

---


---

## 🚀 How to Use

1. Clone or fork the repo.
2. Replace API base URL inside `AbstractionApiClient.cs` if needed:

```csharp
public static string BaseUrl = "http://localhost:8000";
```

3. Open Test.cs and hit Play. It will:

- Register a user

- Log in and store token

- Fetch state program

- Send a command for Smart Program

###  Auth Flow

```csharp
StartCoroutine(AuthService.RegisterAndLogin(
  "username", "password",
  token => Debug.Log("Token: " + token),
  error => Debug.LogError("Login failed: " + error)
));
```

###  Sending Commands

```csharp
StartCoroutine(ApiClient.Post(
  "/trafficlight/command/green",
  res => Debug.Log("Command sent: " + res),
  err => Debug.LogError("Command error: " + err)
));

```

###   Getting State

```csharp
StartCoroutine(ApiClient.Get<string>(
  "/trafficlight/query/trafficlight",
  res => Debug.Log("State: " + res),
  err => Debug.LogError("Failed to get state: " + err)
));

```
## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.