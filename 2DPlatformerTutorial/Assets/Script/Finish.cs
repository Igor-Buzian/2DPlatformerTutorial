using UnityEngine;
using UnityEngine.SceneManagement;


public class Finish : MonoBehaviour
{
    [SerializeField] AudioSource finishSoundEffect;
    private Animator anim;
    [SerializeField] private GameObject fade;

    private float SecondsDelay = 1f; 
    public int allCoins;
    int level;
    private int ModeLevel;
    public bool TutorBool;
    private void Awake()
    {
        if (!TutorBool)
        {
            Cursor.lockState = CursorLockMode.Locked; // Блокирует курсор
            Cursor.visible = false; // Скрывает курсор
        }
       
    }
    private void Start()
    {
        finishSoundEffect = GetComponent<AudioSource>();
        anim = fade.GetComponent<Animator>();
        allCoins = GameObject.FindGameObjectsWithTag("Banana").Length;
        finishSoundEffect.volume = PlayerPrefs.GetFloat("SoundOfCharacter",.2f);
        if (PlayerPrefs.HasKey("level"))
        {
            level = PlayerPrefs.GetInt("level");
        }
        else
        {
            level = 1;
            PlayerPrefs.SetInt("level", level);
        }

        ModeLevel = PlayerPrefs.GetInt("HardcoreMode" + SceneManager.GetActiveScene().buildIndex);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!TutorBool)
            {
                finishSoundEffect.Play();
                Star();
                anim.SetTrigger("Fade");
                Invoke("CompleteLevel", SecondsDelay);
            }
            else
            {
                finishSoundEffect.Play();
                anim.SetTrigger("Fade");
                Invoke("CompleteTutor", SecondsDelay);
            }
           
        }
    }

    private void Star()
    {
        if (((double)PlayerPrefs.GetInt("coin" + SceneManager.GetActiveScene()) / (double)allCoins) <= 0.33f && !PlayerPrefs.HasKey("stars" + SceneManager.GetActiveScene().buildIndex))
        {
            PlayerPrefs.SetInt("stars" + SceneManager.GetActiveScene().buildIndex, 1);
        }
        else if (((double)PlayerPrefs.GetInt("coin" + SceneManager.GetActiveScene()) / (double)allCoins) > 0.33f && ((double)PlayerPrefs.GetInt("coin" + SceneManager.GetActiveScene()) / (double)allCoins) <= 0.99f && (!PlayerPrefs.HasKey("stars" + SceneManager.GetActiveScene().buildIndex) || PlayerPrefs.GetInt("stars" + SceneManager.GetActiveScene().buildIndex) < 2))
        {
            PlayerPrefs.SetInt("stars" + SceneManager.GetActiveScene().buildIndex, 2);
        }
        else if (((double)PlayerPrefs.GetInt("coin" + SceneManager.GetActiveScene()) / (double)allCoins) > 0.99f && (!PlayerPrefs.HasKey("stars" + SceneManager.GetActiveScene().buildIndex) || PlayerPrefs.GetInt("stars" + SceneManager.GetActiveScene().buildIndex) < 3))
        {
            PlayerPrefs.SetInt("stars" + SceneManager.GetActiveScene().buildIndex, 3);
        }
    }
    private void CompleteTutor()
    {
        PlayerPrefs.SetInt("InfoCanvas",1);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("chooseMenu");
    }
    private void CompleteLevel()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        switch (ModeLevel)
        {
            case 1: PlayerPrefs.SetInt("LevelGrade"+SceneManager.GetActiveScene().buildIndex,3); break;
            case 5:
                if (PlayerPrefs.GetInt("LevelGrade"+SceneManager.GetActiveScene().buildIndex,0)>=0 && PlayerPrefs.GetInt("LevelGrade"+SceneManager.GetActiveScene().buildIndex,0)<2)
                {
                    PlayerPrefs.SetInt("LevelGrade"+SceneManager.GetActiveScene().buildIndex,2);
                }

                break;
            case 10: 
                if(PlayerPrefs.GetInt("LevelGrade"+level,0)==0)
                {
                    PlayerPrefs.SetInt("LevelGrade"+level,1);
                }
                break;
        }
            if (SceneManager.GetActiveScene().buildIndex == level)
            {
              //  level = SceneManager.GetActiveScene().buildIndex+1;
                PlayerPrefs.SetInt("level", SceneManager.GetActiveScene().buildIndex+1);
                SceneManager.LoadScene("chooseMenu");
            }
            else
            {
                /*level = 1;
                PlayerPrefs.SetInt("level", level); ��� ���� ��� �� ��� ������ ������ 1 �������*/
                SceneManager.LoadScene("chooseMenu");
            }
    }
}
