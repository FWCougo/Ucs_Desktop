using UnityEngine;

public class SPEED_PASSIVA : PASSIVA
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
                playerManager.PLAYER_MOVE.ChangeSpeed(11);
                break;

            case 2:
                playerManager.PLAYER_MOVE.ChangeSpeed(12);
                break;

            case 3:
                playerManager.PLAYER_MOVE.ChangeSpeed(13);
                break;

            case 4:
                playerManager.PLAYER_MOVE.ChangeSpeed(14);
                break;

            case 5:
                playerManager.PLAYER_MOVE.ChangeSpeed(15);
                break;
        }
    }
}
