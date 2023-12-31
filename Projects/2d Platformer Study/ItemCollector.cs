using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int cherries = 0;

    [SerializeField] private TextMeshProUGUI cherriesText;
    [SerializeField] private AudioSource collectionSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            collectionSound.Play();
            Destroy(collision.gameObject);
            cherries++;
            cherriesText.text = "Cherries:" + cherries;
        }
    }
}
