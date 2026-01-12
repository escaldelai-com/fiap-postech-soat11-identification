using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Restaurant.Identification.Model;

public static class CpfValidation
{

    private static readonly CultureInfo ptBR = new("pt-BR");

    public static Validator IsValidCpf(this Validator validator, string value)
    {
        return validator.Test(IsValidCpf(value), "Invalid CPF.");
    }


    private static bool IsValidCpf(string cpf)
    {
        try
        {
            int[] multiplicador1 = [10, 9, 8, 7, 6, 5, 4, 3, 2];
            int[] multiplicador2 = [11, 10, 9, 8, 7, 6, 5, 4, 3, 2];

            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;

            for (int j = 0; j < 10; j++)
                if (j.ToString(ptBR).PadLeft(11, char.Parse(j.ToString(ptBR))) == cpf)
                    return false;

            var tempCpf = cpf[..9];
            var soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString(ptBR), ptBR) * multiplicador1[i];

            var resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            var digito = resto.ToString(ptBR);
            tempCpf += digito;
            soma = 0;
            for (var i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString(ptBR), ptBR) * multiplicador2[i];

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            digito += resto.ToString(ptBR);

            return cpf.EndsWith(digito, StringComparison.Ordinal);
        }
        catch
        {
            return false;
        }
    }

}
