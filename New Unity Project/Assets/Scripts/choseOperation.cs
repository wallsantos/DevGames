using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class choseOperation : MonoBehaviour
{
    public GameObject TelaMiniGame;
    private Text questionText;
    private Button[] answerButtons;
    private Button ButtonExit;
    public string answer="";

    private PlayerController playerController;

    private bool playerProximo = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerProximo = true;
        }
    }
    void OnTriggerExit2D(Collider2D other){
        // Detecta se o jogador saiu da área do trigger
        if (other.CompareTag("Player"))
        {
            playerController = other.GetComponent<PlayerController>();
            playerProximo = false;

        }
    }
    void Update(){
        if(playerProximo && Input.GetKeyDown(KeyCode.E)){
            TelaMiniGame.SetActive(true);
            answerButtons = new Button[4];
            answerButtons[0] = GameObject.Find("answerButtons[0]").GetComponent<Button>();
            answerButtons[1] = GameObject.Find("answerButtons[1]").GetComponent<Button>();
            answerButtons[2] = GameObject.Find("answerButtons[2]").GetComponent<Button>();
            answerButtons[3] = GameObject.Find("answerButtons[3]").GetComponent<Button>();
            ButtonExit = GameObject.Find("ButtonExit").GetComponent<Button>();
            questionText = GameObject.Find("TelaMiniGame/Image/questionText").GetComponent<Text>();
            questionText.text = "Selecione a operação que fará:";
            answerButtons[0].GetComponentInChildren<Text>().text = "+ (Mais/Adição)";
            answerButtons[1].GetComponentInChildren<Text>().text = "- (Menos/Subtração)";
            answerButtons[2].GetComponentInChildren<Text>().text = "X (Vezes/Multiplicação)";
            answerButtons[3].GetComponentInChildren<Text>().text = ": (Dividir/Divisão)";
            
            
            foreach(Button btn in answerButtons){
                btn.onClick.RemoveAllListeners();    
                answer = btn.GetComponentInChildren<Text>().text;
                btn.onClick.AddListener(() => CapturarResposta(answer));
                 
            }
            ButtonExit.onClick.RemoveAllListeners();
            ButtonExit.onClick.AddListener(ExitMinigame);
        }
        void CapturarResposta(string resposta)
        {   
            answer = resposta;
            ExitMinigame();
        }

        // Configurar o botão de saída
        Button exitButton = TelaMiniGame.transform.Find("ButtonExit").GetComponent<Button>();
        exitButton.onClick.RemoveAllListeners();
        exitButton.onClick.AddListener(ExitMinigame);
    }
    void ExitMinigame(){
        TelaMiniGame.SetActive(false);
    }
}
