using Mirror;
using UnityEngine;

public class LocalPlayerCamera : NetworkBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private AudioListener audioListener;

    public override void OnStartLocalPlayer()
    {
        playerCamera.gameObject.SetActive(true);

        if (audioListener != null)
            audioListener.enabled = true;
    }

    public override void OnStartClient()
    {
        if (isLocalPlayer)
            return;

        playerCamera.gameObject.SetActive(false);

        if (audioListener != null)
            audioListener.enabled = false;
    }
}