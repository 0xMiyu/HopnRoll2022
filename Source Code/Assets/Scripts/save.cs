using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class save : MonoBehaviour
{
    public static List<float> graph = new List<float>();
    public static bool graphlook = false;
    public GameObject graf;
    private float sttim;
    private static float turntim = 0f;
    public GameObject playr;
    private Vector2 vel;
    private bool kekkers;
    private bool gamestarted;
    public GameObject evrything;
    void Start() {
        sttim = -1000f;
        kekkers = false;
        gamestarted = false;
    }
    void Update() {
        if (!gamestarted) {
            if (Input.GetMouseButtonDown(0)) {
                gamestarted = true;
                evrything.SetActive(true);
            }
        }
        if (gamestarted)
        {
            if (graphlook || sttim + turntim + 0.05f > Time.time)
            {
                if(!win.wonn) playr.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                kekkers = true;
            }
            else
            {
                if (kekkers)
                {
                    if(!win.wonn) playr.GetComponent<Rigidbody2D>().velocity = vel;
                    kekkers = false;
                }
            }
            if (Input.GetKeyDown(KeyCode.G) && sttim + turntim + 0.05f < Time.time)
            {
                if (graphlook == false)
                {
                    graphlook = true;
                    sttim = Time.time;
                    graf.SetActive(true);
                    graf.GetComponent<drawgraph>().summongraf();
                    if(!win.wonn) vel = playr.GetComponent<Rigidbody2D>().velocity;
                    transform.eulerAngles = new Vector3(0, 180, 0);
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    graphlook = false;
                    sttim = Time.time;
                }
            }
            /* if (sttim > Time.time - turntim - 0.05f && sttim < Time.time) {
                 if (graphlook)
                 {
                     transform.eulerAngles = new Vector3(0, Mathf.Min(180 * (Time.time - sttim) / turntim, 180), 0);
                 }
                 else {
                     transform.eulerAngles = new Vector3(0, Mathf.Min(180 + 180 * (Time.time - sttim) / turntim, 360), 0);
                 }
             }*/
            if (sttim + turntim < Time.time && !graphlook)
            {
                graf.SetActive(false);
            }
        }
    }
}
