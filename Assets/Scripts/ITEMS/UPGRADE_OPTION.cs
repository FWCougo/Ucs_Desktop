using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UPGRADE_OPTION : MonoBehaviour
{
    [SerializeField] private PASSIVA_SO passiva_SO;
    [SerializeField] private TMP_Text titulo_TXT;
    [SerializeField] private TMP_Text preco_TXT;
    [SerializeField] private Image upgrade_Sprite;
    [SerializeField] private Image[] achievedIMGs;
    [SerializeField] private int level = 0;

    private void Awake()
    {
        ChangeTitle();
        ChangeSprite();
        ChangePreco();
    }

    public void AumentarLV(int _lv)
    {
        level = _lv;
        ActivateIMGs();
        ChangePreco();
    }
    public void ChangeTitle()
    {
        titulo_TXT.text = passiva_SO.nome;
    }
    public void ChangeSprite()
    {
        upgrade_Sprite.sprite = passiva_SO.sprite;
    }
    public void ChangePreco()
    {
        if (passiva_SO.preco.Length <= level) return;
        int _preco = passiva_SO.preco[level];
        preco_TXT.text = $"-{_preco}";
    }
    public void ActivateIMGs()
    {
        for (int i = 0; i < achievedIMGs.Length; i++)
        {
            achievedIMGs[i].gameObject.SetActive(false);
        }

        for (int i = 0; i<level; i++)
        {
            achievedIMGs[i].gameObject.SetActive(true);
        }
    }
}
