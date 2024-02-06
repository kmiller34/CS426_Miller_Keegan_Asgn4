using UnityEngine;
using System.Collections.Generic;

public class BillBoard : MonoBehaviour
{
    List<Camera> playerCameras = new List<Camera>();

    void Start()
    {
        // Find the player cameras when the script starts
        TryFindPlayerCameras();
    }

    void TryFindPlayerCameras()
    {
        playerCameras.Clear(); // Clear the list to avoid duplicates
        Camera[] allCameras = Camera.allCameras;
        foreach (Camera cam in allCameras)
        {
            if (cam.CompareTag("MainCamera"))
            {
                playerCameras.Add(cam);
            }
        }

        // if (playerCameras.Count == 0)
        // {
        //     Debug.LogError("No player cameras found. Make sure there are cameras tagged as 'PlayerCamera' in your scene.");
        // }
    }

    void Update()
    {
        // Check for the player cameras each frame
        if (playerCameras.Count == 0)
        {
            TryFindPlayerCameras();
            return; // Return early if cameras not found yet
        }

        // Set the rotation to face each player camera
        foreach (Camera cam in playerCameras)
        {
            Vector3 direction = transform.position - cam.transform.position;
            direction.y = 0f; // Ensure the object faces horizontally
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
