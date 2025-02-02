using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement; 

public class MenuController : MonoBehaviour
{   
    
    public GameObject menuUI; 
    public Button startButton; 
    public string jogo; 

    public void AbrirJogo(){
        Debug.Log ("pertado");
        SceneManager.LoadScene("SampleScene");
    }
    public void Menu(){
        SceneManager.LoadScene("Menu");
    }

    public void Sair(){
        Debug.Log("Saindo...");
        Application.Quit();
    }

}
