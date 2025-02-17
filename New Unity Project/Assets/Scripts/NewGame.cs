using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    public GameObject creditos;
    public GameObject PauseMenu;
    private bool isCredit = false;
    private bool isPaused = false;

    public void start(){
        creditos.SetActive(false);
    }
    void Update(){
        if(isCredit && (Input.GetMouseButtonDown(0)||Input.anyKeyDown)){
            CloseCredits();
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(isPaused){
                Resume();
            }else{
                Pause();
            }
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Resume();
    }

    public void ExitGame(){
        Time.timeScale = 1f;
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Para o jogo no editor
        #else
            Application.Quit(); // Fecha o jogo na versão compilada
        #endif
    }
    public void Resume(){
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void Pause(){
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void Tutorial(){
        PauseMenu.SetActive(false);
        FindObjectOfType<Dialogs>().StartDialog(1);
        Time.timeScale = 1f;
        isPaused = false;
    }
}
