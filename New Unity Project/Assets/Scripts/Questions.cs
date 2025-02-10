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
    private string operacao;
    private choseOperation StringOperacao;

    private GameObject[] Lapis;
    private GameObject[] LapisPreto;
    public GameObject WinCondition;
    public GameObject LoseCondition;

    private GameObject spriteQuestion1;
    private GameObject spriteQuestion2;
    private GameObject spriteQuestion3;
    private GameObject spriteQuestion4;
    
    public GameObject TelaMiniGame;
    private Text questionText;
    private Button[] answerButtons;
    private Button ButtonExit;

    public GameObject DoorOpen;
    public GameObject DoorClose;
    
    private int correctAnswer;

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
        //StringOperacao = other.GetComponent<choseOperation>();
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
            //playerController = null;
        }
    }

    void Interagir()
    {
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

            switch (level){
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
                default:
                    break;
            }
            int setAnswer = 0;
            string fruta1 = (num1==1)? fruta: frutas[fruta];
            string fruta2 = (num2==1)? fruta: frutas[fruta];
            
            correctAnswer = num1 + num2;

            // Exibir a pergunta
            questionText.text = $"  QUANTO É:\n  {num1} {fruta1} \n+{num2} {fruta2}?";

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
                        }else{
                            answer = correctAnswer - fatorRandom + 5;
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
            Button exitButton = TelaMiniGame.transform.Find("ButtonExit").GetComponent<Button>();
            exitButton.onClick.RemoveAllListeners();
            exitButton.onClick.AddListener(ExitMinigame);
        }
    }

    void ResetarBotoes()
    {
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
            DoorOpen.GetComponent<SpriteRenderer>().sortingOrder = 10;
            DoorClose.SetActive(false);
            ResetarBotoes();
            TelaMiniGame.SetActive(false);
            playerController.canMove = true;
            playerController = null;
            FindObjectOfType<Dialogs>().StartDialog(dialogId);
        }
        else
        {
            RemoveLapis();
            if (Lapis[0].activeSelf == false)
            {
                GameOver();
            }
        }
    }

    void RemoveLapis()
    {
        if (Lapis[2].activeSelf) // Verifica se está ativo corretamente
        {
            Lapis[2].SetActive(false);
            LapisPreto[2].SetActive(true);
        }
        if (Lapis[1].activeSelf) // Verifica se está ativo corretamente
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
        // Permitir que o jogador volte a se mover
        if(playerController!=null){
            playerController.canMove = true;
            playerController = null;
        }
    }
    void GameOver()
    {
        LoseCondition.SetActive(true);
    }
}