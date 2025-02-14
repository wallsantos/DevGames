using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExternalDialog : MonoBehaviour
{
    private bool playerNearExternal = false;
    public int dialogId;

    void Update()
    {
        if(playerNearExternal){
            FindObjectOfType<Dialogs>().StartDialog(dialogId);
            playerNearExternal= false;
        }
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            playerNearExternal = true;
        }
    }
    void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Player")){
        }
    }
}
