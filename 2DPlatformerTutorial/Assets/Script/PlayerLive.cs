using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLive : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private Transform CheckPointPosition;
    [SerializeField] private AudioSource deathSoundEffect;
    [SerializeField] private GameObject[] LivesForTheMode;
    public GameObject[] hearts5;
    public GameObject[] hearts10;
    public int lives;
    private bool heartsMassive10;
    private int justOneTime;
    public int DeadFallInt;
    private Collider2D collider2D;
    void Start()
    {
        collider2D = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        if(transform.position.y < -DeadFallInt)

            if (justOneTime == 0)
            {
                justOneTime++;
                Die();
            }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Trap"))
        {
            Die();
        }
    }

    public void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
