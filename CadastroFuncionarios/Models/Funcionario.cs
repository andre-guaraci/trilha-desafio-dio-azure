using Microsoft.AspNetCore.Components.Web;
using System.Runtime.CompilerServices;

namespace Funcionarios.Models
{
    public class Funcionario
    {
        private DateTime _dataAdmissao;
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Ramal { get; set; }
        public string EmailProfissional { get; set; }
        public string Departamento { get; set; }
        public decimal Salario { get; set; }
        public DateTime DataAdmissao 
        { get => _dataAdmissao;
          set => _dataAdmissao = DateTime.SpecifyKind(value,DateTimeKind.Utc);
        }
        public bool Ativo { get; set; }

        //______________________________________________________________//

        public Funcionario() 
        {
            DataAdmissao = DateTime.Now;

        }

 
        public Funcionario(int id, string nome, string endereco, string ramal, string emailProfissional, string departamento,decimal salario, bool ativo)
        {
            Id = id;
            Nome = nome;
            Endereco = endereco;
            Ramal = ramal;
            EmailProfissional= emailProfissional;
            Departamento = departamento;
            Salario = salario;           
            Ativo = ativo;
            DataAdmissao = DateTime.Now;

        }        
    }
}