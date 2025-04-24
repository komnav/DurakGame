using DurakGame.Entities;
using DurakGame.Requests;

namespace DurakGame.Service;

public interface IGameValidator
{
    void ValidateAttackerRequest(AttackerActionRequest request, Game game);

    void ValidateDefenderRequest(DefendingActionRequest request, Game game);
}