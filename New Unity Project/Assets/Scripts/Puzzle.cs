using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    private GameObject bauAberto;
    private GameObject bauFechado;
    private bool PodeAbrir=false;
    private bool ShowChest=false;
    void Start()
    {
        bauAberto = GameObject.Find("bauAberto");
        bauFechado = GameObject.Find("bauFechado");
    }
    void Update(){
        if(ShowChest && bauFechado.activeSelf){
            bauFechado.GetComponent<SpriteRenderer>().sortingOrder = 10;
        }else if(!ShowChest && bauFechado.activeSelf){
            bauFechado.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
        if(PodeAbrir && Input.GetKeyDown(KeyCode.E)){
            bauFechado.SetActive(false);
            bauAberto.GetComponent<SpriteRenderer>().sortingOrder = 10;
            enabled = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            PodeAbrir = true;
            ShowChest = true;
        }
    }
    void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Player")){
            PodeAbrir = false;
            ShowChest = false;
        }
    }
}
