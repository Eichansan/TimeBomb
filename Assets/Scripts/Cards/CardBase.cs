using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class CardBase : ScriptableObject
{
    // カードの基礎データ
    [SerializeField] new string name;
    [SerializeField] CardType type;
    [SerializeField] int number;
    [SerializeField] Sprite icon;
    [SerializeField] Sprite cardImage;

  public string Name { get => name; }
  public CardType Type { get => type; }
  public int Number { get => number; }
  public Sprite Icon { get => icon; } 
  public Sprite CardImage { get => cardImage; } 

  public enum CardType
    {
        Silence,
        Success, 
        Boom,
    }
}
