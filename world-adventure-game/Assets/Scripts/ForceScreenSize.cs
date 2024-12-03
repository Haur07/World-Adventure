using UnityEngine;

public class ScreenResolutionManager : MonoBehaviour
{
    private void Start()
    {
        Screen.SetResolution(1024, 768, false);
    }
}
