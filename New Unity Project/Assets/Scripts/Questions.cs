using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class Questions : MonoBehaviour
{
    private bool playerNearby = false;
    private PlayerController playerController;
    private SpriteRenderer spriteRenderer;
    public int dialogId;
    private Dictionary<string, string> operacaoAtualizada;
    private string auxOperacao="+ : ADI��O";

    private GameObject[] Lapis;
    private GameObject[] LapisPreto;
    public GameObject WinCondition;
    public GameObject LoseCondition;
    private Text Nota;

    private GameObject spriteQuestion1;
    private GameObject spriteQuestion2;
    private GameObject spriteQuestion3;
    private GameObject spriteQuestion4;
    
    public GameObject TelaMiniGame;
    private Text questionText;
    private Button[] answerButtons;
    private Button ButtonExit;
    private Text L1L3;
    private Text L2L4;

    public GameObject DoorOpen;
    public GameObject DoorClose;
    
    private int correctAnswer;
    private static int checarleveis=0;
    private static int NotaTotal=0;

    public int level;

    void Start(){
        Lapis = new GameObject[3];
        Lapis[0] = GameObject.Find("pencil0");
        Lapis[1] = GameObject.Find("pencil1");
        Lapis[2] = GameObject.Find("pencil2");
        //Lapis Preto
        LapisPreto = new GameObject[3];
        LapisPreto[0] = GameObject.Find("pencilB0");
        LapisPreto[1] = GameObject.Find("pencilB1");
        LapisPreto[2] = GameObject.Find("pencilB2");

        spriteQuestion1 = GameObject.Find("spriteQuestion1");
        spriteQuestion2 = GameObject.Find("spriteQuestion2");
        spriteQuestion3 = GameObject.Find("spriteQuestion3");
        spriteQuestion4 = GameObject.Find("spriteQuestion4");

        Nota = GameObject.Find("Nota").GetComponent<Text>();
        Nota.text = NotaTotal.ToString();

        L1L3 = GameObject.Find("L1Text").GetComponent<Text>();
        L2L4 = GameObject.Find("L2Text").GetComponent<Text>();
        operacaoAtualizada = new Dictionary<string, string>(){
            { "+ : ADI��O", "+ : ADI��O"},
            { "- : SUBTRA��O", "- : SUBTRA��O" },
            { "X : MULTIPLICA��O", "X : MULTIPLICA��O" },
            { "/ : DIVIS�O", "/ : DIVIS�O" }
        };
    }
    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Interagir();
        }
        if (playerNearby){
            
        }
        /*if(playerNearby && Input.GetKeyDown(KeyCode.F)){
            // Permitir que o jogador volte a se mover
            playerController.canMove = true;
        }*/
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            spriteQuestion1.GetComponent<SpriteRenderer>().sortingOrder = 10;
            spriteQuestion2.GetComponent<SpriteRenderer>().sortingOrder = 10;
            spriteQuestion3.GetComponent<SpriteRenderer>().sortingOrder = 10;
            spriteQuestion4.GetComponent<SpriteRenderer>().sortingOrder = 10;
            playerController = other.gameObject.GetComponent<PlayerController>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Detecta se o jogador saiu da �rea do trigger
        if (other.CompareTag("Player"))
        {
            spriteQuestion1.GetComponent<SpriteRenderer>().sortingOrder = 0;
            spriteQuestion2.GetComponent<SpriteRenderer>().sortingOrder = 0;
            spriteQuestion3.GetComponent<SpriteRenderer>().sortingOrder = 0;
            spriteQuestion4.GetComponent<SpriteRenderer>().sortingOrder = 0;
            playerNearby = false;
        }
    }

    void Interagir()
    {
        if(playerNearby==true){
            playerController.canMove = false;
            TelaMiniGame.SetActive(true);
            answerButtons = new Button[4];
            answerButtons[0] = GameObject.Find("answerButtons[0]").GetComponent<Button>();
            answerButtons[1] = GameObject.Find("answerButtons[1]").GetComponent<Button>();
            answerButtons[2] = GameObject.Find("answerButtons[2]").GetComponent<Button>();
            answerButtons[3] = GameObject.Find("answerButtons[3]").GetComponent<Button>();
            questionText = GameObject.Find("TelaMiniGame/Image/questionText").GetComponent<Text>();
            ButtonExit = GameObject.Find("ButtonExit").GetComponent<Button>();
            GenerateQuestion();
        }

        void GenerateQuestion()
        {
            ResetarBotoes();
            // Gerar n�meros aleat�rios para a pergunta
            int num1 = 1;
            int num2 = 1;
            int fatorRandom=1; //Usado para um set das alternativas, muda no switch conforme o player muda de level
            System.Random random = new System.Random();

            Dictionary<string, string> frutas = new Dictionary<string,string>(){
                {"ABACAXI", "ABACAXIS"},
                {"BANANA", "BANANAS"},
                {"LARANJA", "LARANJAS"}
            };
            List<string> chaves = new List<string>(frutas.Keys);
            string fruta = chaves[random.Next(chaves.Count)];
            switch (level){
                case 0:
                    num1 = 1;
                    num2 = 1;  
                    break;
                case 1:
                    num1 = Random.Range(1, 5);
                    num2 = Random.Range(1, 5);
                    break;
                case 2:
                    num1 = Random.Range(10, 50);
                    num2 = Random.Range(10, 50);
                    fatorRandom = 10;
                    break;
                case 3:
                    num1 = Random.Range(100, 500);
                    num2 = Random.Range(100, 500);
                    fatorRandom = 10;
                    break;
                case 4:
                    num1 = Random.Range(500, 999);
                    num2 = Random.Range(500, 999);
                    fatorRandom = 10;
                    break;
                default:
                    break;
            }
            int setAnswer = 0;
            string fruta1 = (num1==1)? fruta: frutas[fruta];
            string fruta2 = (num2==1)? fruta: frutas[fruta];
            switch(operacaoAtualizada[auxOperacao]){
                case "- : SUBTRA��O":
                    correctAnswer = num1 - num2;
                    // Exibir a pergunta
                    questionText.text = $"  QUANTO �:\n  {num1} {fruta1} \n-{num2} {fruta2}?";
                    break;
                case "X : MULTIPLICA��O":
                    correctAnswer = num1 * num2;
                    // Exibir a pergunta
                    questionText.text = $"  QUANTO �:\n  {num1} {fruta1} \nx{num2} {fruta2}?";
                    break;
                case "/ : DIVIS�O":
                    correctAnswer = num1 / num2;
                    // Exibir a pergunta
                    questionText.text = $"  QUANTO �:\n  {num1} {fruta1} \n:{num2} {fruta2}?";
                    break;
                default:
                    correctAnswer = num1 + num2;
                    // Exibir a pergunta
                    questionText.text = $"  QUANTO �:\n  {num1} {fruta1} \n+{num2} {fruta2}?";
                    break;
            }
            if(this.gameObject.name == "wooden_door_0"){
                L1L3.text = "";
                L2L4.text = "";
            }else if(this.gameObject.name == "Kid"){
                L1L3.text = questionText.text;
                
            }else if(this.gameObject.name == "Man"){
                L2L4.text = questionText.text;
            }

            // Gerar respostas aleat�rias e colocar a correta em um bot�o aleat�rio
            int correctButtonIndex = Random.Range(0, answerButtons.Length);
            for (int i = 0; i < answerButtons.Length; i++)
            {
                int answer;
                List<int> usedAnswers = new List<int>(); // Lista para armazenar respostas j� usadas

                if (i == correctButtonIndex)
                {
                    answer = correctAnswer;
                }
                else
                {
                    do
                    {
                        if(setAnswer==0){
                            //answer = Random.Range(1, 20); // Gerar resposta aleat�ria
                            answer = correctAnswer + fatorRandom;
                            setAnswer = 1;
                        }else if(setAnswer==1){
                            answer = correctAnswer - fatorRandom + 5;
                            setAnswer = 2;
                        }else{
                            answer = correctAnswer - fatorRandom;
                        }
                    } while (answer == correctAnswer || usedAnswers.Contains(answer)); // Evitar duplica��es
                }

                // Adicionar a resposta � lista de respostas usadas
                usedAnswers.Add(answer);

                // Atualizar o texto do bot�o e adicionar o evento de clique
                answerButtons[i].GetComponentInChildren<Text>().text = answer.ToString();
                int selectedAnswer = answer; // Capturar valor para o evento
                
                answerButtons[i].onClick.RemoveAllListeners();
                answerButtons[i].onClick.AddListener(() => CheckAnswer(selectedAnswer));
            }

            // Configurar o bot�o de sa�da
            ButtonExit.onClick.RemoveAllListeners();
            ButtonExit.onClick.AddListener(ExitMinigame);
        }
    }

    void ResetarBotoes()
    {
        foreach (Button botao in answerButtons)
        {
            ColorBlock cb = botao.colors;
            cb.normalColor = Color.white; // Cor padr�o (mude conforme necess�rio)
            botao.colors = cb;

            botao.interactable = true; // Garante que os bot�es fiquem interativos novamente
            EventSystem.current.SetSelectedGameObject(null); // Remove a sele��o atual
        }
    }

    void CheckAnswer(int selectedAnswer)
    {
        if (selectedAnswer == correctAnswer)
        {
            if(checarleveis==level){
                checarleveis=checarleveis + 1;
                switch(level){
                    case 0:
                        NotaTotal=0;
                        break;
                    case 1:
                        NotaTotal+=2;
                        break;
                    case 2:
                        NotaTotal+=2;
                        break;
                    case 3:
                        NotaTotal+=2;
                        break;
                    case 4://Caso especial, trata-se do mais dificil
                        NotaTotal+=4;
                        break;
                    default:
                        break;
                }
                Nota.text = NotaTotal.ToString();
            }
            DoorOpen.GetComponent<SpriteRenderer>().sortingOrder = 10;
            DoorClose.SetActive(false);
            ResetarBotoes();
            playerNearby = false;
            FindObjectOfType<Dialogs>().StartDialog(dialogId);
            ExitMinigame();
        }
        else
        {
            NotaTotal-=1;
            Nota.text = NotaTotal.ToString();
            RemoveLapis();
            if (Lapis[0].activeSelf == false)
            {
                GameOver();
            }
        }
    }

    void RemoveLapis()
    {
        if (Lapis[2].activeSelf) // Verifica se est� ativo corretamente
        {
            Lapis[2].SetActive(false);
            LapisPreto[2].SetActive(true);
        }
        if (Lapis[1].activeSelf) // Verifica se est� ativo corretamente
        {
            Lapis[1].SetActive(false);
            LapisPreto[1].SetActive(true);
        }
    }
    void WinGame(){
        //WinCondition.SetActive(true);

        // Fechar o minigame
        TelaMiniGame.SetActive(false);
    }
    void ExitMinigame(){
        // Fechar o minigame
        TelaMiniGame.SetActive(false);
        L1L3.text = "";
        L2L4.text = "";
        // Permitir que o jogador volte a se mover
        playerController.canMove = true;
        playerController = null;
    }
    void GameOver()
    {
        LoseCondition.SetActive(true);
    }
    public void operacaoSelecionada(string op){
        auxOperacao = op;
    }
}