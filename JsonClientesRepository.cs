using System.Text.Json;

namespace CadastroDeClientes.Repositories
{
    public class Cliente { }

    public class JsonClientesRepository
    {
        private readonly string _caminho;

        public JsonClientesRepository(string caminho)
        {
            _caminho = caminho;
        }

        public List<Cliente>? Carregar()
        {
            if (!File.Exists(_caminho))
                return [];

            string json = File.ReadAllText(_caminho);

            return JsonSerializer.Deserialize<List<Cliente>>(json) ?? [];
        }

        public void Salvar(List<Cliente> clientes)
        {
            var opcoes = new JsonSerializerOptions 
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(clientes, opcoes);
            File.WriteAllText(_caminho, json);
        }
    }
}