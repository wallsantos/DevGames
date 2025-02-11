using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Dialogs : MonoBehaviour
{
    public GameObject dialogBox;
    private Text textDialog;
    private int contDialog=1;
    private PlayerController playerController;
    private choseOperation operation;
    private string msgText;
    private string valorPego;
        
    void Start()
    {
        dialogBox.SetActive(true);
        textDialog = GameObject.Find("CMPrincipal/Dialog Box/Canvas/textDialog").GetComponent<Text>();
    }
    void Update()
    {
        if(contDialog>0){
            playerController = this.gameObject.GetComponent<PlayerController>();
            playerController.canMove = false;
            switch(contDialog){
                case 1:
                    msgText = "OL�, BEM-VINDO AO PEREIRINHA ADVENTURE, NESSE BREVE TUTORIAL ENSINAREMOS VOC� A JOGAR. APERTE E PARA CONTINUAR.";
                    break;
                case 2:
                    msgText = "NOSSO JOGO UTILIZA EM TECLAS WASD PARA MOVER-SE, O MOUSE PARA CLICAR NAS ALTERNATIVAS E E PARA INTERAGIR.";
                    break;
                case 3:
                    msgText = "NOSSO PRIMEIRO DESAFIO EST� NA PORTA, V� AT� ELA E APERTE E PARA INICIAR.";
                    break;
                case 4:
                    msgText = "PARAB�NS, VOC� PASSOU O PRIMEIRO DESAFIO!";
                    break;
                case 5:
                    msgText = "AGORA, SEMPRE QUE APARECEREM NOVAS IMAGENS NA TELA, SIGNIFICA QUE VOC� TEM NOVOS DESAFIOS. AO ACERTAR, SUA NOTA NA FOLHA BRANCA AUMENTAR�.";
                    break;
                case 6:
                    msgText = "VOC� SELECIONOU A OPERA��O: " + valorPego + ", VOC� PODER� ALTER�-LA NO COMPUTADOR SEMPRE QUE QUISER!";
                    break;
                default:
                    break;
            }
            dialogBox.SetActive(true);
            textDialog.text = msgText;
            if(Input.GetKeyDown(KeyCode.E)){
                dialogBox.SetActive(false);
                switch(contDialog){
                case 1:
                    contDialog=2;
                    break;
                case 2:
                    contDialog=3;
                    break;
                case 3:
                    contDialog=0;
                    break;
                case 4:
                    contDialog=5;
                    break;
                case 5:
                    contDialog=0;
                    break;
                case 6:
                    contDialog=0;
                    break;
                default:
                    break;
                }
            }
        }else{
            if(playerController!=null){
                playerController.canMove = true;
                playerController = null;
            }
        }
    }

    public void StartDialog(int id, string opSelect = "")
    {
        contDialog = id;
        valorPego = opSelect;
    }
}
