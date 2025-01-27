using UnityEngine;

public class Questions : MonoBehaviour
{
    private bool playerNearby = false; // Verifica se o jogador est� perto

    void Update()
    {
        // Verifica se o jogador est� pr�ximo e pressionou "E"
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Interagir();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Detecta se o jogador entrou na �rea do trigger
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            Debug.Log("Voc� est� perto da barraca. Pressione 'E' para interagir.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Detecta se o jogador saiu da �rea do trigger
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            Debug.Log("Voc� saiu da �rea da barraca.");
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