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
    private string msgText;
        
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
                    msgText = "OLÁ, BEM-VINDO AO PEREIRINHA ADVENTURE, NESSE BREVE TUTORIAL ENSINAREMOS VOCÊ A JOGAR. APERTE \"" + "E" + "\" PARA CONTINUAR.";
                    break;
                case 2:
                    msgText = "NOSSO JOGO UTILIZA EM TECLAS \""+"WASD"+"\" PARA MOVER-SE, O \""+"MOUSE"+"\" PARA CLICAR NAS ALTERNATIVAS E A TECLA \""+"E"+"\" PARA INTERAGIR.";
                    break;
                case 3:
                    msgText = "EXISTEM ESCADAS QUE ACESSAM ANDARES COM AS OPERAÇÕES MATEMÁTICAS, NAS PAREDES ESTARÁ QUAL OPERAÇÃO E ANDAR VOCÊ ESTÁ.";
                    break;
                case 4:
                    msgText = "PARA RESPONDER PERGUNTAS VOCÊ DEVE IR ATÉ A SALA ABERTA E APERTAR \""+"E"+"\" QUANDO UM ÍCONE APARECER NO TOPO DO DONO DA SALA.";
                    break;
                case 5:
                    msgText = "VOCÊ TERMINA O JOGO QUANDO RESPONDER TODAS AS PERGUNTAS NO TEMPO LIMITE.";
                    break;
                case 6:
                    msgText = "PARA ABRIR AS PORTAS PRECISA RESPONDER AS PERGUNTAS DA ÚLTIMA PORTA ABERTA DO ANDAR\nBOA SORTE!.";
                    break;
                case 7:
                    msgText = "VOCÊ ACERTOU, A PRÓXIMA PORTA FOI ABERTA";
                    break;
                case 8:
                    msgText = "PARABÉNS, VOCÊ ACERTOU E TERMINOU AS TAREFAS";
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
                    contDialog=4;
                    break;
                case 4:
                    contDialog=5;
                    break;
                case 5:
                    contDialog=6;
                    break;
                case 6:
                    contDialog=0;
                    break;
                case 7:
                    contDialog=0;
                    break;
                case 8:
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
    public void StartDialog(int id)
    {
        contDialog = id;
    }
}
