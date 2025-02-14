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
    string teclas = "\"W\" \"A\" \"S\" \"D\"";
        
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
                    msgText = "NOSSO JOGO UTILIZA AS TECLAS " + teclas + " PARA MOVER-SE, O \""+"MOUSE"+"\" PARA CLICAR NAS ALTERNATIVAS E A TECLA \""+"E"+"\" PARA INTERAGIR.";
                    break;
                case 3:
                    msgText = "VÁ NA PRIMEIRA PORTA ABERTA E QUANDO APARECER UM ÍCONE ACIMA DO RESPONSÁVEL PELA SALA, APERTE \"" + "E" +"\"";
                    break;
                case 4:
                    msgText = "VOCÊ ACERTOU O PRIMEIRO, PARABÉNS!";
                    break;
                case 5:
                    msgText = "EXISTEM ESCADAS QUE ACESSAM ANDARES COM AS OPERAÇÕES MATEMÁTICAS, SENDO ESSE PRIMEIRO ADIÇÃO, NAS PAREDES ESTARÁ QUAL OPERAÇÃO E ANDAR VOCÊ ESTÁ.";
                    break;
                case 6:
                    msgText = "VOCÊ TERMINA O JOGO QUANDO RESPONDER TODAS AS PERGUNTAS DOS 4 ANDARES DENTRO DO TEMPO LIMITE. DE CADA NÍVEL.";
                    break;
                case 7:
                    msgText = "PARA ABRIR AS PORTAS PRECISA RESPONDER AS PERGUNTAS DA ÚLTIMA PORTA ABERTA\nBOA SORTE!";
                    break;
                case 8:
                    msgText = "VOCÊ ACERTOU, JÁ PODE IR PARA PRÓXIMA PORTA ABERTA";
                    break;
                case 9:
                    msgText = "VOCÊ ACERTOU A PERGUNTA MAIS DIFÍCIL DESSE ANDAR, JÁ PODE SUBIR PARA O PRÓXIMO.";
                    break;
                case 10:
                    msgText = "PARABÉNS, VOCÊ TERMINOU AS TAREFAS";
                    break;
                case 11:
                    msgText = "EM CASO DE ERRO EXISTEM BAÚS ESCONDIDOS NAS PAREDES, ELES RECUPERAM SUA VIDA(SEUS LÁPIS), NÃO DEIXE DE PROCURÁ-LOS.";
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
                    contDialog=11;
                    break;
                case 5:
                    contDialog=6;
                    break;
                case 6:
                    contDialog=7;
                    break;
                case 7:
                    contDialog=8;
                    break;
                case 8:
                    contDialog=0;
                    break;
                case 9:
                    contDialog=0;
                    break;
                case 11:
                    contDialog=5;
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
