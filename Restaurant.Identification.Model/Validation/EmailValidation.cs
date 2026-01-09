using System.Net.Mail;

namespace Restaurant.Identification.Model;

public static class EmailValidation
{

    public static Validator IsValidEmail(this Validator validator, string value)
    {
        return validator.Test(IsValidEmail(value), "Invalid e-mail");
    }


    private static bool IsValidEmail(string email)
    {
        try
        {
            _ = new MailAddress(email);

            return true;
        }
        catch
        {
            return false;
        }
    }

}
