using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_gestao_estudantes
{
    internal class Estudante
    {
        MEU_BD bancoDeDados = new MEU_BD();

        // Função que inclui o estudante no banco de dados.
        public bool inserirEstudante(string nome, string sobrenome,
            DateTime nascimento, string telefone, string genero,
            string endereco, MemoryStream foto) 
        {
            return false;
        }
    }
}
