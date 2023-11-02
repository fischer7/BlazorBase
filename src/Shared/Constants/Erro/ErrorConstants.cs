using GlobalShared.Wrapper;

namespace GlobalShared.Constants.Erro;
public sealed class ErrorConstants
{
    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error NullValue = new("Error.NullValue", "The specified result value is null.");
    public static readonly Error UserPassNullValue = new("User/Pass", "User or password cannot be null");
    public static readonly Error UserOrPassInvalid = new("User.Login", "User or Password incorrect.");
    public static readonly Error InvalidLoginToken = new("User.Login", "Invalid Operation with Token.");
    public static readonly Error InvalidPatronNumberIndex = new("PatronNumber", "Invalid Patron Number Index operation.");
    public static readonly Error DatabaseFail = new("Fail to save", "Fail to save to the database.");
}