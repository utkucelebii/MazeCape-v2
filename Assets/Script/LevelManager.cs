using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    public bool isGameOn;

    public Transform player;

    [SerializeField] private DungeonGenerator dungeonGenerator;
    [SerializeField] private Animator anim;
    [SerializeField] private Image Fade;

    private void Start()
    {
        dungeonGenerator.size = new Vector2(Random.Range(2, 10), Random.Range(2, 10));
        dungeonGenerator.MazeGenerator();
    }

    public void Respawn()
    {
        StartCoroutine("ReloadScene");
    }

    IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(3f);
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => Fade.color.a == 1);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
