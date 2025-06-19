# Unity REST API Template: Auth & Command Client

A Unity project template that uses  **abstraction server to interact with smart programs on the Vara Network**, allowing the client to **send messages and read state** from on-chain contracts.

This architecture enables seamless integration between Unity and actor-based smart contracts through a RESTful backend layer.

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