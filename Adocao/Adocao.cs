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
        public int Cod_Adocao { get; set; }

        public Adocao(string cpf, int chip, int cod_Adocao)
        {
            Cpf = cpf;
            Chip = chip;
            Cod_Adocao = cod_Adocao;
        }
    }
}
