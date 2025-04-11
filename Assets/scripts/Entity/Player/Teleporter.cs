using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public void TeleportToStart(Vector3 basePosition)
    {
        transform.position = basePosition;
    }
}

