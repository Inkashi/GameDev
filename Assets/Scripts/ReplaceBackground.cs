using Unity.VisualScripting;
using UnityEngine;

public class ReplaceBackground : MonoBehaviour
{
    public GameObject backgroundDark;
    private float startX;
    private bool backgroundchanged = false;
    // Start is called before the first frame update

    void Start()
    {
        startX = gameObject.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.x > startX + 100 && !backgroundchanged)
        {
            ReplaceChild();
        }
    }

    void ReplaceChild()
    {
        foreach (Transform child in gameObject.GetComponentInChildren<Transform>())
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in backgroundDark.GetComponentInChildren<Transform>())
        {
            Instantiate(child, gameObject.transform);
        }
        backgroundchanged = true;
    }


}
