using DurakGame.Entities;
using DurakGame.Entities.Enum;
using DurakGame.Exceptions;
using DurakGame.Requests;

namespace DurakGame.Service;

public sealed class GameValidator : IGameValidator
{
    public void ValidateAttackerRequest(
        AttackerActionRequest request,
        Game game)
    {
        if (game.CurrentAction != GameAction.AttackerAction)
        {
            throw new PlayerInvalidRequestException(PlayerInvalidRequestExceptionCodes.GameActionNotValid);
        }

        var requestAttacker = request.Cards.FirstOrDefault();

        var isValidCardRequested = game.Attacker.Hand.Any(
            x => x.Rank == requestAttacker?.Rank
                 && x.Suit == requestAttacker?.Suit);

        if (isValidCardRequested != true)
        {
            throw new PlayerInvalidRequestException(PlayerInvalidRequestExceptionCodes.CardsNotValid);
        }
    }

    public void ValidateDefenderRequest(DefendingActionRequest request, Game game)
    {
        if (game.CurrentAction != GameAction.DefendAction)
        {
            throw new PlayerInvalidRequestException(PlayerInvalidRequestExceptionCodes.GameActionNotValid);
        }

        var requestDefender = request.Cards.FirstOrDefault();

        var isValidCardRequested = game.Defender.Hand.Any(
            x => x.Rank == requestDefender?.Rank
                 && x.Suit == requestDefender?.Suit);

        if (isValidCardRequested != true)
        {
            throw new PlayerInvalidRequestException(PlayerInvalidRequestExceptionCodes.CardsNotValid);
        }

        var fieldCards = game.FieldCards.FirstOrDefault();

        if (fieldCards == null)
            throw new PlayerInvalidRequestException(PlayerInvalidRequestExceptionCodes.NotCardsInField);

        foreach (var requestCard in request.Cards)
        {
            if (requestCard.Rank < fieldCards.Rank)
            {
                throw new PlayerInvalidRequestException(PlayerInvalidRequestExceptionCodes.CardsIsNotValidToDefend);
            }

            if (requestCard.Suit != fieldCards.Suit &&
                game.Deck.TrumpCard.Suit != requestCard.Suit)
            {
                throw new PlayerInvalidRequestException(PlayerInvalidRequestExceptionCodes.CardsIsNotValidToDefend);
            }
        }
    }
}