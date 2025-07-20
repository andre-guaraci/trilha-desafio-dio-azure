using Azure.Data.Tables;
using Funcionarios.Models;

namespace CadastroFuncionarios.Models.Services
{
    public class LogService
    {
        private readonly TableClient _tableClient;

        public LogService(string connectionString, string tableName)
        {
            _tableClient = new TableClient(connectionString, tableName);
            _tableClient.CreateIfNotExists(); // garante que a tabela exista
        }

        public async Task SaveLogAsync(Funcionario funcionario, TipoAcao tipoAcao)
        {
            var partitionKey = $"{DateTime.UtcNow:yyyy-MM}-{funcionario.Departamento}";
            var rowKey = Guid.NewGuid().ToString();
            var log = new FuncionarioLog(funcionario, tipoAcao, partitionKey, rowKey);

            await _tableClient.AddEntityAsync(log);
        }
    }
}
