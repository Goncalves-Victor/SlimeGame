using UnityEngine;
using UnityEngine.UI;

public class Cronometro : MonoBehaviour
{
    public Text textoCronometro; 
    private float tempoDecorrido = 0f;

    void Update()
    {
        tempoDecorrido += Time.deltaTime;

        textoCronometro.text=Mathf.FloorToInt(tempoDecorrido / 60).ToString("00") + ":" + Mathf.FloorToInt(tempoDecorrido % 60).ToString("00");
    }
}
