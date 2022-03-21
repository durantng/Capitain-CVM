using UnityEngine;
using UnityEngine.SceneManagement;

public class FinDeNiveau : MonoBehaviour
{
    private int level;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Félicitation, le niveau est terminé.");
            GameManager.Instance.SaveData();
            level = SceneManager.GetActiveScene().buildIndex + 1;
            GameManager.Instance
                    .PlayerData.setlevel(level-1);
            if (level <= 3)
            {
                SceneManager.LoadScene(level);
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
