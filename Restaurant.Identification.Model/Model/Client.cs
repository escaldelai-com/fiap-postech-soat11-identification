namespace Restaurant.Identification.Model;

public class Client
{

    public string Nome { get; private set; }

    public CPF CPF { get; private set; }

    public string Email { get; private set; }


    public Client(string nome, string cpf, string email)
    {
        Validator.Create()
            .IsNotNullOrWhiteSpace(nome)
            .IsValidEmail(email)
            .Validate();

        Nome = nome;
        CPF = new CPF(cpf);
        Email = email;
    }

}
