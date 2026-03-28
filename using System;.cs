using System;
using System.Collections.Generic;

class Program
{
    static List<Cliente> clientes = new List<Cliente>();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n1 - Cadastrar");
            Console.WriteLine("2 - Listar");
            Console.WriteLine("3 - Sair");

            var opcao = Console.ReadLine();

            if (opcao == "1")
                Cadastrar();
            else if (opcao == "2")
                Listar();
            else
                break;
        }
    }

    static void Cadastrar()
    {
        Console.Write("Nome: ");
        string nome = Console.ReadLine();

        Console.Write("Email: ");
        string email = Console.ReadLine();

        clientes.Add(new Cliente { Nome = nome, Email = email });

        Console.WriteLine("Cliente cadastrado!");
    }

    static void Listar()
    {
        foreach (var c in clientes)
        {
            Console.WriteLine($"Nome: {c.Nome} | Email: {c.Email}");
        }
    }
}