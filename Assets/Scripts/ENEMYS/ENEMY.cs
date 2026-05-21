using UnityEngine;

public class ENEMY : MonoBehaviour
{
    public ENEMY_SO enemy_SO;
    public PLAYER[] playersTransform;
    public Transform currentPlayer;

    private float dist = 1000;

    private void Awake()
    {
        GetAllPlayers();
    }   

    private void GetAllPlayers()
    {
        playersTransform = FindObjectsByType<PLAYER>(FindObjectsSortMode.None);

        float aux_dist = 0;

        aux_dist = dist;

        for(int i = 0; i<playersTransform.Length; i++)
        {          
            dist = Vector3.Distance(transform.position, playersTransform[i].transform.position);

            if(dist < aux_dist)
            {
                currentPlayer = playersTransform[i].transform;
            }

            aux_dist = dist;
        }
               
        
    }

}
