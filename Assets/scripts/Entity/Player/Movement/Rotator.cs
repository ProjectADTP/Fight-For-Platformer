using UnityEngine;

public class Rotator : MonoBehaviour
{
    public void Rotate(float moveInput)
    {
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(0, 0, moveInput));

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1f);
    }
}