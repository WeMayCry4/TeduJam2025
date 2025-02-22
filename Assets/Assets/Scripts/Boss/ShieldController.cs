using UnityEngine;

public class ShieldController : MonoBehaviour
{
    public GameObject[] destroyableObjects; // Yok edilmesi gereken objeler
    public GameObject shield; // Kalkan objesi

    void Update()
    {
        CheckObjects();
    }

    void CheckObjects()
    {
        foreach (GameObject obj in destroyableObjects)
        {
            if (obj != null)
            {
                return; // Hala yok edilmemiş bir obje varsa çık
            }
        }

        // Bütün objeler yok edildiğinde kalkanı kapat
        shield.SetActive(false);
    }
}
