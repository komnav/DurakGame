using System.Collections.Generic;
using System.Linq;
using DurakGame.Entities.Enum;

namespace DurakGame.Entities;

public class Deck
{
    private List<Card> cards { get; set; }

    public IReadOnlyList<Card> Cards => cards.AsReadOnly();
    public Card TrumpCard { get; set; }

    public Deck()
    {
        cards = new List<Card>();

        cards.Add(new Card { Suit = Suit.Club, Rank = Rank.Six });
        cards.Add(new Card { Suit = Suit.Club, Rank = Rank.Seven });
        cards.Add(new Card { Suit = Suit.Club, Rank = Rank.Eight });
        cards.Add(new Card { Suit = Suit.Club, Rank = Rank.Nine });
        cards.Add(new Card { Suit = Suit.Club, Rank = Rank.Ten });
        cards.Add(new Card { Suit = Suit.Club, Rank = Rank.Jack });
        cards.Add(new Card { Suit = Suit.Club, Rank = Rank.Queen });
        cards.Add(new Card { Suit = Suit.Club, Rank = Rank.King });
        cards.Add(new Card { Suit = Suit.Club, Rank = Rank.Ace });

        cards.Add(new Card { Suit = Suit.Diamond, Rank = Rank.Six });
        cards.Add(new Card { Suit = Suit.Diamond, Rank = Rank.Seven });
        cards.Add(new Card { Suit = Suit.Diamond, Rank = Rank.Eight });
        cards.Add(new Card { Suit = Suit.Diamond, Rank = Rank.Nine });
        cards.Add(new Card { Suit = Suit.Diamond, Rank = Rank.Ten });
        cards.Add(new Card { Suit = Suit.Diamond, Rank = Rank.Jack });
        cards.Add(new Card { Suit = Suit.Diamond, Rank = Rank.Queen });
        cards.Add(new Card { Suit = Suit.Diamond, Rank = Rank.King });
        cards.Add(new Card { Suit = Suit.Diamond, Rank = Rank.Ace });

        cards.Add(new Card { Suit = Suit.Heart, Rank = Rank.Six });
        cards.Add(new Card { Suit = Suit.Heart, Rank = Rank.Seven });
        cards.Add(new Card { Suit = Suit.Heart, Rank = Rank.Eight });
        cards.Add(new Card { Suit = Suit.Heart, Rank = Rank.Nine });
        cards.Add(new Card { Suit = Suit.Heart, Rank = Rank.Ten });
        cards.Add(new Card { Suit = Suit.Heart, Rank = Rank.Jack });
        cards.Add(new Card { Suit = Suit.Heart, Rank = Rank.Queen });
        cards.Add(new Card { Suit = Suit.Heart, Rank = Rank.King });
        cards.Add(new Card { Suit = Suit.Heart, Rank = Rank.Ace });

        cards.Add(new Card { Suit = Suit.Spade, Rank = Rank.Six });
        cards.Add(new Card { Suit = Suit.Spade, Rank = Rank.Seven });
        cards.Add(new Card { Suit = Suit.Spade, Rank = Rank.Eight });
        cards.Add(new Card { Suit = Suit.Spade, Rank = Rank.Nine });
        cards.Add(new Card { Suit = Suit.Spade, Rank = Rank.Ten });
        cards.Add(new Card { Suit = Suit.Spade, Rank = Rank.Jack });
        cards.Add(new Card { Suit = Suit.Spade, Rank = Rank.Queen });
        cards.Add(new Card { Suit = Suit.Spade, Rank = Rank.King });
        cards.Add(new Card { Suit = Suit.Spade, Rank = Rank.Ace });

        if (cards.Count > 0)
        {
            TrumpCard = cards.First();
        }
    }

    public void DeleteCard(Card card)
    {
        cards.Remove(card);
    }
}