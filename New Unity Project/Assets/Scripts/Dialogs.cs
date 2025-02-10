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
                    msgText = "Ol�, obrigado por escolher jogar esse jogo que quer te ensinar de forma divertida a matem�tica, nesse breve tutorial ensinaremos voc� a jogar, Aperte E para continuar";
                    break;
                case 2:
                    msgText = "Nosso jogo consiste em teclas WASD para mover-se, o Mouse para clicar nas alternativas e E para interagir";
                    break;
                case 3:
                    msgText = "Nosso primeiro desafio esta na porta, v� at� ela e aperte E para iniciar";
                    break;
                case 4:
                    msgText = "Parab�ns, voc� passou o primeiro desafio";
                    break;
                case 5:
                    msgText = "Agora, sempre que aparecerem novas imagens na tela, significa que voc� tem novos desafios, boa sorte!";
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
