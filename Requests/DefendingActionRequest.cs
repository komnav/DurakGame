using DurakGame.Entities;

namespace DurakGame.Requests;

public class DefendingActionRequest
{
    public List<Card> Cards { get; set; }

    public DefendingActionType Action { get; set; }
}