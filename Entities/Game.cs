using DurakGame.Entities.Enum;

namespace DurakGame.Entities;

public class Game
{
    public List<Card> FieldCards { get; set; } = [];

    public GamePlayer Player1 { get; set; }

    public GamePlayer Player2 { get; set; }

    public List<Card> Beat { get; set; } = [];

    public Deck Deck { get; set; } = new();


    public GameAction CurrentAction { get; set; }
    

    public GamePlayer Attacker { get; set; }
    

    public GamePlayer Defender { get; set; }
}