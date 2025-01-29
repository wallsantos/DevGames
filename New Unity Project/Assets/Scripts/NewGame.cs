using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    public GameObject creditos;
    private bool isCredit = false;

    public void start(){
        creditos.SetActive(false);
    }
    void Update(){
        if(isCredit && (Input.GetMouseButtonDown(0)||Input.anyKeyDown)){
            CloseCredits();
        }
    }
    public void ShowCredits(){
        creditos.SetActive(true);
        isCredit = true;
    }
    public void CloseCredits(){
        creditos.SetActive(false);
        isCredit = false;
    }
    public void RestartGame(){
        SceneManager.LoadScene("Project2D");
    }

    public void ExitGame(){
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Para o jogo no editor
        #else
            Application.Quit(); // Fecha o jogo na versão compilada
        #endif
    }

}
