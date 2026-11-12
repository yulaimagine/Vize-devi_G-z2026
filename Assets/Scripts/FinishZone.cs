using UnityEngine;
using UnityEngine.SceneManagement;
 
public class FinishZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // bitis alanina giren oyuncu mu?
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
 
            // puan hedefe ulastiysa sahneyi yeniden yukle
            if (player.score >= player.targetScore)
            {
                Debug.Log("Oyun bitti, sahne yeniden başlıyor!");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                Debug.Log("Yeterli puan yok! Puan: " + player.score);
            }
        }
    }
}