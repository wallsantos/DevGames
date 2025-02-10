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
    
    
    void Start()
    {
        dialogBox = GameObject.Find("Dialog Box");
        textDialog = GameObject.Find("CMPrincipal/Dialog Box/Canvas/textDialog").GetComponent<Text>();
    }
    void Update()
    {
        if(contDialog>1){
            dialogBox.SetActive(true);
            if(Input.GetKeyDown(KeyCode.E)){
                contDialog = dialogs(contDialog);
                dialogBox.SetActive(false);
            }
        }else if(contDialog==1){
            textDialog.text= "Olá, obrigado por escolher jogar esse jogo que quer te ensinar de forma divertida a matemática, nesse breve tutorial ensinaremos você a jogar, Aperte E para continuar";
            contDialog = 2;
        }
    }

    public void StartDialog(int id)
    {
        contDialog = id;
        dialogBox.SetActive(true);
        dialogs(contDialog);
    }

    private int dialogs(int proximoDialogo){
        switch(proximoDialogo){
            case 1:
                textDialog.text= "Olá, obrigado por escolher jogar esse jogo que quer te ensinar de forma divertida a matemática, nesse breve tutorial ensinaremos você a jogar, Aperte E para continuar";
                return 2;
            case 2:
                textDialog.text= "Nosso jogo consiste em teclas WASD para mover-se, o Mouse para clicar nas alternativas e E para interagir";
                return 3;
            case 3:
                textDialog.text= "Nosso primeiro desafio esta na porta, vá até ela e aperte E para iniciar";
                return 4;
            case 4:
                textDialog.text= "Parabéns, você passou o primeiro desafio";
                return 0;
            case 5:
                textDialog.text= "Agora, sempre que aparecerem novas imagens na tela, significa que você tem novos desafios, boa sorte!";
                return proximoDialogo+1;
            default:
                return 0;
        }
    }
}
