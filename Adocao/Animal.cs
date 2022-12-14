using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adocao
{
    internal class Animal
    {
        public int Chip { get; set; }
        public string Familia { get; set; }
        public string Raca { get; set; }
        public string Sexo { get; set; }
        public string Nome { get; set; }

        public Animal()
        {

        }

        public Animal(int chip, string familia, string raca, string sexo, string nome)
        {
            this.Chip = chip;
            this.Familia = familia;
            this.Raca = raca;
            this.Sexo = sexo;
            this.Nome = nome;
        }

        public Animal CadastrarAnimal()
        {
            string familia,raca,n,sexo;
            int chip;

            Console.Clear();
            Console.WriteLine("Cadastro de Animal: \n\n");
            Console.WriteLine("Digite o chip do Animal: ");
            chip = int.Parse(Console.ReadLine());
            Console.WriteLine("Família (cachorro/gato): ");
            familia = Console.ReadLine();
            Console.WriteLine("Raça: ");
            raca = Console.ReadLine();
            Console.WriteLine("Sexo (F ou M): "); ;
            sexo = Console.ReadLine();
            Console.WriteLine("Nome: ");
            n = Console.ReadLine();

           return new Animal (chip,familia,raca,sexo,n);
        }

        
    }
}
