
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(Camera))]
public class CameraAspectRatio : MonoBehaviour
{
    [Header("Game Bounds")]
    [SerializeField] private float gameWidth = 7.5f;
    [SerializeField] private float gameHeight = 11f;

    [Header("Debug Preview")]
    [SerializeField] private bool showDebug = true;
    [SerializeField] private Color boundsColor = Color.green;
    [SerializeField] private Color cameraColor = Color.cyan;

    private Camera cam;

    private void OnEnable()
    {
        cam = GetComponent<Camera>();
        cam.orthographic = true;
        UpdateCamera();
    }

    private void Update()
    {
        if (cam == null)
        {
            cam = GetComponent<Camera>();
        }

        if (cam == null)
        {
            return;
        }

        UpdateCamera();
    }

    private void OnValidate()
    {
        gameWidth = Mathf.Max(0.01f, gameWidth);
        gameHeight = Mathf.Max(0.01f, gameHeight);

        if (cam == null) 
        { 
            return; 
        }
        
        cam = GetComponent<Camera>();

        if (cam != null)
        {
            cam.orthographic = true;
            UpdateCamera();
        }
    }

    private void UpdateCamera()
    {
        float aspect = cam.aspect;

        float sizeByHeight = gameHeight / 2f;
        float sizeByWidth = gameWidth / (2f * aspect);

        cam.orthographicSize = Mathf.Max(
            sizeByHeight,
            sizeByWidth
        );
    }

    private void OnDrawGizmos()
    {
        if (!showDebug)
            return;

        if (cam == null)
            cam = GetComponent<Camera>();

        if (cam == null || !cam.orthographic)
            return;

        Matrix4x4 oldMatrix = Gizmos.matrix;

        Gizmos.matrix = Matrix4x4.TRS(
            transform.position,
            transform.rotation,
            Vector3.one
        );

        // Green: game bounds
        Gizmos.color = boundsColor;

        Gizmos.DrawWireCube(
            Vector3.zero,
            new Vector3(gameWidth, gameHeight, 0f)
        );

        // Cyan: actual camera view
        float cameraHeight = cam.orthographicSize * 2f;
        float cameraWidth = cameraHeight * cam.aspect;

        Gizmos.color = cameraColor;

        Gizmos.DrawWireCube(
            Vector3.zero,
            new Vector3(cameraWidth, cameraHeight, 0f)
        );

        Gizmos.matrix = oldMatrix;
    }
}
