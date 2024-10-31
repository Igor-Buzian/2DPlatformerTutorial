using UnityEngine;
using UnityEngine.SceneManagement;


public class Finish : MonoBehaviour
{
    public Animator anim;

    private float SecondsDelay = 1f;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("Fade");
            Invoke("CompleteLevel", SecondsDelay);

        }
    }


    private void CompleteLevel()
    {
        SceneManager.LoadScene("SampleScene 1");
    }
}
