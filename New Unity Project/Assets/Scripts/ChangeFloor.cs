using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFloor : MonoBehaviour
{
    private static bool playerNearStair = false;
    public GameObject floorWanted;
    public GameObject floorActual;
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player") && !playerNearStair){
            floorActual.SetActive(false);
            floorWanted.SetActive(true);
            playerNearStair = true;
        }
    }
    void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Player")){
            playerNearStair = false;
        }
    }
}
