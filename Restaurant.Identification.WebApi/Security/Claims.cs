namespace Restaurant.Identification.WebApi.Security;

public class Claims
{

    public class Client
    {
        public const string GetNoIdentify = "identification:client:get-no-identify";
        public const string GetIdentify = "identification:client:get-identify";
        public const string GetList = "identification:client:get-list";
        public const string GetById = "identification:client:get-by-id";
        public const string Register = "identification:client:register";
    }

    public static string[] All => [
        Client.GetNoIdentify,
        Client.GetIdentify,
        Client.GetList,
        Client.GetById,
        Client.Register
    ];

}
