using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour {
    public Sprite playerSprite;
    public Sprite lifeSprite;
    public GameObject player;
    public GameObject life;
    public GameObject loadingImage;
    public GameObject starryBackground;
    public static int rememberedLives=0;

    public void LoadImage()
    {
        SpriteRenderer shipSprite = player.GetComponent<SpriteRenderer>();
        SpriteRenderer UISprite = life.GetComponent<SpriteRenderer>();
        shipSprite.sprite = playerSprite;
        UISprite.sprite = lifeSprite;
    }

    public void LoadScene(int level)
    {
        loadingImage.SetActive(true);
        SceneManager.LoadScene(level);
    }

    public void LoadVinBackground()
    {
        starryBackground.SetActive(true);
    }

    public void LoadJamesBackground()
    {
        starryBackground.SetActive(false);
    }
}
