namespace Restaurant.Identification.Model;

public class NotAuthorizedException : Exception
{

    public override string Message => "Not authorized";

}
