using UnityEngine;

public class Questions : MonoBehaviour
{
    private bool playerNearby = false; // Verifica se o jogador está perto

    void Update()
    {
        // Verifica se o jogador está próximo e pressionou "E"
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Interagir();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Detecta se o jogador entrou na área do trigger
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            Debug.Log("Você está perto da barraca. Pressione 'E' para interagir.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Detecta se o jogador saiu da área do trigger
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            Debug.Log("Você saiu da área da barraca.");
        }
    }

    private void Interagir()
    {
        // Aqui você pode implementar o comportamento da interação
        Debug.Log("Você interagiu com a barraca!");

        // Exemplo de opções:
        int escolha = Random.Range(0, 2); // Escolhe entre 0 ou 1
        if (escolha == 0)
        {
            Debug.Log("A barraca te faz uma pergunta!");
            // Exemplo: abrir um menu com uma pergunta
        }
        else
        {
            Debug.Log("Há um desafio! Deseja aceitar?");
            // Exemplo: abrir um menu de confirmação
        }
    }
}