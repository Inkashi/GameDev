using UnityEngine;
using UnityEngine.SceneManagement;

public class visionSkript : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 2)
        {
            Destroy(canvas);
        }
        else
        {
            canvas.SetActive(true);
        }
    }
}
