using System;
using System.Collections.Generic;
using System.Linq;

namespace CadastroDeClientes;

internal class Program
{
    private static readonly List<Cliente> Clientes = new();

    private static void Main()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("1 - Cadastrar cliente");
            Console.WriteLine("2 - Listar clientes");
            Console.WriteLine("3 - Sair");
            Console.Write("Escolha uma opcao: ");

            var opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    Cadastrar();
                    break;
                case "2":
                    Listar();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Opcao invalida.");
                    break;
            }
        }
    }

    private static void Cadastrar()
    {
        var nome = LerCampoObrigatorio("Nome");
        var email = LerCampoObrigatorio("Email");

        if (!email.Contains('@') || !email.Contains('.'))
        {
            Console.WriteLine("Email invalido. Cadastro cancelado.");
            return;
        }

        Clientes.Add(new Cliente(nome, email));

        Console.WriteLine("Cliente cadastrado!");
    }

    private static void Listar()
    {
        if (!Clientes.Any())
        {
            Console.WriteLine("Nenhum cliente cadastrado.");
            return;
        }

        foreach (var cliente in Clientes)
        {
            Console.WriteLine($"Nome: {cliente.Nome} | Email: {cliente.Email}");
        }
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
