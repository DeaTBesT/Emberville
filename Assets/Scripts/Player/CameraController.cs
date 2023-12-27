using UnityEngine;

public class CameraController : Controller
{
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }
}
