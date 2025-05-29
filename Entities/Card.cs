using DurakGame.Entities.Enum;

namespace DurakGame.Entities;

public class Card
{
    public int Id { get; set; }
    public Rank Rank { get; set; }
    public Suit Suit { get; set; }
    
}