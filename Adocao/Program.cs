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
            do
            {
                Console.WriteLine("O que deseja fazer?\n\n1-Cadastrar Animal\n2-Cadastrar Adotante\n3-Registrar Adoção\n4-Editar Cadastro de Animal\n5-Editar Cadastro de Adotante");
                op = int.Parse(Console.ReadLine());
            } while (op < 0 || op > 5);
            switch (op)
            {
                case 1:
                    a = a.CadastrarAnimal();
                    InserirAnimal_BD(a);
                    Console.Clear();
                    Menu();
                    break;
                case 2:
                    p = p.CadastrarAdotante();
                    InserirAdotante_BD(p);
                    Console.Clear();
                    Menu();
                    break;
                case 3:
                    CadastrarAdocao();
                    Console.Clear();
                    Menu();
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
        static public void CadastrarAdocao()
        {
            Console.WriteLine("Nova adoção: \n\n");
            Adotante ad = BuscarAdotante();
            Animal an = BuscarAnimal();
            if (ad != null && an != null)
            {
                string sql = $"INSERT INTO dbo.Adocao(cpf, chip, dataAdocao) values('{ad.Cpf}','{an.Chip}', '{DateTime.Now}')";
            }
            else
            {
                Console.WriteLine("Cadastro de Adotante ou Animal não encontrado! Aperte enter para continuar...");
                Console.ReadKey();
            }
        }
        static public Adotante BuscarAdotante()
        {
            string cpf, sql = $"SELECT Nome, Cpf, Sexo, Data_nasc, Logradouro, Numero, Bairro, Cep, Cidade, Telefone FROM Adotante;";

            Console.WriteLine("Digite o CPF do adotante: ");
            cpf = Console.ReadLine();

            try
            {
                Banco conn = new Banco();
                SqlConnection conexaosql = new SqlConnection(conn.Caminho());
                conexaosql.Open();
                SqlCommand cmd = new SqlCommand(sql, conexaosql);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        Console.WriteLine("Cadastro não localizado!Aperte enter para continuar...");
                        Console.ReadKey();
                        return null;
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Nome: {reader.GetString(0)}");
                            Console.WriteLine($"CPF: {reader.GetString(1)}");
                            Console.WriteLine($"Sexo: {reader.GetString(2)}");
                            Console.WriteLine($"Data de nascimento: {reader.GetDateTime(3)}");
                            Console.WriteLine($"Logradouro: {reader.GetString(4)}");
                            Console.WriteLine($"Número: {reader.GetInt32(5)}");
                            Console.WriteLine($"Bairro: {reader.GetString(6)}");
                            Console.WriteLine($"CEP: {reader.GetString(7)}");
                            Console.WriteLine($"Cidade: {reader.GetString(8)}");
                            Console.WriteLine($"Telefone: {reader.GetString(9)}");
                            Console.WriteLine("\n\nAperte enter para confirmar.");
                            Console.ReadKey();
                            return new Adotante(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3), reader.GetString(4), reader.GetInt32(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9));   
                        }
                        conexaosql.Close();
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine($"Erro número {e.Number}, tente novamente.");
            }
            return null;
        }
        static public Animal BuscarAnimal()
        {
            string chip, sql = $"SELECT Chip,Familia,Raca,Sexo,Nome FROM Animal;";

            Console.Clear();
            Console.WriteLine("Digite o CHIP do animal: ");
            chip = Console.ReadLine();
            try
            {
                Banco conn = new Banco();
                SqlConnection conexaosql = new SqlConnection(conn.Caminho());
                conexaosql.Open();
                SqlCommand cmd = new SqlCommand(sql, conexaosql);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        Console.WriteLine("Animal não localizado...Aperte enter para continuar...");
                        Console.ReadKey();
                        return null;
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Chip de Identificação: {reader.GetInt32(0)}");
                            Console.WriteLine($"Familia: {reader.GetString(1)}");
                            Console.WriteLine($"Raça: {reader.GetString(2)}");
                            Console.WriteLine($"Sexo: {reader.GetString(3)}");
                            Console.WriteLine($"Nome: {reader.GetString(4)}\n");
                            Console.WriteLine("\n\nAperte enter para confirmar a adoção.");
                            Console.ReadKey();
                            return new Animal(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));
                            
                        }
                    }conexaosql.Close();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine("Erro código " + e.Number + "Contate o administrador");      
            }
            return null;
        }




    }

}



