using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adocao
{
    internal class Adotante
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Sexo { get; set; }
        public DateTime Data_Nasc { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public string Cidade { get; set; }
        public string Telefone { get; set; }

        public Adotante()
        {

        }
        public Adotante(string nome, string cpf, string sexo, DateTime data_Nasc, string logradouro, int numero, string bairro, string cep, string cidade, string telefone)
        {
            this.Nome = nome;
            this.Cpf = cpf;
            this.Sexo = sexo;
            this.Data_Nasc = data_Nasc;
            this.Logradouro = logradouro;
            this.Numero = numero;
            this.Bairro = bairro;
            this.Cep = cep;
            this.Cidade = cidade;
            this.Telefone = telefone;
        }

        public Adotante CadastrarAdotante()
        {
            string n, cpf, logradouro, bairro, cep, cidade, telefone,sexo;
            DateTime datanasc;
            int num;

            Console.Clear();
            Console.WriteLine("Cadastro do Adotante: \n");
            Console.WriteLine("Nome Completo: ");
            n = Console.ReadLine();
            Console.WriteLine("CPF: ");
            cpf = Console.ReadLine();
            Console.WriteLine("Sexo (F ou M): ");
            sexo = Console.ReadLine();
            Console.WriteLine("Data de nascimento (dd/mm/aaaa): ");
            datanasc = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Logradouro (rua/avenida): ");
            logradouro = Console.ReadLine();
            Console.WriteLine("Número: ");
            num = int.Parse(Console.ReadLine());
            Console.WriteLine("Bairro: ");
            bairro = Console.ReadLine();
            Console.WriteLine("Cep: ");
            cep = Console.ReadLine();
            Console.WriteLine("Cidade: ");
            cidade = Console.ReadLine();
            Console.WriteLine("Telefone: ");
            telefone = Console.ReadLine();

            return new Adotante(n, cpf, sexo, datanasc, logradouro, num, bairro, cep, cidade, telefone);
        }







    }
}
