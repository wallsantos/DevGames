// Prot�tipo simples de um jogo educativo estilo Matific
using UnityEngine;
using UnityEngine.UI;

public class MathGame : MonoBehaviour
{
    public Text questionText; // Texto para exibir a pergunta
    public Button[] answerButtons; // Bot�es para as respostas
    public Text scoreText; // Texto para exibir o placar

    private int correctAnswer;
    private int score;

    void Start()
    {
        score = 0;
        GenerateQuestion();
    }

    void GenerateQuestion()
    {
        // Gerar n�meros aleat�rios para a pergunta
        int num1 = Random.Range(1, 10);
        int num2 = Random.Range(1, 10);
        correctAnswer = num1 + num2;

        // Exibir a pergunta
        questionText.text = "Quanto � " + num1 + " + " + num2 + "?";

        // Gerar respostas aleat�rias e colocar a correta em um bot�o aleat�rio
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

            // Atualizar o texto do bot�o e adicionar o evento de clique
            answerButtons[i].GetComponentInChildren<Text>().text = answer.ToString();
            int selectedAnswer = answer; // Capturar valor para o evento
            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => CheckAnswer(selectedAnswer));
        }
    }

    void CheckAnswer(int selectedAnswer)
    {
        if (selectedAnswer == correctAnswer)
        {
            score += 10;
            scoreText.text = "Pontua��o: " + score;
            GenerateQuestion();
        }
        else
        {
            score -= 5;
            scoreText.text = "Pontua��o: " + score;
        }
    }
}
