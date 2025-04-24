namespace DurakGame.Exceptions;

public class PlayerInvalidRequestException : Exception
{
    public PlayerInvalidRequestExceptionCodes Code { get; }

    public PlayerInvalidRequestException(PlayerInvalidRequestExceptionCodes code)
        : base($"Invalid client request exception Code is {code}")
    {
        Code = code;
    }
}