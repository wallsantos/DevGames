using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class Questions : MonoBehaviour
{
    private bool playerNearby = false;
    public SpriteRenderer spriteQuestion;
    public Text messageText;
    public CinemachineVirtualCamera cinemachineCamera;

    public GameObject WinCondition;
    public GameObject LoseCondition;
    private PlayerController playerController;
    public GameObject TelaMiniGame;
    public Text questionText;
    public Button[] answerButtons;
    public Button exitButton;
    public GameObject[] Lapis;
    public GameObject[] LapisPreto;

    public int level;
    private int correctAnswer;
    private int score;
    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Interagir();
        }
        if (playerNearby){
            Camera camera = Camera.main;
            Vector3 worldPosition = spriteQuestion.transform.position;
            Vector3 screenPosition = camera.WorldToScreenPoint(worldPosition);
            Vector3 offset = new Vector3(20, 30, 0);  // Ajuste a posição do texto
            messageText.rectTransform.position = screenPosition + offset;
        }
        if(playerNearby && Input.GetKeyDown(KeyCode.F)){
            messageText.text = "HÁ UMA NOVA PERGUNTA, APERTE E PARA INTERAGIR";
            WinCondition.SetActive(false);
            // Permitir que o jogador volte a se mover
            playerController.canMove = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            spriteQuestion.sortingOrder = 4;
            messageText.text = "HÁ UMA NOVA PERGUNTA, APERTE E PARA INTERAGIR";
            playerController = other.GetComponent<PlayerController>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Detecta se o jogador saiu da área do trigger
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            spriteQuestion.sortingOrder = 0;
            messageText.text = "";
            playerController = null;
        }
    }

    private void Interagir()
    {
        playerController.canMove = false;
        TelaMiniGame.SetActive(true);
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
                    fatorRandom = Random.Range(3,6);
                    break;
                case 3:
                    num1 = Random.Range(100, 500);
                    num2 = Random.Range(100, 500);
                    fatorRandom = Random.Range(7,13);
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

    private void CheckAnswer(int selectedAnswer)
    {
        if (selectedAnswer == correctAnswer)
        {
            WinGame();
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

    private void RemoveLapis()
    {
        for (int i = Lapis.Length - 1; i >= 0; i--)
        {
            if (Lapis[i].activeSelf)
            {
                Lapis[i].SetActive(false);
                LapisPreto[i].SetActive(true);
                break;
            }
        }
    }
    void WinGame(){
        messageText.text = "";
        WinCondition.SetActive(true);

        // Fechar o minigame
        TelaMiniGame.SetActive(false);
    }
    void ExitMinigame(){
        // Fechar o minigame
        TelaMiniGame.SetActive(false);
        // Permitir que o jogador volte a se mover
        playerController.canMove = true;
    }
    private void GameOver()
    {
        messageText.text = "";
        LoseCondition.SetActive(true);
    }
}