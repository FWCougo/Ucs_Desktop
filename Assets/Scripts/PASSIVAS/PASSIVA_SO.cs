using UnityEngine;

[CreateAssetMenu(fileName = "PASSIVAS")]
public class PASSIVA_SO : ScriptableObject
{
    public string nome;
    public Sprite sprite;

    public int[] preco = {10, 25, 40, 50, 100};
}
