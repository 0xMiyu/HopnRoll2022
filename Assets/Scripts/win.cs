using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class win : MonoBehaviour
{
    private float wintim;
    private bool wonanim;
    public static bool wonn;
    private Vector3 coc;
    private Vector3 bohl;
    private float rocspeed = 5f;
    public GameObject mlghorn;
    public GameObject ween;
    void Start(){
        wonanim = false;
        wonn = false;
        wintim = 10000000f;
        coc = transform.position;
        bohl = transform.localScale;
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            mlghorn.SetActive(true);
            player kekker = other.gameObject.GetComponent<player>();
            Destroy(kekker);
            Rigidbody2D kekkers = other.gameObject.GetComponent<Rigidbody2D>();
            Destroy(kekkers);
            other.gameObject.transform.SetParent(gameObject.transform);
            Destroy(other);
            wintim = Time.time;
            wonanim = true;
        }
        if(other.CompareTag("moon")){
            wonn = true;
            wonanim = false;
            Destroy(gameObject);
        }
    }
    
    void Update(){
        if(wonanim){
            transform.position = new Vector3(coc.x-rocspeed*(Time.time-wintim), coc.y+Random.Range(-0.08f / (1.5f*Mathf.Max(Time.time-wintim, 0.5f)), 0.08f/ (1.5f*Mathf.Max(Time.time-wintim, 0.5f))), coc.z);
            transform.localScale = bohl/(2*Mathf.Max(Time.time-wintim, 0.5f));
            if(transform.position.x<=-3f){
                wonn = true;
            wonanim = false;
            ween.SetActive(true);
            Destroy(gameObject);
            }
        }
    }
}
