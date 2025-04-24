using DurakGame.Entities;
using DurakGame.Entities.Enum;
using DurakGame.Requests;
using DurakGame.Service;

IGameService gameService = new GameService();

var player1 = new Player { Name = "Player 1", };
var player2 = new Player { Name = "Player 2" };

gameService.Start(player1, player2);

while (!gameService.IsGameOver())
{
    gameService.ShowCardsHandPlayers();

    Console.WriteLine("Attacker action");
    var actionGameAttacker = Enum.Parse<AttackerActionType>(Console.ReadLine()!);

    Console.WriteLine("What's your card do you want to attack?");
    var cardRankAttacker = Enum.Parse<Rank>(Console.ReadLine()!);

    Console.WriteLine("What's your card do you want to attack with suit?");
    var cardSuitAttacker = Enum.Parse<Suit>(Console.ReadLine()!);

    var cardsAttacker = new Card { Rank = cardRankAttacker, Suit = cardSuitAttacker };

    var cardsAttackerRequest = new AttackerActionRequest
    {
        Action = actionGameAttacker,
        Cards = [cardsAttacker]
    };

    gameService.AttackerAction(cardsAttackerRequest);

    Console.WriteLine("Do you want to defend?");
    var actionGameDefending = Enum.Parse<DefendingActionType>(Console.ReadLine()!);

    Console.WriteLine("What's your card do you want to defend?");
    var cardRankDefender = Enum.Parse<Rank>(Console.ReadLine()!);

    Console.WriteLine("What's your card do you want to defend with suit?");
    var cardSuitDefending = Enum.Parse<Suit>(Console.ReadLine()!);

    var cardsDefending = new Card { Rank = cardRankDefender, Suit = cardSuitDefending };

    var cardsDefendingRequest = new DefendingActionRequest
    {
        Action = actionGameDefending,
        Cards = [cardsDefending]
    };
    gameService.DefenderAction(cardsDefendingRequest);

    gameService.Winner();
}