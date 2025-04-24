using DurakGame.Entities;
using DurakGame.Entities.Enum;
using DurakGame.Requests;

namespace DurakGame.Service;

public class GameService : IGameService
{
    private Game _game;
    private readonly Random _random = new Random();
    private Deck _deck;
    private IGameValidator _gameValidator = new GameValidator();

    public void Start(Player player1, Player player2)
    {
        _game = new Game();

        _deck = new Deck();

        _game.Player1 = new GamePlayer { Player = player1, Hand = [] };

        _game.Player2 = new GamePlayer { Player = player2, Hand = [] };

        DistributeCards();

        DefineAttackerAndDefender();

        _game.CurrentAction = GameAction.AttackerAction;
    }

    public Game GetGame()
    {
        return _game;
    }

    private void DistributeCards()
    {
        var deck = _game.Deck.Cards.OrderBy(_ => _random.Next()).ToList();

        for (var i = 0; i < 6; i++)
        {
            var card1 = deck.First();
            _game.Player1.Hand.Add(card1);
            _game.Deck.DeleteCard(card1);
            deck.RemoveAt(0);

            var card2 = deck.First();
            _game.Player2.Hand.Add(card2);
            _game.Deck.DeleteCard(card2);
            deck.RemoveAt(0);
        }
    }

    public bool IsGameOver()
    {
        if (_game.Player1.Hand.Count == 0 || _game.Player2.Hand.Count == 0)
        {
            return true;
        }

        return false;
    }


    public void AttackerAction(AttackerActionRequest request)
    {
        if (request.Action == AttackerActionType.Beat)
        {
            _game.Beat = _game.FieldCards;
            _game.FieldCards.Clear();

            (_game.Defender, _game.Attacker) = (_game.Attacker, _game.Defender);

            ExcitementOfTheHandAttacker();
            ExcitementOfTheHandDefending();
            _game.CurrentAction = GameAction.AttackerAction;
            return;
        }

        _gameValidator.ValidateAttackerRequest(request, _game);
        var cardsAttackerRequest = request.Cards;
        _game.FieldCards.AddRange(cardsAttackerRequest);

        foreach (var requestCard in cardsAttackerRequest)
        {
            _game.Attacker.Hand.RemoveAll(
                x => x.Suit == requestCard.Suit
                     && x.Rank == requestCard.Rank);
        }

        _game.CurrentAction = GameAction.DefendAction;
    }

    public void DefenderAction(DefendingActionRequest request)
    {
        _gameValidator.ValidateDefenderRequest(request, _game);

        if (request.Action == DefendingActionType.Defend)
        {
            var cardsDefendingRequest = request.Cards;
            _game.FieldCards.AddRange(cardsDefendingRequest);
            foreach (var requestCard in cardsDefendingRequest)
            {
                _game.Defender.Hand
                    .RemoveAll(x => x.Suit == requestCard.Suit && x.Rank == requestCard.Rank);
            }

            _game.CurrentAction = GameAction.AttackerAction;
        }

        if (request.Action == DefendingActionType.Take)
        {
            _game.Defender.Hand.AddRange(_game.FieldCards);
            _game.FieldCards.Clear();

            ExcitementOfTheHandAttacker();
            _game.CurrentAction = GameAction.AttackerAction;
        }
    }

    private void DefineAttackerAndDefender()
    {
        var trump = _game.Deck.TrumpCard;

        var smallTrumpPlayer1 = _game.Player1.Hand?
            .OrderBy(x => x.Suit == trump.Suit)
            .Min(x => x.Rank);

        var smallTrumpPlayer2 = _game.Player2.Hand?
            .OrderBy(x => x.Suit == trump.Suit)
            .Min(x => x.Rank);

        if (smallTrumpPlayer1 == null || smallTrumpPlayer2 == null)
            throw new Exception("No trump players have been set.");
        if (smallTrumpPlayer1 > smallTrumpPlayer2)
        {
            _game.Attacker = _game.Player2;
            _game.Defender = _game.Player1;
        }

        else
        {
            _game.Attacker = _game.Player1;
            _game.Defender = _game.Player2;
        }
    }

    public void ShowCardsHandPlayers()
    {
        foreach (var cardAttacker in _game.Attacker.Hand)
        {
            Console.WriteLine(
                $"Cards attacker:{cardAttacker.Id} {_game.Attacker.Player.Name} = > {cardAttacker.Rank} ({cardAttacker.Suit})");
        }

        Console.WriteLine("_________________________");
        foreach (var cardDefending in _game.Defender.Hand)
        {
            Console.WriteLine(
                $"Cards defending: {_game.Defender.Player.Name} = > {cardDefending.Rank} ({cardDefending.Suit})");
        }
    }


    private void ExcitementOfTheHandAttacker()
    {
        var deck = _game.Deck.Cards;
        if (deck.Count != 0)
        {
            var countForExcitementOfTheHandAttacker = 6 - _game.Attacker.Hand.Count;
            for (var i = 0; i < countForExcitementOfTheHandAttacker; i++)
            {
                var card1 = deck.First();
                _game.Attacker.Hand.Add(card1);
                _game.Deck.DeleteCard(card1);
            }
        }
    }

    private void ExcitementOfTheHandDefending()
    {
        var deck = _game.Deck.Cards;

        if (deck.Count != 0)
        {
            var countForExcitementOfTheHandAttacker = 6 - _game.Defender.Hand.Count;
            for (var i = 0; i < countForExcitementOfTheHandAttacker; i++)
            {
                var card1 = deck.First();
                _game.Defender.Hand.Add(card1);
                _game.Deck.DeleteCard(card1);
            }
        }
    }

    public Player Winner()
    {
        if (_game.Player1.Hand.Count == 0)
        {
            return _game.Player1.Player;
        }
        else
        {
            return _game.Player2.Player;
        }
    }
}