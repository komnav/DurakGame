using Durak.Entities;
using Durak.Entities.Enum;
using Durak.Exceptions;
using Durak.Requests;
using Durak.Service;
using FluentAssertions;

namespace Durak.Tests;

public class GameValidatorTests
{
    private GameValidator _gameValidator;

    [SetUp]
    public void Setup()
    {
        _gameValidator = new GameValidator();
    }

    [Test]
    public void GameValidator_AttackerRequest_StatusGame_NotAttackTest()
    {
        // Arrange 
        var game = new Game
        {
            CurrentAction = GameAction.DefendAction
        };
        var request = new AttackerActionRequest
        {
            Action = AttackerActionType.Attack,
        };

        // Act, Assert
        var exception = Assert.Throws<PlayerInvalidRequestException>(
            () => _gameValidator.ValidateAttackerRequest(request, game));
        exception.Code.Should().Be(PlayerInvalidRequestExceptionCodes.GameActionNotValid);
    }

    [Test]
    public void GameValidator_AttackerRequest_CheckRequestCards_FromHandAttacker_ToAttackTest()
    {
        var player1 = new Player { Name = "Player 1", };
        var player2 = new Player { Name = "Player 2" };


        var game = new Game
        {
            Player1 = new GamePlayer
            {
                Player = player1,
                Hand = new List<Card>()
            },
            Player2 = new GamePlayer
            {
                Player = player2,
                Hand = new List<Card>()
            }
        };
        DistributeCards(game);
        game.Attacker = game.Player1;

        var newCards = new Card
        {
            Rank = Rank.Ace,
            Suit = Suit.Club
        };

        var requestNewAttacker = new AttackerActionRequest
        {
            Cards = [newCards],
            Action = AttackerActionType.Attack
        };

        //_gameValidator.ValidateAttackerRequest(requestNewAttacker, game);
        var exception = Assert.Throws<PlayerInvalidRequestException>(
            () => _gameValidator.ValidateAttackerRequest(requestNewAttacker, game)
        );
        exception.Code.Should().Be(PlayerInvalidRequestExceptionCodes.CardsNotValid);
    }

    // [Test]
    // public void GameValidate_DefendingRequest_StatusGame_NotDefendingTest()
    // {
    //     var game = new Game
    //     {
    //         CurrentAction = GameAction.AttackerAction
    //     };
    //     var request = new DefendingActionRequest
    //     {
    //         Action = DefendingActionType.Defend
    //     };
    //
    //     var exception = Assert.Throws<PlayerInvalidRequestException>(
    //         () => _gameValidator.ValidateDefenderRequest(request, game));
    //     exception.Code.Should().Be(PlayerInvalidRequestExceptionCodes.GameActionNotValid);
    // }
    //
    // [Test]
    // public void GameValidator_DefendingRequest_CheckRequestCards_FromHandAttacker_ToDefendingTest()
    // {
    //     var player1 = new Player { Name = "Player 1", };
    //     var player2 = new Player { Name = "Player 2" };
    //
    //     var game = new Game
    //     {
    //         CurrentAction = GameAction.DefendAction,
    //         Player1 = new GamePlayer
    //         {
    //             Player = player1,
    //         },
    //         Player2 = new GamePlayer
    //         {
    //             Player = player2,
    //         }
    //     };
    //
    //     var newCards = new Card
    //     {
    //         Rank = Rank.Ace,
    //         Suit = Suit.Club
    //     };
    //
    //     var request = new DefendingActionRequest
    //     {
    //         Cards = [newCards],
    //         Action = DefendingActionType.Defend
    //     };
    //
    //     var exception = Assert.Throws<PlayerInvalidRequestException>(
    //         () => _gameValidator.ValidateDefenderRequest(request, game)
    //     );
    //     exception.Code.Should().Be(PlayerInvalidRequestExceptionCodes.CardsNotValid);
    // }
    //
    // [Test]
    // public void GameValidator_CheckFields_IsNotNull()
    // {
    //     var player1 = new Player { Name = "Player 1", };
    //     var player2 = new Player { Name = "Player 2" };
    //
    //     var game = new Game
    //     {
    //         CurrentAction = GameAction.DefendAction,
    //         Player1 = new GamePlayer
    //         {
    //             Player = player1,
    //         },
    //         Player2 = new GamePlayer
    //         {
    //             Player = player2,
    //         }
    //     };
    //
    //     var requestToDefend = game.Defender.Hand[0];
    //     var request = new DefendingActionRequest
    //     {
    //         Cards = [requestToDefend],
    //         Action = DefendingActionType.Defend
    //     };
    //     var exception = Assert.Throws<PlayerInvalidRequestException>(
    //         () => _gameValidator.ValidateDefenderRequest(request, game)
    //     );
    //     exception.Code.Should().Be(PlayerInvalidRequestExceptionCodes.NotCardsInField);
    // }
    //
    // [Test]
    // public void GameValidator_DefendingRequest_CheckRequestCardsRank_ForValidToDefend()
    // {
    //     var player1 = new Player { Name = "Player 1", };
    //     var player2 = new Player { Name = "Player 2" };
    //     var game = new Game
    //     {
    //         CurrentAction = GameAction.DefendAction,
    //         Player1 = new GamePlayer
    //         {
    //             Player = player1,
    //         },
    //         Player2 = new GamePlayer
    //         {
    //             Player = player2,
    //         }
    //     };
    //
    //     game.CurrentAction = GameAction.DefendAction;
    //
    //     var newCards = new Card
    //     {
    //         Rank = Rank.Ace,
    //         Suit = Suit.Club
    //     };
    //     game.FieldCards.Add(newCards);
    //
    //     game.Defender = game.Player1;
    //     DistributeCards();
    //
    //     var requestToDefend = game.Defender.Hand[0];
    //     var requestDefending = new DefendingActionRequest
    //     {
    //         Cards = [requestToDefend],
    //         Action = DefendingActionType.Defend
    //     };
    //     var exception = Assert.Throws<PlayerInvalidRequestException>(
    //         () => _gameValidator.ValidateDefenderRequest(requestDefending, game)
    //     );
    //     exception.Code.Should().Be(PlayerInvalidRequestExceptionCodes.CardsIsNotValidToDefend);
    // }
    //
    // [Test]
    // public void GameValidator_DefendingRequest_CheckRequestCardsSuitAndTrump_ForValidToDefend()
    // {
    //     var player1 = new Player { Name = "Player 1", };
    //     var player2 = new Player { Name = "Player 2" };
    //
    //     var game = new Game
    //     {
    //         CurrentAction = GameAction.DefendAction,
    //         Player1 = new GamePlayer
    //         {
    //             Player = player1,
    //         },
    //         Player2 = new GamePlayer
    //         {
    //             Player = player2,
    //         }
    //     };
    //
    //     var newCards = new Card
    //     {
    //         Rank = Rank.Ace,
    //         Suit = Suit.Club
    //     };
    //     game.FieldCards.Add(newCards);
    //
    //     var requestToDefend = game.Defender.Hand[0];
    //     var requestDefending = new DefendingActionRequest
    //     {
    //         Cards = [requestToDefend],
    //         Action = DefendingActionType.Defend
    //     };
    //     var exception = Assert.Throws<PlayerInvalidRequestException>(
    //         () => _gameValidator.ValidateDefenderRequest(requestDefending, game)
    //     );
    //     exception.Code.Should().Be(PlayerInvalidRequestExceptionCodes.CardsIsNotValidToDefend);
    // }
    //
    private void DistributeCards(Game game)
    {
        game.Attacker = game.Player1;
        game.Defender = game.Player2;
        var newCards = new List<Card>
        {
            new Card
            {
                Rank = Rank.Six,
                Suit = Suit.Club
            },
            new Card
            {
                Rank = Rank.Seven,
                Suit = Suit.Club
            },
            new Card
            {
                Rank = Rank.Eight,
                Suit = Suit.Club
            },
            new Card
            {
                Rank = Rank.Nine,
                Suit = Suit.Heart
            },
            new Card
            {
                Rank = Rank.Six,
                Suit = Suit.Diamond
            },
            new Card
            {
                Rank = Rank.Queen,
                Suit = Suit.Club
            },
        };
        game.Player1.Hand.AddRange(newCards);
        game.Player2.Hand.AddRange(newCards);
    }
}