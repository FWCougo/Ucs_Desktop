using UnityEngine;

public class MENU_MANAGER : MonoBehaviour
{
    [SerializeField]
    private GameObject[] menuList;

    [SerializeField]
    private int activeMenu = 0;
    [SerializeField]
    private int previous = -1;

    public static MENU_MANAGER Instance;


    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        OpenMenu(0);
    }

    public void OpenMenu(Transform _menu)
    {
        for (int i = 0; i < menuList.Length; i++)
        {
            if (menuList[i].transform == _menu)
            {
                activeMenu = i;
            }
        }

        SetTrueAndFalse();
    }

    public void OpenMenu(string _menu)
    {
        for (int i = 0; i < menuList.Length; i++)
        {
            if (menuList[i].name == _menu)
            {
                activeMenu = i;
            }
        }

        SetTrueAndFalse();
    }

    public void OpenMenu(int index)
    {
        for (int i = 0; i < menuList.Length; i++)
        {
            if (index == i)
            {
                activeMenu = i;
            }
        }

        SetTrueAndFalse();
    }

    private void SetTrueAndFalse()
    {
        if(previous != -1)
            menuList[previous].SetActive(false);

        menuList[activeMenu].SetActive(true);
        previous = activeMenu;
    }

}
