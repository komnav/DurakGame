using Durak.Entities;
using Durak.Service;
using Durak.Entities.Enum;
using Durak.Requests;


IGameService gameService = new GameService();


var player1 = new Player { Name = "Player 1", };
var player2 = new Player { Name = "Player 2" };

gameService.Start(player1, player2);

while (!gameService.IsGameOver())
{
    gameService.ShowCardsHandPlayers();

    Console.WriteLine("What's your card do you want to attack?");
    var cardRankAttacker = Enum.Parse<Rank>(Console.ReadLine()!);

    Console.WriteLine("What's your card do you want to attack with suit?");
    var cardSuitAttacker = Enum.Parse<Suit>(Console.ReadLine()!);

    Console.WriteLine("Action game");
    var actionGameAttacker = Enum.Parse<AttackerActionType>(Console.ReadLine()!);

    var cardsAttacker = new Card { Rank = cardRankAttacker, Suit = cardSuitAttacker };

    var cardsAttackerRequest = new AttackerActionRequest
    {
        Cards = [cardsAttacker],
        Action = actionGameAttacker
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
        Cards = [cardsDefending],
        Action = actionGameDefending
    };

    gameService.DefenderAction(cardsDefendingRequest);

    gameService.Winner();
};