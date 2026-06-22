using UnityEngine;

public class DMG_PASSIVA : PASSIVA
{
    private void Start()
    {
        CheckLevel();
    }

    public void CheckLevel()
    {
        switch (nivelPassiva)
        {
            case 1:
                GAME_MANAGER.Instance.SetExtraDMG(1);
                break;

            case 2:
                GAME_MANAGER.Instance.SetExtraDMG(2);
                break;

            case 3:
                GAME_MANAGER.Instance.SetExtraDMG(3);
                break;

            case 4:
                GAME_MANAGER.Instance.SetExtraDMG(4);
                break;

            case 5:
                GAME_MANAGER.Instance.SetExtraDMG(5);
                break;
        }
    }
}
