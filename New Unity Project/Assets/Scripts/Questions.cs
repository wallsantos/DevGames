using UnityEngine;
using UnityEngine.UI;

public class Questions : MonoBehaviour
{
    private bool playerNearby = false;
    public SpriteRenderer spriteQuestion;
    public Text messageText;
    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Interagir();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            spriteQuestion.sortingOrder = 4;
            messageText.text = "Teste";
            messageText.fontSize = 12;
            messageText.alignment = TextAnchor.MiddleCenter;
            RectTransform rect = messageText.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(200,50);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Detecta se o jogador saiu da �rea do trigger
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            spriteQuestion.sortingOrder = 0;
        }
    }

    private void Interagir()
    {
        // Aqui voc� pode implementar o comportamento da intera��o
        Debug.Log("Voc� interagiu com a barraca!");

        // Exemplo de op��es:
        int escolha = Random.Range(0, 2); // Escolhe entre 0 ou 1
        if (escolha == 0)
        {
            Debug.Log("A barraca te faz uma pergunta!");
            // Exemplo: abrir um menu com uma pergunta
        }
        else
        {
            Debug.Log("H� um desafio! Deseja aceitar?");
            // Exemplo: abrir um menu de confirma��o
        }
    }
}