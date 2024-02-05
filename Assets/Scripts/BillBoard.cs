using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 board;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        board = Camera.main.transform.forward;
        board.y = 0;

        transform.rotation = Quaternion.LookRotation(board);
    }
}
