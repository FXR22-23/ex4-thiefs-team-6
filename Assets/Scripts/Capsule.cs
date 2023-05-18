using UnityEngine.SceneManagement;
using UnityEngine;

public class Capsule : MonoBehaviour
{
    [SerializeField] private GameObject winningPlatform;
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject avatar;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject == winningPlatform)
        {
            gameManager.GetComponent<GameManager>().TurnOnWinMusic();
            Invoke("LoadWinningScene", 4);
        }
    }

    private void LoadWinningScene()
    {
        SceneManager.LoadScene(2);
        player.transform.position = new Vector3(12.836f, 2.6766f, 37.968f);
        avatar.transform.position = new Vector3(12.836f, 2.6766f, 37.968f);
    }
}
