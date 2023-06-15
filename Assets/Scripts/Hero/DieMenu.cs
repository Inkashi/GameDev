using UnityEngine;

public class DieMenu : MonoBehaviour
{
    [SerializeField] GameObject menuWindow;
    // Start is called before the first frame update
    void Start()
    {
        menuWindow.SetActive(false);
    }

    public void showDiemenu()
    {
        menuWindow.SetActive(true);
    }
}
