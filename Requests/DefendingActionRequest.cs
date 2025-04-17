using Durak.Entities;

namespace Durak.Requests;

public class DefendingActionRequest
{
    public List<Card> Cards { get; set; }

    public DefendingActionType Action { get; set; }
}