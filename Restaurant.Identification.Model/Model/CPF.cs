namespace Restaurant.Identification.Model;

public class CPF
{

    public string Numero { get; private set; }


    public CPF(string numero)
    {
        Validator.Create()
            .IsValidCpf(numero)
            .Validate();

        Numero = numero
            .Trim()
            .Replace(".", "")
            .Replace("-", "");
    }

}
