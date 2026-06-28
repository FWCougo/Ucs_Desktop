using UnityEngine;

public class HP_PASSIVA : PASSIVA
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
                playerManager.PLAYER.ChangeMaxHP(120);
                break;

            case 2:
                playerManager.PLAYER.ChangeMaxHP(140);
                break;

            case 3:
                playerManager.PLAYER.ChangeMaxHP(160);
                break;

            case 4:
                playerManager.PLAYER.ChangeMaxHP(180);
                break;

            case 5:
                playerManager.PLAYER.ChangeMaxHP(200);
                break;
        }
    }
}
