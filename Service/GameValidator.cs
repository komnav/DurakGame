using Durak.Entities;
using Durak.Entities.Enum;
using Durak.Exceptions;
using Durak.Requests;

namespace Durak.Service;

public sealed class GameValidator : IGameValidator
{
    public void ValidateAttackerRequest(AttackerActionRequest request, Game game)
    {
        if (game.CurrentAction != GameAction.AttackerAction)
        {
            throw new PlayerInvalidRequestException(PlayerInvalidRequestExceptionCodes.GameActionNotValid);
        }

        if (request.Cards.FirstOrDefault() != game.Attacker.Hand.FirstOrDefault() &&
            request.Action != AttackerActionType.Beat)
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

        if (request.Cards.FirstOrDefault() != game.Defender.Hand.FirstOrDefault() &&
            request.Action != DefendingActionType.Take)
        {
            throw new PlayerInvalidRequestException(PlayerInvalidRequestExceptionCodes.CardsNotValid);
        }

        var fieldCards = game.FieldCards.FirstOrDefault();

        if (fieldCards == null) throw new PlayerInvalidRequestException(PlayerInvalidRequestExceptionCodes.NotCardsInField);

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