using UnityEngine;

public class CAMERA_MANAGER : MonoBehaviour
{
    [SerializeField]
    private GameObject[] cameraList;

    [SerializeField]
    private int activeCamera = 0;
    [SerializeField]
    private int previous = -1;

    public static CAMERA_MANAGER Instance;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        OpenCamera(0);
    }

    public void OpenMenu(Transform _menu)
    {
        for (int i = 0; i < cameraList.Length; i++)
        {
            if (cameraList[i].transform == _menu)
            {
                activeCamera = i;
            }
        }

        SetTrueAndFalse();
    }

    public void OpenMenu(string _menu)
    {
        for (int i = 0; i < cameraList.Length; i++)
        {
            if (cameraList[i].name == _menu)
            {
                activeCamera = i;
            }
        }

        SetTrueAndFalse();
    }

    public void OpenCamera(int index)
    {
        for (int i = 0; i < cameraList.Length; i++)
        {
            if (index == i)
            {
                activeCamera = i;
            }
        }

        SetTrueAndFalse();
    }

    private void SetTrueAndFalse()
    {
        if (previous != -1)
            cameraList[previous].SetActive(false);

        cameraList[activeCamera].SetActive(true);
        previous = activeCamera;
    }
}
