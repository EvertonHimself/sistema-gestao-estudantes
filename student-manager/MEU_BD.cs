using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data; // eu digitei essa linha.

namespace student_manager
{
    internal class MEU_BD
    {
        // "conexao" é o OBJETO que REPRESENTA a conexão com o
        // banco de dados.
        private MySqlConnection conexao = new MySqlConnection("datasource=localhost;username=root;password=;database=t5_sga_bd");

        // Versão pública da variável/objeto "conexao".
        public MySqlConnection getConexao
        {
            get
            {
                return conexao;
            }
        }

        // Método para ABRIR/INICIALIZAR a conexão.
        public void abrirConexao() 
        {
            if (conexao.State == ConnectionState.Closed)
            {
                conexao.Open();
            }
        }

        // Método para FECHAR/ENCERAR a conexão.
        public void fecharConexao()
        {
            if (conexao.State == ConnectionState.Open)
            {
                conexao.Close();
            }
        }
    }
}
