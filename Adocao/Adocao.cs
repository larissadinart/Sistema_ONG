using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adocao
{
    internal class Adocao
    {
        public string Cpf { get; set; }
        public int Chip { get; set; }
        public DateTime Data_Adocao { get; set; }

        public Adocao(string cpf, int chip, DateTime data_Adocao)
        {
            Cpf = cpf;
            Chip = chip;
            Data_Adocao = data_Adocao;
        }
    }
}
