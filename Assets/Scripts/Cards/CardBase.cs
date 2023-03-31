using UnityEngine;

[CreateAssetMenu]
public class CardBase : ScriptableObject
{
    // カードの基礎データ
    [SerializeField] new string name;
    [SerializeField] CardType type;
    [SerializeField] int number;
    [SerializeField] Sprite icon;

  public string Name { get => name; }
  public CardType Type { get => type; }
  public int Number { get => number; }
  public Sprite Icon { get => icon; } 

  public enum CardType
    {
        Silence,
        Success, 
        Boom,
    }
}
