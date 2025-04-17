using Durak.Entities;
using Durak.Requests;

namespace Durak.Service;

public interface IGameValidator
{
    void ValidateAttackerRequest(AttackerActionRequest request, Game game);

    void ValidateDefenderRequest(DefendingActionRequest request, Game game);
}