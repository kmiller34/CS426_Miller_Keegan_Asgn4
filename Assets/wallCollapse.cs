using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class wallCollapse : MonoBehaviour
{
    public GameObject[] objectsToMonitor;
    public GameObject[] room2Objects;
    public GameObject[] room3Objects;
    public GameObject wallToDisappear;
    public GameObject Room2Wall;
    public TMP_Text winner;

     private void Start()
    {
        objectsToMonitor = GameObject.FindGameObjectsWithTag("Target");
        room2Objects = GameObject.FindGameObjectsWithTag("Room2");
        room3Objects = GameObject.FindGameObjectsWithTag("Room3");
    }
    private void Update()
    {
        wallToDisappear.SetActive(true);
        bool allObjectsDisappeared = true;
        foreach (GameObject obj in objectsToMonitor)
        {
            if (obj != null)
            {
                allObjectsDisappeared = false;
                break;
            }
        }
        if (allObjectsDisappeared)
        {
            wallToDisappear.SetActive(false);
        }

        Room2Wall.SetActive(true);
        bool room2ObjectsDisappear = true;
        foreach (GameObject obj in room2Objects)
        {
            if (obj != null)
            {
                room2ObjectsDisappear = false;
                break;
            }
        }
        if (room2ObjectsDisappear)
        {
            
            Room2Wall.SetActive(false);
        }

        bool room3ObjectsDisappear = true;
        Debug.Log(room3Objects.Length);
        foreach (GameObject obj in room3Objects){
            if(obj != null){
                room3ObjectsDisappear = false;
                break;
            }
        }
        if(room3ObjectsDisappear){
            Debug.Log("hello");
            winner.text = "You WON!!!!!!";
        }
    }
}
