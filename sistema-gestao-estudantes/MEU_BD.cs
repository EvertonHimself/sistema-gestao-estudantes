using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace sistema_gestao_estudantes
{
    // A classe do nosso banco de dados.
    internal class MEU_BD
    {
        // O "objeto" que representa a nossa conexão com o banco de dados.
        private MySqlConnection conexao = new MySqlConnection("datasource=localhost;username=root;password=;database=t4_sga_bd");
        
        // Controla o acesso a variável "conexao".
        public MySqlConnection getConexao
        {
            get 
            { 
                return conexao; 
            }
        }

        // Método para ABRIR a conexão, ou seja, iniciar a conexão
        // com o BD.
        public void abrirConexao()
        {
            // Abre a conexão SE a conexão estiver fechada.
            if (conexao.State == ConnectionState.Closed)
            {
                conexao.Open();
            }
        }

        // Método que irá fechar a conexão.
        public void fecharConexao()
        {
            // Fecha a conexão SE a conexão estiver aberta.
            if (conexao.State == ConnectionState.Open)
            {
                conexao.Close();
            }
        }
    }
}
