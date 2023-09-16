using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace student_manager
{
    public partial class Login_Form : Form
    {
        public Login_Form()
        {
            InitializeComponent();
        }

        private void Login_Form_Load(object sender, EventArgs e)
        {
            // Coloca a imagem user.png na picture box da janela.
            pictureBox1.Image = Image.FromFile("../../imagens/user.png");
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            MEU_BD bancoDeDados = new MEU_BD();

            MySqlDataAdapter adaptador = new MySqlDataAdapter();
            DataTable tabela = new DataTable();
            MySqlCommand comando = new MySqlCommand("SELECT * FROM `usuarios` WHERE `nome_de_usuario` = @usn AND `senha` = @psw", bancoDeDados.getConexao);

            // Atualiza os valores dos parâmetros @usn e @psw, do comando acima.
            comando.Parameters.Add("@usn", MySqlDbType.VarChar).Value = txtUsuario.Text;
            comando.Parameters.Add("@psw", MySqlDbType.VarChar).Value = txtSenha.Text;

            adaptador.SelectCommand = comando;

            adaptador.Fill(tabela);

            if (tabela.Rows.Count > 0)
            {
                //MessageBox.Show("YES");
                this.DialogResult = DialogResult.OK;
            }
            else 
            {
                MessageBox.Show("Usuário ou senha inválidos", 
                    "Erro de Login", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
