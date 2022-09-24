using System;
using System.Data;
using System.Data.SqlClient;

namespace Adocao
{
    internal class Program
    {
       
        static Adotante p = new Adotante();
        static Animal a = new Animal();
        static void Main(string[] args)
        {
            Menu();
        }

        static public void Menu()
        {
            int op;
            Console.WriteLine("O que deseja fazer?\n\n1-Cadastrar Animal\n2-Cadastrar Adotante\n3-Registrar Adoção\n4-Editar Cadastro de Animal\n5-Editar Cadastro de Adotante");
            op = int.Parse(Console.ReadLine());
            switch (op)
            {
                case 1:
                    a = a.CadastrarAnimal();
                    InserirAnimal_BD(a);
                    Console.WriteLine("Animal cadastrado com sucesso!\n\nAperte enter para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                    Menu();
                    break;
                case 2:
                    p = p.CadastrarAdotante();
                    InserirAdotante_BD(p);
                    Console.WriteLine("Adotante cadastrado com sucesso!\n\nAperte enter para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                    Menu();
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        }
        static public void InserirAdotante_BD(Adotante adotante)
        {
            Banco conn = new Banco();
            SqlConnection conexaosql = new SqlConnection(conn.Caminho());
            conexaosql.Open();
            string sql = $"insert into Adotante(nome, cpf, sexo, data_nasc, logradouro, numero, bairro,cep,cidade,telefone) values ('{p.Nome}' , " +
                $"'{p.Cpf}', '{p.Sexo}', '{p.Data_Nasc}', '{p.Logradouro}', '{p.Numero}', '{p.Bairro}', '{p.Cep}','{p.Cidade}','{p.Telefone}');";
            SqlCommand cmd = new SqlCommand(sql, conexaosql);
            cmd.ExecuteNonQuery();
            conexaosql.Close();
        }

        static public void InserirAnimal_BD(Animal animal)
        {
            Banco conn = new Banco();
            SqlConnection conexaosql = new SqlConnection(conn.Caminho());
            conexaosql.Open();
            string sql = $"insert into Animal(chip, familia, raca, sexo,nome) values ('{a.Chip}' , " +
                $"'{a.Familia}', '{a.Raca}', '{a.Sexo}', '{a.Nome}');";
            SqlCommand cmd = new SqlCommand(sql, conexaosql);
            cmd.ExecuteNonQuery();
            conexaosql.Close();
        }
    }

}
