using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class DetectionController : MonoBehaviour
{
    public string targetDetection="Player";

    public List<Collider2D> detectedObjs = new List<Collider2D>();

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag==targetDetection){
            detectedObjs.Add(collision);
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.gameObject.tag==targetDetection){
            detectedObjs.Remove(collision);
        }
    }
}
