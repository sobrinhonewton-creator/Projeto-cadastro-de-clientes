using System.Net.Mail;

namespace CadastroDeClientes;

public static class Validacoes
{
    public static bool ValidarEmail(string email)
    {
        try
        {
            var mailAddress = new MailAddress(email);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    public static bool ValidarTelefone(string telefone)
    {
        // Remove caracteres não numéricos do telefone
        string telefoneNumerico = new string(telefone.Where(char.IsDigit).ToArray());

        // Verifica se o telefone possui 10 ou 11 dígitos
        return telefoneNumerico.Length == 10 || telefoneNumerico.Length == 11;
    }
}

