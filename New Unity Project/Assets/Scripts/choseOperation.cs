using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class choseOperation : MonoBehaviour
{
    public GameObject TelaMiniGame;
    private Text questionText;
    private Button[] operationButtons;
    private Button ButtonExit;
    public string operation="";
    private GameObject QuestIcon;
    private GameObject Computer;
    private GameObject ComputerScreen;
    public int dialogId;

    public GameObject DoorOpen;
    public GameObject DoorClose;

    private PlayerController playerController;

    private bool playerProximo = false;

    void Start(){
        operation="";
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerController = other.gameObject.GetComponent<PlayerController>();
            playerProximo = true;
            QuestIcon = GameObject.Find("QuestIcon");
            Computer = GameObject.Find("Computador");
            ComputerScreen = GameObject.Find("TelaComputador");
            if(this.gameObject.name=="Computador"){
                ComputerScreen.GetComponent<SpriteRenderer>().sortingOrder = 10;
            }else if(this.gameObject.name=="QuestIcon"){
                if(QuestIcon!=null && operation==""){
                    QuestIcon.GetComponent<SpriteRenderer>().sortingOrder = 10;
                }
            }
        }
    }
    void OnTriggerExit2D(Collider2D other){
        // Detecta se o jogador saiu da área do trigger
        if (other.CompareTag("Player"))
        {
            playerProximo = false;
            if(QuestIcon!=null){
                QuestIcon.GetComponent<SpriteRenderer>().sortingOrder = 0;
            }
            ComputerScreen.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
    }
    void Update(){
        if(playerProximo && Input.GetKeyDown(KeyCode.E)){
            if(playerController!=null){
                playerController.canMove = false;
            }
            TelaMiniGame.SetActive(true);
            operationButtons = new Button[4];
            operationButtons[0] = GameObject.Find("answerButtons[0]").GetComponent<Button>();
            operationButtons[1] = GameObject.Find("answerButtons[1]").GetComponent<Button>();
            operationButtons[2] = GameObject.Find("answerButtons[2]").GetComponent<Button>();
            operationButtons[3] = GameObject.Find("answerButtons[3]").GetComponent<Button>();
            ButtonExit = GameObject.Find("ButtonExit").GetComponent<Button>();
            ResetarBotoes();
            questionText = GameObject.Find("TelaMiniGame/Image/questionText").GetComponent<Text>();
            questionText.text = "SELECIONE QUAL OPERAÇÃO QUER NO JOGO:";
            operationButtons[0].GetComponentInChildren<Text>().text = "+ : ADIÇÃO";
            operationButtons[1].GetComponentInChildren<Text>().text = "- : SUBTRAÇÃO";
            operationButtons[2].GetComponentInChildren<Text>().text = "X : MULTIPLICAÇÃO";
            operationButtons[3].GetComponentInChildren<Text>().text = "/ : DIVISÃO";
            
            
            foreach(Button btn in operationButtons){
                btn.onClick.RemoveAllListeners();    
                string operationCopy=btn.GetComponentInChildren<Text>().text;
                btn.onClick.AddListener(() => CapturarResposta(operationCopy));
                 
            }
            ButtonExit.onClick.RemoveAllListeners();
            ButtonExit.onClick.AddListener(ExitMinigame);
        }
        void CapturarResposta(string resposta)
        {   
            operation = resposta;
            ExitMinigame();
        }

        // Configurar o botão de saída
        Button exitButton = TelaMiniGame.transform.Find("ButtonExit").GetComponent<Button>();
        exitButton.onClick.RemoveAllListeners();
        exitButton.onClick.AddListener(ExitWithoutChoose);
    }
    void ResetarBotoes()
    {
        foreach (Button btn in operationButtons)
        {
            ColorBlock cb = btn.colors;
            cb.normalColor = Color.white; // Cor padrão (mude conforme necessário)
            btn.colors = cb;

            btn.interactable = true; // Garante que os botões fiquem interativos novamente
            EventSystem.current.SetSelectedGameObject(null); // Remove a seleção atual
        }
    }
    void ExitMinigame(){
        if(playerController!=null){
            playerController.canMove = true;
            playerController = null;
        }
        TelaMiniGame.SetActive(false);
        FindObjectOfType<Dialogs>().StartDialog(dialogId, operation);
        playerProximo = false;
        if(DoorClose.activeSelf){
            QuestIcon.SetActive(false);
            DoorOpen.GetComponent<SpriteRenderer>().sortingOrder = 10;
            DoorClose.SetActive(false);
        }
    }
    void ExitWithoutChoose(){
        if(playerController!=null){
            playerController.canMove = true;
            playerController = null;
        }
        TelaMiniGame.SetActive(false);
        playerProximo = false;
    }
}
