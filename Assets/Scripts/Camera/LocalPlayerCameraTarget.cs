using Unity.Cinemachine;
using Mirror;
using UnityEngine;

public class LocalPlayerCameraTarget : NetworkBehaviour
{
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        CinemachineCamera cinemachineCamera =
            FindFirstObjectByType<CinemachineCamera>();

        if (cinemachineCamera == null)
        {
            Debug.LogError("Cinemachine Camera nebyla ve scene nalezena.");
            return;
        }

        cinemachineCamera.Follow = transform;
    }
}