using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
using System.Collections;

public class CercadoController : MonoBehaviour
{
    public Text mensagemVitoria; 
    private bool slimeBossDentro = false;

    void Start()
    {
        mensagemVitoria.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SlimeBoss"))
        {
            slimeBossDentro = true;
            StartCoroutine(ContagemRegressiva());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("SlimeBoss"))
        {
            slimeBossDentro = false; 
        }
    }

    IEnumerator ContagemRegressiva()
    {
        for (int i = 3; i > 0; i--)
        {
            yield return new WaitForSeconds(1f);
        }

        if (slimeBossDentro) 
        {
            mensagemVitoria.gameObject.SetActive(true);
            mensagemVitoria.text = "VITÓRIA";

            yield return new WaitForSeconds(3f); 

            SceneManager.LoadScene("Menu");
        }
    }
}
