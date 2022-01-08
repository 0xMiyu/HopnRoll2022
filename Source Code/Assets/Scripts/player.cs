using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class player : MonoBehaviour
{
    public Camera cypherspycam;
    private static float speed = 270f;
    private Rigidbody2D rb;
    private float mdt;
    private int layermask;
    private float distToGround;
    private float cd;
    private float prevupdat;
    private float prevpos;
    private bool loggr;
    private static float chargetime = 1f;
    private bool mouseisdown;
    private float awktime;
    private int prevcont;
    private int conta;
    public AudioSource audioSource;
    private Collider2D[] useless;
    private Vector2 valo;
    void Start()
    {
        
    }
    private void Awake()
    {
        mdt = Time.time + 0.3f;
        mouseisdown = false;
        rb = gameObject.GetComponent<Rigidbody2D>();
        distToGround = GetComponent<Collider2D>().bounds.extents.y;
        layermask = 1 << 3;
        layermask = ~layermask;
        loggr = false;
        prevpos = -100f;
        awktime = Time.time+0.3f;
        useless = new Collider2D[0];
        prevupdat = 0f;
        
    }
    bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, -Vector2.up, distToGround + 0.1f, layermask);
    }
    void Update()
    {
        float kek = (rb.velocity.x-valo.x)*(rb.velocity.x-valo.x)+(rb.velocity.y-valo.y)*(rb.velocity.y-valo.y);
//        if(rb.velocity.x*rb.velocity.x + rb.velocity.y * rb.velocity.y>150f){Debug.Log("Oshit" + Time.time.ToString());}
        if(kek>150f){
            audioSource.Play();
        }
        valo = rb.velocity;

        useless = Physics2D.OverlapCircleAll(transform.position,  0.35f, ~(1 << LayerMask.NameToLayer("Player")));
        
        conta = useless.Length;
        if(prevcont<conta){
            audioSource.Play();
        }
        prevcont = conta;
       // Debug.Log(IsGrounded().ToString() + Time.time.ToString());
        cypherspycam.gameObject.transform.position = new Vector3(0, 7.2f * Mathf.FloorToInt((transform.position.y + 4.5f) / 7.2f) - 1, -10);
        if (prevupdat < Time.time - 0.2f && Mathf.Abs(prevpos - transform.position.y) > 0.5f && !loggr)
        {
            loggr = true;
            prevupdat = Time.time + 0.5f;
            prevpos = transform.position.y;
        }
        if (prevupdat < Time.time && loggr)
        {
            save.graph.Add(transform.position.y);
            loggr = false;
        }
        if (Input.GetMouseButton(0) && !mouseisdown)
        {
            mdt = Time.time;
            mouseisdown = true;
        }
        if (!Input.GetMouseButton(0) && mouseisdown)
        {
            mouseisdown = false;
            if (Time.time > awktime && Time.time > cd + 0.1f && IsGrounded())
            {
                cd = Time.time;
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                float xfor = (worldPosition.x - transform.position.x) / (Mathf.Sqrt(((worldPosition.x - transform.position.x) * (worldPosition.x - transform.position.x)) + ((worldPosition.y - transform.position.y) * (worldPosition.y - transform.position.y))));

                float yfor = (worldPosition.y - transform.position.y) / (Mathf.Sqrt(((worldPosition.x - transform.position.x) * (worldPosition.x - transform.position.x)) + ((worldPosition.y - transform.position.y) * (worldPosition.y - transform.position.y))));

                xfor *= (Mathf.Min(Time.time - mdt, chargetime)) * speed;
                yfor *= (Mathf.Min(Time.time - mdt, chargetime)) * speed;

                rb.AddForce(new Vector2(xfor, yfor));
            }
        }
    }
}
