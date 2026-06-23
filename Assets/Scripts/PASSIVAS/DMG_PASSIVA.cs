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
                GAME_MANAGER.Instance.SetExtraDMG(0.6f);
                break;

            case 2:
                GAME_MANAGER.Instance.SetExtraDMG(1.2f);
                break;

            case 3:
                GAME_MANAGER.Instance.SetExtraDMG(1.8f);
                break;

            case 4:
                GAME_MANAGER.Instance.SetExtraDMG(2.4f);
                break;

            case 5:
                GAME_MANAGER.Instance.SetExtraDMG(3f);
                break;
        }
    }
}
