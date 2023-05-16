using UnityEngine.SceneManagement;
using UnityEngine;

public class Capsule : MonoBehaviour
{
    [SerializeField] private GameObject winningPlatform;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject == winningPlatform)
        {
            SceneManager.LoadScene(2);
        }
    }
}
