using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlayer : MonoBehaviour
{

    public GameStateController controller;
    public GameObject gameOver;
    public CanvasGroupAlpha CanvasAlpha;

    [SerializeField]
    private string startScene;
    bool colliderYes = false;

    private void Start()
    {
        CanvasAlpha = FindObjectOfType<CanvasGroupAlpha>();

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!colliderYes)
        {
            if (collision.gameObject.tag == "Player")
            {
                gameOver.SetActive(true);

                StartCoroutine(LoadLevel());
                colliderYes = true;

            }
        }
      

    }

    public void EndScene()
    {
        StartCoroutine(LoadLevel());
    }
    // Start is called before the first frame update

    IEnumerator LoadLevel()
    {
        CanvasAlpha.Fade();
        AsyncOperation asyncLoadOperation = SceneManager.LoadSceneAsync(startScene);
        asyncLoadOperation.allowSceneActivation = false;
        yield return new WaitForSeconds(3);
        if (asyncLoadOperation.progress >= 0.9f && CanvasAlpha.m_fadeComplete)
        {
            colliderYes =false;
            asyncLoadOperation.allowSceneActivation = true;

        }

        yield return null;


    }
}
