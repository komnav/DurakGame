using Durak.Entities;

namespace Durak.Requests;

public class AttackerActionRequest
{
    public List<Card> Cards { get; set; }

    public AttackerActionType Action { get; set; }
}