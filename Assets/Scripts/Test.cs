using UnityEngine;
using AbstractionServer;

public class Example : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(AuthService.RegisterAndLogin("admin6", "1234",
            token =>
            {
                Debug.Log("✅ Login successful, token: " + token);

                // 🔍 GET traffic light state
                StartCoroutine(AbstractionApiClient.Get<string>(
                    "/trafficlight/query/trafficlight",
                    res => Debug.Log("🚦 Traffic light state: " + res),
                    err => Debug.LogError("❌ Failed to get state: " + err)
                ));

                // 🟢 POST  command
                StartCoroutine(AbstractionApiClient.Post(
                    "/trafficlight/command/red",
                    res => Debug.Log("🟢 command sent: " + res),
                    err => Debug.LogError("❌ Command error: " + err)
                ));
            },
            err => Debug.LogError("❌ Login failed: " + err)
        ));
    }
}
