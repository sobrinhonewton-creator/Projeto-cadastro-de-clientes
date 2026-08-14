using System;
using System.Collections.Generic;
using System.Linq;

namespace CadastroDeClientes;

internal class Program
{
    private static readonly List<Cliente> clientes = new List<Cliente>();

    private static void Main()
{
    while (true)
    {
        Console.Clear();

        Console.WriteLine();
        Console.WriteLine("        CUSTOMER MANAGER .NET");
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("1 - Cadastrar cliente");
        Console.WriteLine("2 - Listar clientes");
        Console.WriteLine("3 - Buscar cliente");
        Console.WriteLine("4 - Editar cliente");
        Console.WriteLine("5 - Excluir cliente");
        Console.WriteLine("6 - Estatísticas");
        Console.WriteLine("0 - Sair");
        Console.WriteLine();

        Console.Write("Escolha uma opção: ");

        string? opcao = Console.ReadLine();

        switch (opcao)
        {
            case "1":
                Cadastrar();
                break;

            case "2":
                Listar();
                break;

            case "3":
                Buscar();
                break;

            case "4":
                Editar();
                break;

            case "5":
                Excluir();
                break;

            case "6":
                ExibirEstatisticas();
                break;

            case "0":
                return;

            default:
                Console.WriteLine("Opção inválida.");
                Pausar();
                break;
        }
    }
}

    private static void Buscar()
{
    Console.Clear();

    Console.WriteLine("=== BUSCAR CLIENTE ===");
    Console.WriteLine();

    Console.Write("Informe o ID do cliente: ");

    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("ID inválido.");
        Pausar();
        return;
    }

    Cliente? cliente = clientes.FirstOrDefault(c => c.Id == id);

    if (cliente == null)
    {
        Console.WriteLine("Cliente não encontrado.");
        Pausar();
        return;
    }

    Console.WriteLine();
    Console.WriteLine($"ID: {cliente.Id}");
    Console.WriteLine($"Nome: {cliente.Nome}");
    Console.WriteLine($"E-mail: {cliente.Email}");
    Console.WriteLine($"Telefone: {cliente.Telefone}");

    Pausar();
}

    private static void Editar()
{
    Console.Clear();

    Console.WriteLine("=== EDITAR CLIENTE ===");
    Console.WriteLine();

    Console.Write("Informe o ID do cliente: ");

    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("ID inválido.");
        Pausar();
        return;
    }

    Cliente? cliente = clientes.FirstOrDefault(c => c.Id == id);

    if (cliente == null)
    {
        Console.WriteLine("Cliente não encontrado.");
        Pausar();
        return;
    }

    Console.WriteLine();
    Console.WriteLine($"Cliente selecionado: {cliente.Nome}");
    Console.WriteLine("Deixe o campo vazio para manter o valor atual.");
    Console.WriteLine();

    Console.Write($"Nome ({cliente.Nome}): ");
    string novoNome = Console.ReadLine()?.Trim() ?? "";

    Console.Write($"E-mail ({cliente.Email}): ");
    string novoEmail = Console.ReadLine()?.Trim() ?? "";

    Console.Write($"Telefone ({cliente.Telefone}): ");
    string novoTelefone = Console.ReadLine()?.Trim() ?? "";

    if (!string.IsNullOrWhiteSpace(novoNome))
    {
        cliente.Nome = novoNome;
    }

    if (!string.IsNullOrWhiteSpace(novoEmail))
    {
        if (!novoEmail.Contains("@"))
        {
            Console.WriteLine("O novo e-mail é inválido.");
            Pausar();
            return;
        }

        bool emailEmUso = clientes.Any(c =>
            c.Id != cliente.Id &&
            c.Email.Equals(
                novoEmail,
                StringComparison.OrdinalIgnoreCase
            )
        );

        if (emailEmUso)
        {
            Console.WriteLine("Esse e-mail já pertence a outro cliente.");
            Pausar();
            return;
        }

        cliente.Email = novoEmail;
    }

    if (!string.IsNullOrWhiteSpace(novoTelefone))
    {
        cliente.Telefone = novoTelefone;
    }

    Console.WriteLine();
    Console.WriteLine("Cliente atualizado com sucesso.");

    Pausar();
}

    private static void Excluir()
{
    Console.Clear();

    Console.WriteLine("=== EXCLUIR CLIENTE ===");
    Console.WriteLine();

    Console.Write("Informe o ID do cliente: ");

    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("ID inválido.");
        Pausar();
        return;
    }

    Cliente? cliente = clientes.FirstOrDefault(c => c.Id == id);

    if (cliente == null)
    {
        Console.WriteLine("Cliente não encontrado.");
        Pausar();
        return;
    }

    Console.WriteLine();
    Console.WriteLine($"Cliente: {cliente.Nome}");
    Console.Write("Confirma a exclusão? (S/N): ");

    string resposta = Console.ReadLine()?.Trim() ?? "";

    if (!resposta.Equals("S", StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine("Exclusão cancelada.");
        Pausar();
        return;
    }

    clientes.Remove(cliente);

    Console.WriteLine("Cliente excluído com sucesso.");

    Pausar();
}

    private static void ExibirEstatisticas()
{
    Console.Clear();

    Console.WriteLine("=== ESTATÍSTICAS ===");
    Console.WriteLine();

    int totalClientes = clientes.Count;

    int comTelefone = clientes.Count(c =>
        !string.IsNullOrWhiteSpace(c.Telefone)
    );

    int semTelefone = clientes.Count(c =>
        string.IsNullOrWhiteSpace(c.Telefone)
    );

    Console.WriteLine($"Total de clientes: {totalClientes}");
    Console.WriteLine($"Clientes com telefone: {comTelefone}");
    Console.WriteLine($"Clientes sem telefone: {semTelefone}");

    Pausar();
}

    private static void Cadastrar()
{
    Console.Clear();

    Console.WriteLine("=== CADASTRAR CLIENTE ===");
    Console.WriteLine();

    Console.Write("Nome: ");
    string nome = Console.ReadLine()?.Trim() ?? "";

    if (string.IsNullOrWhiteSpace(nome))
    {
        Console.WriteLine("O nome é obrigatório.");
        Pausar();
        return;
    }

    Console.Write("E-mail: ");
    string email = Console.ReadLine()?.Trim() ?? "";

    if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
    {
        Console.WriteLine("E-mail inválido.");
        Pausar();
        return;
    }

    bool emailExiste = clientes.Any(c =>
        c.Email.Equals(
            email,
            StringComparison.OrdinalIgnoreCase
        )
    );

    if (emailExiste)
    {
        Console.WriteLine("Já existe um cliente cadastrado com esse e-mail.");
        Pausar();
        return;
    }

    Console.Write("Telefone: ");
    string telefone = Console.ReadLine()?.Trim() ?? "";

    if (string.IsNullOrWhiteSpace(telefone))
    {
        Console.WriteLine("O telefone é obrigatório.");
        Pausar();
        return;
    }

    int novoId = clientes.Count == 0
        ? 1
        : clientes.Max(c => c.Id) + 1;

    Cliente cliente = new Cliente
    {
        Id = novoId,
        Nome = nome,
        Email = email,
        Telefone = telefone
    };

    clientes.Add(cliente);

    Console.WriteLine();
    Console.WriteLine($"Cliente cadastrado com sucesso. ID: {cliente.Id}");

    Pausar();
}
static void Pausar()
{
    Console.WriteLine();
    Console.WriteLine("Pressione qualquer tecla para continuar...");
    Console.ReadKey();
}

    private static void Listar()
{
    Console.Clear();

    Console.WriteLine("=== CLIENTES CADASTRADOS ===");
    Console.WriteLine();

    if (!clientes.Any())
    {
        Console.WriteLine("Nenhum cliente cadastrado.");
        Pausar();
        return;
    }

    foreach (var cliente in clientes)
    {
        Console.WriteLine($"ID: {cliente.Id}");
        Console.WriteLine($"Nome: {cliente.Nome}");
        Console.WriteLine($"E-mail: {cliente.Email}");
        Console.WriteLine($"Telefone: {cliente.Telefone}");
        Console.WriteLine($"Cadastro: {cliente.DataCadastro:dd/MM/yyyy HH:mm}");
        Console.WriteLine("--------------------------------");
    }

    Pausar();
}
    private static string LerCampoObrigatorio(string rotulo)
    {
        while (true)
        {
            Console.Write($"{rotulo}: ");
            var valor = Console.ReadLine()?.Trim();

            if (!string.IsNullOrWhiteSpace(valor))
            {
                return valor;
            }

            Console.WriteLine($"{rotulo} e obrigatorio.");
        }
    }
}
