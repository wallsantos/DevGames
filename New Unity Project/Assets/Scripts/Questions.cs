using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Questions : MonoBehaviour
{
    private bool playerNearby = false;
    public SpriteRenderer spriteQuestion;
    public Text messageText;
    public CinemachineVirtualCamera cinemachineCamera;

    private PlayerController playerController;
    public Text questionText;
    public Button[] answerButtons;
    public Text scoreText;

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
            Vector3 offset = new Vector3(20, 5, 0);  // Ajuste a posição do texto
            messageText.rectTransform.position = screenPosition + offset;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            spriteQuestion.sortingOrder = 4;
            messageText.text = "Há uma nova missão Aperte E para interagir";
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

    private void Interagir(){
        playerController.canMove = false;
        spriteQuestion.sortingOrder = 0;
        messageText.text = "";
        score = 0;
        GenerateQuestion();

        void GenerateQuestion(){
            // Gerar números aleatórios para a pergunta
            int num1 = Random.Range(1, 10);
            int num2 = Random.Range(1, 10);
            correctAnswer = num1 + num2;

            // Exibir a pergunta
            questionText.text = "Quanto é " + num1 + " + " + num2 + "?";

            // Gerar respostas aleatórias e colocar a correta em um botão aleatório
            int correctButtonIndex = Random.Range(0, answerButtons.Length);
            for (int i = 0; i < answerButtons.Length; i++)
            {
                int answer;
                if (i == correctButtonIndex)
                {
                    answer = correctAnswer;
                }
                else
                {
                    do
                    {
                        answer = Random.Range(1, 20);
                    } while (answer == correctAnswer); // Evitar duplicar a resposta correta
                }

                // Atualizar o texto do botão e adicionar o evento de clique
                answerButtons[i].GetComponentInChildren<Text>().text = answer.ToString();
                int selectedAnswer = answer; // Capturar valor para o evento
                answerButtons[i].onClick.RemoveAllListeners();
                answerButtons[i].onClick.AddListener(() => CheckAnswer(selectedAnswer));
            }
        }

        void CheckAnswer(int selectedAnswer){
            if (selectedAnswer == correctAnswer)
            {
                score += 10;
                scoreText.text = "Pontuação: " + score;
                GenerateQuestion();
                playerController.canMove = true;
            }
            else
            {
                score -= 5;
                scoreText.text = "Pontuação: " + score;
            }
        }
    }
}