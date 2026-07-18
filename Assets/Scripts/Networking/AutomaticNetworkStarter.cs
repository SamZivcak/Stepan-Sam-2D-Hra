using System.Collections;
using Mirror;
using UnityEngine;

public class AutomaticNetworkStarter : MonoBehaviour
{
    [SerializeField] private string serverAddress = "YOUR_VPS_IP";
    [SerializeField] private float clientStartDelay = 0.5f;

    private IEnumerator Start()
    {
        if (NetworkManager.singleton == null)
        {
            Debug.LogError("NetworkManager was not found.");
            yield break;
        }

#if UNITY_SERVER
        Debug.Log("Starting dedicated server.");
        NetworkManager.singleton.StartServer();

        yield break;

#elif !UNITY_EDITOR
        Debug.Log($"Connecting to server: {serverAddress}");

        NetworkManager.singleton.networkAddress = serverAddress;

        yield return new WaitForSeconds(clientStartDelay);

        NetworkManager.singleton.StartClient();

        yield break;

#else
        Debug.Log("Running in Unity Editor. Automatic connection is disabled.");

        yield break;
#endif
    }
}