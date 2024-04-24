using UnityEngine;
using UnityEngine.UI;

public class KeyItem : MonoBehaviour
{
    public bool canCollect = false;
    public Text pressEText;

    private ItemCheckBar itemCheckBar; // Reference to the ItemCheckBar script

    private void Start()
    {
        pressEText.gameObject.SetActive(false); // Hide the text initially
        itemCheckBar = FindObjectOfType<ItemCheckBar>(); // Find the ItemCheckBar script in the scene
    }

    private void Update()
    {
        if (canCollect && Input.GetKeyDown(KeyCode.E))
        {
            Collect();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canCollect = true;
            pressEText.gameObject.SetActive(true); // Show the text when the player gets close
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canCollect = false;
            pressEText.gameObject.SetActive(false); // Hide the text when the player moves away
        }
    }

    private void Collect()
    {
        // Perform collection logic here
        // For example, you can disable or destroy the key item game object
        Destroy(gameObject);
        pressEText.gameObject.SetActive(false); // Hide the text when the player moves away

        // Notify the ItemCheckBar script about the collection
        if (itemCheckBar != null)
        {
            itemCheckBar.CollectKeyItem();
        }
    }
}
