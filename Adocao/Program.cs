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
                Console.WriteLine("O que deseja fazer?\n\n1-Cadastrar Animal\n2-Cadastrar Adotante\n3-Registrar Adoção\n4-Editar Cadastros");
                op = int.Parse(Console.ReadLine());
            } while (op < 0 || op > 4);
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
                    EditarCadastro();
                    Console.Clear();
                    Menu();
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
                Banco conn = new Banco();
                SqlConnection conexaosql = new SqlConnection(conn.Caminho());
                conexaosql.Open();
                string sql = $"INSERT INTO dbo.Adocao(cpf, chip, dataAdocao) values('{ad.Cpf}','{an.Chip}', '{DateTime.Now}')";
                SqlCommand cmd = new SqlCommand(sql, conexaosql);
                cmd.ExecuteNonQuery();
                conexaosql.Close();
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
                            Console.WriteLine($"Nome: {reader.GetString(4)}");
                            Console.WriteLine("\n\nAperte enter para confirmar a adoção.");
                            Console.ReadKey();
                            Console.ReadKey();
                            return new Animal(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));

                        }
                    }
                    conexaosql.Close();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine("Erro código " + e.Number + "Contate o administrador");
            }
            return null;
        }
        static public void EditarCadastro()
        {
            int op;
            Console.Clear();
            Console.WriteLine("Qual cadastro deseja editar?\n1- Adotante\n2- Animal");
            op = int.Parse(Console.ReadLine());

            if (op == 1)
            {
                Adotante ad = BuscarAdotante();
                if (ad != null)
                {
                    Banco conn = new Banco();
                    SqlConnection conexaosql = new SqlConnection(conn.Caminho());
                    conexaosql.Open();

                    int opcao;
                    Console.Clear();
                    Console.WriteLine("Qual informação deseja editar?\n1-Nome\n2-Sexo\n3-Data de Nascimento\n4-Endereço\n5-Telefone");
                    opcao = int.Parse(Console.ReadLine());
                    switch (opcao)
                    {
                        case 1:
                            Console.Clear();
                            Console.WriteLine("Digite o nome: ");
                            string nome = Console.ReadLine();
                            string sql = $"UPDATE Adotante SET nome VALUE {nome} WHERE cpf = {ad.Cpf}";
                            SqlCommand cmd = new SqlCommand(sql, conexaosql);
                            cmd.ExecuteNonQuery();
                            conexaosql.Close();
                            break;
                        case 2:
                            Console.Clear();
                            Console.WriteLine("Digite o Sexo: ");
                            string sexo = Console.ReadLine();
                            string sqls = $"UPDATE Adotante SET sexo VALUE {sexo} WHERE cpf = {ad.Cpf}";
                            SqlCommand cmds = new SqlCommand(sqls, conexaosql);
                            cmds.ExecuteNonQuery();
                            conexaosql.Close();
                            break;
                        case 3:
                            Console.Clear();
                            Console.WriteLine("Digite a Data de Nascimento: ");
                            DateTime data = DateTime.Parse(Console.ReadLine());
                            string sqld = $"UPDATE Adotante SET sexo VALUE {data} WHERE cpf = {ad.Cpf}";
                            SqlCommand cmdd = new SqlCommand(sqld, conexaosql);
                            cmdd.ExecuteNonQuery();
                            conexaosql.Close();
                            break;
                        case 4:
                            Console.Clear();
                            Console.WriteLine("Digite o logradouro: ");
                            string logradouro = Console.ReadLine();
                            Console.WriteLine("Digite o numero: ");
                            int num = int.Parse(Console.ReadLine());
                            Console.WriteLine("Digite o bairro: ");
                            string bairro = Console.ReadLine();
                            Console.WriteLine("Digite o CEP: ");
                            string cep = Console.ReadLine();
                            Console.WriteLine("Digite a Cidade: ");
                            string cidade = Console.ReadLine();
                            string sqle = $"UPDATE Adotante SET (Logradouro,numero,bairro,cep,cidade) VALUES {logradouro},{num},{bairro},{cep},{cidade} WHERE cpf = {ad.Cpf};";
                            SqlCommand cmde = new SqlCommand(sqle, conexaosql);
                            cmde.ExecuteNonQuery();
                            conexaosql.Close();
                            break;
                        case 5:
                            Console.Clear();
                            Console.WriteLine("Digite o Telefone: ");
                            string tel = Console.ReadLine();
                            string sqlt = $"UPDATE Adotante SET sexo VALUE {tel} WHERE cpf = {ad.Cpf}";
                            SqlCommand cmdt = new SqlCommand(sqlt, conexaosql);
                            cmdt.ExecuteNonQuery();
                            conexaosql.Close();
                            break;
                    }
                }
                else if (op == 2)
                {
                    Animal an = BuscarAnimal();
                    if (an != null)
                    {
                        Banco conn = new Banco();
                        SqlConnection conexaosql = new SqlConnection(conn.Caminho());
                        conexaosql.Open();

                        int opcao;
                        Console.Clear();
                        Console.WriteLine("Qual informação deseja editar?\n1-Familia\n2-Raça\n3-Sexo\n4-Nome");
                        opcao = int.Parse(Console.ReadLine());
                        switch (opcao)
                        {
                            case 1:
                                Console.Clear();
                                Console.WriteLine("Digite a familia: ");
                                string familia = Console.ReadLine();
                                string sql = $"UPDATE Animal SET familia VALUE {familia} WHERE chip = {an.Chip}";
                                SqlCommand cmd = new SqlCommand(sql, conexaosql);
                                cmd.ExecuteNonQuery();
                                conexaosql.Close();
                                break;
                            case 2:
                                Console.Clear();
                                Console.WriteLine("Digite a raça: ");
                                string raca = Console.ReadLine();
                                string sqlr = $"UPDATE Animal SET raca VALUE {raca} WHERE chip = {an.Chip}";
                                SqlCommand cmdr = new SqlCommand(sqlr, conexaosql);
                                cmdr.ExecuteNonQuery();
                                conexaosql.Close();
                                break;
                            case 3:
                                Console.Clear();
                                Console.WriteLine("Digite o sexo: ");
                                string sexo = Console.ReadLine();
                                string sqls = $"UPDATE Animal SET familia VALUE {sexo} WHERE chip = {an.Chip}";
                                SqlCommand cmds = new SqlCommand(sqls, conexaosql);
                                cmds.ExecuteNonQuery();
                                conexaosql.Close();
                                break;
                            case 4:
                                Console.Clear();
                                Console.WriteLine("Digite o nome: ");
                                string nome = Console.ReadLine();
                                string sqln = $"UPDATE Animal SET familia VALUE {nome} WHERE chip = {an.Chip}";
                                SqlCommand cmdn = new SqlCommand(sqln, conexaosql);
                                cmdn.ExecuteNonQuery();
                                conexaosql.Close();
                                break;
                            default:
                                Console.WriteLine("Opção inválida!");
                                break;
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Cadastro de Adotante ou Animal não encontrado! Aperte enter para continuar...");
                        Console.ReadKey();
                    }
                }

            }

        }

    }
}



