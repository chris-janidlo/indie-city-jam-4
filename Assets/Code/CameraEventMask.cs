using UnityEngine;

public class CameraEventMask : MonoBehaviour
{
    public LayerMask Mask;
    public Camera Camera;
    
    void Start ()
    {
        Camera.eventMask = Mask;
    }
}
