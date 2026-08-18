using System.Text.Json;
using CadastroDeClientes;

namespace CadastroDeClientes.Repositories;

public sealed class JsonClientesRepository
{
    private static readonly JsonSerializerOptions OpcoesJson = new()
    {
        WriteIndented = true
    };

    private readonly string _caminho;

    public JsonClientesRepository(string caminho)
    {
        _caminho = caminho;
    }

    public List<Cliente> Carregar()
    {
        if (!File.Exists(_caminho))
        {
            return [];
        }

        try
        {
            string json = File.ReadAllText(_caminho);

            if (string.IsNullOrWhiteSpace(json))
            {
                return [];
            }

            return JsonSerializer.Deserialize<List<Cliente>>(
                json,
                OpcoesJson) ?? [];
        }
        catch (JsonException)
        {
            CriarBackupDoArquivoCorrompido();

            Console.WriteLine(
                "Aviso: o arquivo de clientes contém um JSON inválido.");

            return [];
        }
        catch (IOException ex)
        {
            Console.WriteLine(
                $"Não foi possível ler o arquivo de clientes: {ex.Message}");

            return [];
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine(
                $"Sem permissão para acessar o arquivo: {ex.Message}");

            return [];
        }
    }

    public bool Salvar(IReadOnlyCollection<Cliente> clientes)
    {
        string arquivoTemporario = $"{_caminho}.tmp";

        try
        {
            string? diretorio = Path.GetDirectoryName(_caminho);

            if (!string.IsNullOrWhiteSpace(diretorio))
            {
                Directory.CreateDirectory(diretorio);
            }

            string json = JsonSerializer.Serialize(
                clientes,
                OpcoesJson);

            File.WriteAllText(arquivoTemporario, json);

            File.Move(
                arquivoTemporario,
                _caminho,
                overwrite: true);

            return true;
        }
        catch (IOException ex)
        {
            Console.WriteLine(
                $"Não foi possível salvar os clientes: {ex.Message}");

            return false;
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine(
                $"Sem permissão para salvar os clientes: {ex.Message}");

            return false;
        }
        finally
        {
            TentarExcluirArquivoTemporario(arquivoTemporario);
        }
    }

    private void CriarBackupDoArquivoCorrompido()
    {
        string data = DateTime.Now.ToString("yyyyMMdd-HHmmss");

        string caminhoBackup =
            $"{_caminho}.corrompido-{data}.bak";

        try
        {
            File.Copy(
                _caminho,
                caminhoBackup,
                overwrite: true);

            Console.WriteLine(
                $"Backup do arquivo corrompido criado em: {caminhoBackup}");
        }
        catch (IOException ex)
        {
            Console.WriteLine(
                $"Não foi possível criar o backup: {ex.Message}");
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine(
                $"Sem permissão para criar o backup: {ex.Message}");
        }
    }

    private static void TentarExcluirArquivoTemporario(string caminho)
    {
        try
        {
            if (File.Exists(caminho))
            {
                File.Delete(caminho);
            }
        }
        catch (IOException)
        {
            // A aplicação poderá tentar remover o arquivo posteriormente.
        }
        catch (UnauthorizedAccessException)
        {
            // A falha na limpeza não deve encerrar a aplicação.
        }
    }
}