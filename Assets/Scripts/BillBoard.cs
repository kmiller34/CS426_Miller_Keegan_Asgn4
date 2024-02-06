using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 board;

    Camera mainCamera;

    void Start()
    {
        // Initial attempt to find the main camera
        TryFindMainCamera();
    }

    void TryFindMainCamera()
    {
        mainCamera = FindMainCamera();
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found. Make sure there is a camera tagged as 'MainCamera' in your scene.");
        }
    }

    Camera FindMainCamera()
    {
        Camera[] cameras = Camera.allCameras;
        foreach (Camera cam in cameras)
        {
            if (cam.CompareTag("MainCamera"))
            {
                return cam;
            }
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        // Check for the main camera each frame
        if (mainCamera == null)
        {
            TryFindMainCamera();
        }

        // Proceed with the billboard behavior if the main camera is found
        if (mainCamera != null)
        {
            board = mainCamera.transform.forward;
            board.y = 0;
            transform.rotation = Quaternion.LookRotation(board);
            board.y = 0;
            
        }
    }
}