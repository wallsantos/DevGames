using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Globalization;

public class Questions : MonoBehaviour
{
    private bool playerNearby = false;
    private PlayerController playerController;
    private SpriteRenderer spriteRenderer;
    public int dialogId;

    private GameObject Bau;
    public GameObject WinCondition;
    public GameObject LoseCondition;
    private Text Nota;
    private Text LevelAtual;

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
    public static int checarleveis=1;
    public static float NotaTotal=0.0f;

    public int level;
    private bool isGodMode = false;

    void Start(){
        spriteQuestion1 = GameObject.Find("spriteQuestion1");
        spriteQuestion2 = GameObject.Find("spriteQuestion2");
        spriteQuestion3 = GameObject.Find("spriteQuestion3");
        spriteQuestion4 = GameObject.Find("spriteQuestion4");

        Nota = GameObject.Find("Nota").GetComponent<Text>();
        if(Nota.text=="-"){
            Nota.text = "-";
        }
        L1L3 = GameObject.Find("L1Text").GetComponent<Text>();
        L2L4 = GameObject.Find("L2Text").GetComponent<Text>();
        LevelAtual = GameObject.Find("AtLevel").GetComponent<Text>();
    }
    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Interagir();
        }
        if (playerNearby){
            
        }
        if(Input.GetKeyDown(KeyCode.G)){
            ToggleGodMode();
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
            playerController = other.gameObject.GetComponent<PlayerController>();
            switch(this.gameObject.name){
                case "nivel1":
                    spriteQuestion1.GetComponent<SpriteRenderer>().sortingOrder = 10;
                    break;
                case "nivel2":
                    spriteQuestion2.GetComponent<SpriteRenderer>().sortingOrder = 10;
                    break;
                case "nivel3":
                    spriteQuestion3.GetComponent<SpriteRenderer>().sortingOrder = 10;
                    break;
                case "nivel4":
                    spriteQuestion4.GetComponent<SpriteRenderer>().sortingOrder = 10;
                    break;
                default:
                    break;

            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Detecta se o jogador saiu da área do trigger
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
            // Gerar números aleatórios para a pergunta
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
            switch (level) {
                // Adição
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

                // Subtração
                case 5:
                    num1 = Random.Range(4, 11);
                    num2 = Random.Range(1, 5);
                    break;
                case 6:
                    num1 = Random.Range(30, 50);
                    num2 = Random.Range(10, 31);
                    fatorRandom = 10;
                    break;
                case 7:
                    num1 = Random.Range(400, 500);
                    num2 = Random.Range(100, 401);
                    fatorRandom = 10;
                    break;
                case 8:
                    num1 = Random.Range(500, 999);
                    num2 = Random.Range(100, 501);
                    fatorRandom = 10;
                    break;

                // Multiplicação
                case 1:
                case 9: // Começa multiplicação
                    num1 = Random.Range(1, 6);
                    num2 = Random.Range(1, 6);
                    break;
                case 10:
                    num1 = Random.Range(2, 11);
                    num2 = Random.Range(2, 11);
                    fatorRandom = 1;
                    break;
                case 11:
                    num1 = Random.Range(10, 26);
                    num2 = Random.Range(2, 11);
                    fatorRandom = 10;
                    break;
                case 12:
                    num1 = Random.Range(20, 51);
                    num2 = Random.Range(5, 21);
                    fatorRandom = 20;
                    break;

                // Divisão
                case 13:
                    num1 = Random.Range(5, 26);
                    num2 = Random.Range(1, 6);
                    while (num1 % num2 != 0) {
                        num2 = Random.Range(1, 6);
                    }
                    fatorRandom = 1;
                    break;
                case 14:
                    num1 = Random.Range(20, 100);
                    num2 = Random.Range(2, 11);
                    while (num1 % num2 != 0) {
                        num2 = Random.Range(2, 11);
                    }
                    fatorRandom = 2;
                    break;
                case 15:
                    num1 = Random.Range(100, 501);
                    num2 = Random.Range(5, 21);
                    // Garantir que num1 seja divisível por num2
                    num1 = num2 * Random.Range(5, 26);  // Multiplicar num2 por um valor aleatório para garantir divisibilidade
                    fatorRandom = 3;
                    break;

                case 16:
                    num1 = Random.Range(200, 1000);
                    num2 = Random.Range(10, 51);
                    // Garantir que num1 seja divisível por num2
                    num1 = num2 * Random.Range(4, 21);  // Multiplicar num2 por um valor aleatório para garantir divisibilidade
                    fatorRandom = 3;
                    break;

                default:
                    break;
            }
            int setAnswer = 0;
            string fruta1 = (num1==1)? fruta: frutas[fruta];
            string fruta2 = (num2==1)? fruta: frutas[fruta];
            switch(level){
                case 1:
                case 2:
                case 3:
                case 4:
                    correctAnswer = num1 + num2;
                    // Exibir a pergunta
                    questionText.text = $"  QUANTO É:\n  {num1} {fruta1} \n+{num2} {fruta2}?";
                    break;
                case 5:
                case 6:
                case 7:
                case 8:
                    correctAnswer = num1 - num2;
                    // Exibir a pergunta
                    questionText.text = $"  QUANTO É:\n  {num1} {fruta1} \n-{num2} {fruta2}?";
                    break;
                case 9:
                case 10:
                case 11:
                case 12:
                    correctAnswer = num1 * num2;
                    // Exibir a pergunta
                    questionText.text = $"  QUANTO É:\n  {num1} {fruta1} \nx{num2} {fruta2}?";
                    break;
                case 13:
                case 14:
                case 15:
                case 16:
                    correctAnswer = num1 / num2;
                    // Exibir a pergunta
                    questionText.text = $"  QUANTO É:\n  {num1} {fruta1} \n:{num2} {fruta2}?";
                    break;
                default:
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

            // Gerar respostas aleatórias e colocar a correta em um botão aleatório
            int correctButtonIndex = Random.Range(0, answerButtons.Length);
            for (int i = 0; i < answerButtons.Length; i++)
            {
                int answer;
                List<int> usedAnswers = new List<int>(); // Lista para armazenar respostas já usadas

                if (i == correctButtonIndex)
                {
                    answer = correctAnswer;
                }
                else
                {
                    do
                    {
                        if(setAnswer==0){
                            //answer = Random.Range(1, 20); // Gerar resposta aleatória
                            answer = correctAnswer + fatorRandom;
                            setAnswer = 1;
                        }else if(setAnswer==1){
                            answer = correctAnswer - fatorRandom + 5;
                            setAnswer = 2;
                        }else{
                            answer = correctAnswer - fatorRandom;
                        }
                    } while (answer == correctAnswer || usedAnswers.Contains(answer)); // Evitar duplicações
                }

                // Adicionar a resposta à lista de respostas usadas
                usedAnswers.Add(answer);

                // Atualizar o texto do botão e adicionar o evento de clique
                answerButtons[i].GetComponentInChildren<Text>().text = answer.ToString();
                int selectedAnswer = answer; // Capturar valor para o evento
                
                answerButtons[i].onClick.RemoveAllListeners();
                answerButtons[i].onClick.AddListener(() => CheckAnswer(selectedAnswer));
            }

            // Configurar o botão de saída
            ButtonExit.onClick.RemoveAllListeners();
            ButtonExit.onClick.AddListener(ExitMinigame);
        }
    }

    public void ToggleGodMode()
    {
        if (answerButtons == null || answerButtons.Length == 0)
        {
            return;
        }

        isGodMode = !isGodMode;
        UpdateButtonColors();
    }
    void UpdateButtonColors(){
        int correctButtonIndex = -1;
        string respostaCorreta = correctAnswer.ToString();
        string alternativa = "";
        // Identifica o botão correto
        for (int i = 0; i < answerButtons.Length; i++)
        {
            alternativa = answerButtons[i].GetComponentInChildren<Text>().text;
            if (alternativa == respostaCorreta && correctButtonIndex == -1 && i != -1)
            {
                correctButtonIndex = i;
            }
        }

        if (isGodMode)
        {
            for (int i = 0; i < answerButtons.Length; i++)
            {
                if(correctButtonIndex != -1){
                    Button button = answerButtons[i].GetComponent<Button>();
                    ColorBlock colors = button.colors;
                    colors.normalColor = (i == correctButtonIndex) ? Color.green : Color.red;
                    button.colors = colors;

                    // Forçar a atualização chamando esta linha:
                    button.GetComponent<Image>().color = colors.normalColor;
                }
            }
        }
        else
        {
            for (int i = 0; i < answerButtons.Length; i++)
            {
                Button button = answerButtons[i].GetComponent<Button>();
                ColorBlock colors = button.colors;
                colors.normalColor = Color.white; // Cor normal de volta para branco
                button.colors = colors;

                // Forçar atualização da cor do botão
                button.GetComponent<Image>().color = colors.normalColor;
            }
        }
        respostaCorreta = "";
    }

    void ResetarBotoes()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            ColorBlock colors = button.colors;
            colors.normalColor = Color.white; // Cor normal de volta para branco
            button.colors = colors;

            // Forçar atualização da cor do botão
            button.GetComponent<Image>().color = colors.normalColor;
        }
        foreach (Button botao in answerButtons)
        {
            ColorBlock cb = botao.colors;
            cb.normalColor = Color.white; // Cor padrão (mude conforme necessário)
            botao.colors = cb;

            botao.interactable = true; // Garante que os botões fiquem interativos novamente
            EventSystem.current.SetSelectedGameObject(null); // Remove a seleção atual
        }
    }

    void CheckAnswer(int selectedAnswer)
    {
        if (selectedAnswer == correctAnswer)
        {
            if(checarleveis == 16){
                if(NotaTotal>5.5){
                    WinGame();
                }else{
                    GameOver();
                }
            }
            if(checarleveis==level){
                checarleveis=checarleveis + 1;
                if(checarleveis != 16){
                    LevelAtual.text = "LEVEL ATUAL: " + checarleveis + "/16";
                }
                switch (level)
                {
                    case 4:
                    case 8:
                    case 12:
                    case 16:
                        NotaTotal += 1.0f;
                        break;

                    case int n when (n >= 1 && n <= 15 && n != 4 && n != 8 && n != 12 && n != 16):
                        NotaTotal += 0.5f;
                        break;
                }
                Nota.text = NotaTotal.ToString("F1", new CultureInfo("pt-BR"));
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
            if(NotaTotal>=0.5f){
                NotaTotal-=0.5f;
                Nota.text = NotaTotal.ToString("F1", new CultureInfo("pt-BR"));
            }else{
                Nota.text = NotaTotal.ToString("F1", new CultureInfo("pt-BR"));
            }
            RemoveLapis();
        }
    }

    void RemoveLapis()
    {
        if(playerController.EndGame()){
            ExitMinigame();
            GameOver();
        }else{
            playerController.upUpdateLife(false);
        }
    }
    void WinGame(){
        WinCondition.SetActive(true);

        // Fechar o minigame
        TelaMiniGame.SetActive(false);
    }
    void ExitMinigame(){
        // Fechar o minigame
        ResetarBotoes();
        TelaMiniGame.SetActive(false);
        L1L3.text = "";
        L2L4.text = "";
        // Permitir que o jogador volte a se mover
        playerController.canMove = true;
        playerController = null;
        isGodMode = false;
        ResetarBotoes();
    }
    void GameOver()
    {
        LoseCondition.SetActive(true);
    }
}