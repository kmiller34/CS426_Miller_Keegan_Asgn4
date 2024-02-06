using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTarget : MonoBehaviour
{
    public GameObject textBox; // Reference to the text box above the target

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) // Assuming the bullet is tagged as "Bullet"
        {
            Destroy(gameObject); // Destroy the target object
            
            // Check if textBox is assigned
            if(textBox != null)
            {
                Destroy(textBox); // Destroy the text box
            }
        }
    }
}
