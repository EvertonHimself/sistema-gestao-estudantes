using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sistema_gestao_estudantes
{
    public partial class AtualizarDeletarEstudante : Form
    {
        // Cria uma instância de um estudante.
        Estudante estudante = new Estudante();
        public AtualizarDeletarEstudante()
        {
            InitializeComponent();
        }

        private void buttonEnviarFoto_Click(object sender, EventArgs e)
        {
            // Pesquisa pela imagem no computador.
            OpenFileDialog abrirArquivo = new OpenFileDialog();
            abrirArquivo.Filter =
                "Seleciona a Foto(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";
            if (abrirArquivo.ShowDialog() == DialogResult.OK)
            {
                pictureBoxFoto.Image = Image.FromFile(abrirArquivo.FileName);
            }
        }

        // Verificar se todos os campos de cadastro foram
        // preenchidos.
        bool Verificar()
        {
            if ((textBoxNome.Text.Trim() == "") ||
                (textBoxSobrenome.Text.Trim() == "") ||
                (textBoxTelefone.Text.Trim() == "") ||
                (textBoxEndereco.Text.Trim() == "") ||
                (pictureBoxFoto.Image == null))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void buttonConfirmar_Click(object sender, EventArgs e)
        {
            // Atualiza as informações do estudante.
            //Estudante estudante = new Estudante();
            // Converte um número de uma caixa de texto em NÚMERO DE VERDADE.
            int id = Convert.ToInt32(textBoxID.Text);
            string nome = textBoxNome.Text;
            string sobrenome = textBoxSobrenome.Text;
            DateTime nascimento = dateTimePickerNascimento.Value;
            string telefone = textBoxTelefone.Text;
            string endereco = textBoxEndereco.Text;
            string genero = "Feminino";

            if (radioButtonMasculino.Checked)
            {
                genero = "Masculino";
            }

            // A foto do estudante em formato binário.
            MemoryStream foto = new MemoryStream();

            // Verifica se o estudante é maior de 10 anos.
            int anoDeNascimento = dateTimePickerNascimento.Value.Year;
            // Pega o ano no qual estamos.
            int anoAtual = DateTime.Now.Year;
            if (
                (anoAtual - anoDeNascimento) < 10 ||
                (anoAtual - anoDeNascimento) > 100
                )
            {
                MessageBox.Show("A idade precisa ser entre 10 e 100 anos.",
                    "Idade Inválida",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (Verificar())
            {
                pictureBoxFoto.Image.Save(foto, pictureBoxFoto.Image.RawFormat);
                if (estudante.atualizarEstudante(id, nome, sobrenome, nascimento,
                    telefone, genero, endereco, foto))
                {
                    MessageBox.Show("Informações Atualizadas!", "Sucesso!",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Erro", "Inserir Estudante",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Campos não preenchidos",
                    "Inserir Estudante", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void buttonRemover_Click(object sender, EventArgs e)
        {
            // Remove o estudante selecionado.
            int id = Convert.ToInt32(textBoxID.Text);

            // Exibe uma caixa de diálogo perguntando se realmente
            // quer remover o estudante.
            if (MessageBox.Show("Tem certeza que quer remover o estudante?", 
                "Remover Estudante", MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (estudante.deletarEstudante(id))
                {   
                    // Parâmetros: mensagem, título da janela, botão, ícone.
                    MessageBox.Show("Estudante Removido", "Remover Estudante",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Limpa os campos de textos.
                    textBoxID.Text = ""; // Obs: dará erro se tentar apagar o mesmo estudante 2x.
                    textBoxNome.Text = "";
                    textBoxSobrenome.Text = "";
                    textBoxTelefone.Text = "";
                    textBoxEndereco.Text = "";
                    dateTimePickerNascimento.Value = DateTime.Now;
                    pictureBoxFoto.Image = null;
                }
                else
                {
                    MessageBox.Show("Estudante Não Removido",
                        "Remover Estudante", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
        }

        private void buttonProcurar_Click(object sender, EventArgs e)
        {
            // Procura estudante pela ID.
            int id = Convert.ToInt32(textBoxID.Text);
            MySqlCommand comando = new MySqlCommand("SELECT `id`,`nome`,`sobrenome`,`nascimento`,`genero`,`telefone`,`endereco`,`foto` FROM `estudantes` WHERE `id`=" + id);

            DataTable tabela = estudante.pegarEstudantes(comando);

            if (tabela.Rows.Count > 0)
            {
                textBoxNome.Text = tabela.Rows[0]["nome"].ToString();
                textBoxSobrenome.Text = tabela.Rows[0]["sobrenome"].ToString();
                textBoxTelefone.Text = tabela.Rows[0]["telefone"].ToString();
                textBoxEndereco.Text = tabela.Rows[0]["endereco"].ToString();

                dateTimePickerNascimento.Value = (DateTime)tabela.Rows[0]["nascimento"];

                if (tabela.Rows[0]["genero"].ToString() == "Feminino")
                {
                    radioButtonFeminino.Checked = true;
                }
                else
                {
                    radioButtonMasculino.Checked = true;
                }

                // Foto do estudante.
                byte[] fotoDaTabela = (byte[]) tabela.Rows[0]["foto"];
                MemoryStream fotoDaInterface = new MemoryStream(fotoDaTabela);
                pictureBoxFoto.Image = Image.FromStream(fotoDaInterface);
            }
        }
    }
}
