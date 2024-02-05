using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// adding namespaces
using Unity.Netcode;
// because we are using the NetworkBehaviour class
// NewtorkBehaviour class is a part of the Unity.Netcode namespace
// extension of MonoBehaviour that has functions related to multiplayer
//al was here 
public class PlayerMovement : NetworkBehaviour
{
    public float speed = 2f;
    public float jumpForce = 5f;
    // create a list of colors
    public List<Color> colors = new List<Color>();

    // getting the reference to the prefab
    [SerializeField]
    private GameObject spawnedPrefab;
    // save the instantiated prefab
    private GameObject instantiatedPrefab;

    public GameObject cannon;
    public GameObject bullet;

    // reference to the camera audio listener
    [SerializeField] private AudioListener audioListener;
    // reference to the camera
    [SerializeField] private Camera playerCamera;

    // bool to see if player is grounded or not.
    private bool isGrounded;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().freezeRotation = true;
    }
    // Update is called once per frame
    void Update()
    {
        // check if the player is the owner of the object
        // makes sure the script is only executed on the owners 
        // not on the other prefabs 
        if (!IsOwner) return;

        HandleMovementInput();
        HandleRotationInput();
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            //despawn the object
            instantiatedPrefab.GetComponent<NetworkObject>().Despawn(true);
            // destroy the object
            Destroy(instantiatedPrefab);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            // call the BulletSpawningServerRpc method
            // as client can not spawn objects
            BulletSpawningServerRpc(cannon.transform.position, cannon.transform.rotation);
        }
    }

    void HandleMovementInput()
    {
        Vector3 moveDirection = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            moveDirection.z = +1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection.z = -1f;
        }


        transform.Translate(moveDirection * speed * Time.deltaTime);

    }

    void HandleRotationInput()
    {
        float rotationSpeed = 100.0f;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the player is grounded when colliding with a surface
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Set isGrounded to false when leaving a surface
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void Jump()
    {
        // Apply the jump force to the player
        GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    // this method is called when the object is spawned
    // we will change the color of the objects
    public override void OnNetworkSpawn()
    {
        GetComponent<MeshRenderer>().material.color = colors[(int)OwnerClientId];

        // check if the player is the owner of the object
        if (!IsOwner) return;
        // if the player is the owner of the object
        // enable the camera and the audio listener
        audioListener.enabled = true;
        playerCamera.enabled = true;
    }

    // need to add the [ServerRPC] attribute
    [ServerRpc]
    // method name must end with ServerRPC
    private void BulletSpawningServerRpc(Vector3 position, Quaternion rotation)
    {
        // call the BulletSpawningClientRpc method to locally create the bullet on all clients
        BulletSpawningClientRpc(position, rotation);
    }

    [ClientRpc]
    private void BulletSpawningClientRpc(Vector3 position, Quaternion rotation)
    {
        GameObject newBullet = Instantiate(bullet, position, rotation);
        newBullet.GetComponent<Rigidbody>().velocity += Vector3.up * 2;
        newBullet.GetComponent<Rigidbody>().AddForce(newBullet.transform.forward * 1500);
        // newBullet.GetComponent<NetworkObject>().Spawn(true);
    }
}