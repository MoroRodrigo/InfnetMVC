using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfnetMVC.Models
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }

        // Chave estrangeira
        public int DepartamentoId { get; set; }

        // Propriedade de navegação para o Departamento
        public Departamento Departamento { get; set; }
    }
}
