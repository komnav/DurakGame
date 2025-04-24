using DurakGame.Entities;
using DurakGame.Requests;

namespace DurakGame.Service;

public interface IGameService
{
    void Start(Player player1, Player player2);

    bool IsGameOver();

    void AttackerAction(AttackerActionRequest request);

    void DefenderAction(DefendingActionRequest request);

    void ShowCardsHandPlayers();

    Player Winner();
}