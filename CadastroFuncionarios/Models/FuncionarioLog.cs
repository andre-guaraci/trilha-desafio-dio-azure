using Azure;
using Azure.Data.Tables;
using Funcionarios.Models;
using System.Text.Json;
using CadastroFuncionarios.Models.Services;

public class FuncionarioLog : ITableEntity
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Endereco { get; set; }
    public string Ramal { get; set; }
    public string EmailProfissional { get; set; }
    public string Departamento { get; set; }
    public decimal Salario { get; set; }
    private DateTime _dataAdmissao;
    public DateTime DataAdmissao
    {
        get => _dataAdmissao;
        set => _dataAdmissao = DateTime.SpecifyKind(value, DateTimeKind.Utc);

    }

    public TipoAcao TipoAcao { get; set; }
    public string JSON { get; set; }
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;
    public ETag ETag { get; set; }
    DateTimeOffset? ITableEntity.Timestamp { get => Timestamp; set => throw new NotImplementedException(); }

    public FuncionarioLog() 
    {
        DataAdmissao = DateTime.Now;
    }

    public FuncionarioLog(Funcionario funcionario, TipoAcao tipoAcao, string partitionKey, string rowKey)
    {
        Id = funcionario.Id;
        Nome = funcionario.Nome;
        Endereco = funcionario.Endereco;
        Ramal = funcionario.Ramal;
        EmailProfissional = funcionario.EmailProfissional;
        Departamento = funcionario.Departamento;
        Salario = funcionario.Salario;
        DataAdmissao = DateTime.Now;
        

        TipoAcao = tipoAcao;
        JSON = JsonSerializer.Serialize(funcionario);
        PartitionKey = AzureTableKeyHelper.CreateValidPartitionKey(funcionario.Departamento);
        RowKey = AzureTableKeyHelper.CreateValidRowKey(DateTime.UtcNow, funcionario.Id);
    }
}