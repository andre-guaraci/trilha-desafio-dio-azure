namespace CadastroFuncionarios.Models.Services
{
    public static class AzureTableKeyHelper
    {
        public static string CreateValidPartitionKey(string input)
        {
            // Remove caracteres inválidos
            var invalidChars = new[] { '/', '\\', '#', '?', '\t', '\n', '\r' };
            var validKey = new string(input
                .Where(c => !invalidChars.Contains(c))
                .ToArray());

            // Limita o tamanho
            return validKey.Length > 900 ? validKey.Substring(0, 900) : validKey;
        }

        public static string CreateValidRowKey(DateTime timestamp, int id)
        {
            return $"{timestamp:yyyyMMddHHmmss}-{id}";
        }
    }
}
