using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawgraph : MonoBehaviour
{
    private float maxx;
    private float miin;
    private int strrt;
    private int kount;
    public Camera camm;
    private float camwid;
    private float camhei;
    public Shader lineshader;
    private Material redd;
    private static int mgx=1000000000;
    private Material gren;
    private float linwid;
    public void summongraf()
    {
        transform.position = new Vector3(camm.gameObject.transform.position.x, camm.gameObject.transform.position.y, -20f);
        redd = new Material(lineshader);
        redd.SetColor("_Color", Color.red);
        gren = new Material(lineshader);
        gren.SetColor("_Color", Color.green);
        camwid = (float)camm.pixelWidth;
        camhei = (float)camm.pixelHeight;
        maxx = -10f;
        miin = 10000000f;
        //Debug.Log(save.graph.Count);
        kount = Mathf.Min(save.graph.Count, mgx);
        strrt = Mathf.Max(0, save.graph.Count - mgx);
        for (int i = 0; i < kount; i++)
        {
            maxx = Mathf.Max(maxx, save.graph[i + strrt]);
            miin = Mathf.Min(miin, save.graph[i + strrt]);
        }
        if (maxx != miin)
        {
            
            Vector3 start;
            Vector3 end = new Vector3((camwid * 19 / 20), (save.graph[strrt] - miin) / (maxx - miin) * (camhei * 18 / 20) + camhei / 20, -19f);
            end = camm.ScreenToWorldPoint(end);
            end = new Vector3(end.x, end.y, -19f);
            for (int i = 1; i < kount; i++)
            {
                start = new Vector3(end.x, end.y, -19f);
                end = new Vector3((camwid / 20) + (camwid * 18 / 20) * (kount-i) / (kount), (save.graph[i + strrt] - miin) / (maxx - miin) * (camhei * 18 / 20) + camhei / 20, -19f);
                end = camm.ScreenToWorldPoint(end);
                end = new Vector3(end.x, end.y, -19f);
                GameObject myLine = new GameObject();
                myLine.transform.position = start;
                myLine.transform.SetParent(transform);
                myLine.AddComponent<graphkms>();
                myLine.AddComponent<LineRenderer>();
                LineRenderer lr = myLine.GetComponent<LineRenderer>();
                if (start.y <= end.y)
                {
                    lr.material = gren;
                    lr.startColor = Color.green;
                    lr.endColor = Color.green;
                }
                else
                {
                    lr.material = redd;
                    lr.startColor = Color.red;
                    lr.endColor = Color.red;
                }
                linwid = 0.05f - (0.05f-0.005f)*(save.graph.Count-50)/350;
                if (save.graph.Count < 50) {
                    linwid = 0.05f;
                }
                if (save.graph.Count>400) {
                    linwid = 0.005f;
                }
                lr.startWidth = linwid;
                lr.endWidth = linwid;
                lr.SetPosition(0, start);
                lr.SetPosition(1, end);
            }
        }
    }
    // Update is called once per frame
}
