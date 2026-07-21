namespace CadastroDeClientes;

public sealed class Cliente
{
    public Cliente(string nome, string email)
    {
        Nome = nome;
        Email = email;
    }

    public string Nome { get; }
    public string Email { get; }
}
