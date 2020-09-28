using Correios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsultandoCEPAPI
{
    public partial class Teste : Form
    {
        public Teste()
        {
            InitializeComponent();
        }

        private void Teste_Load(object sender, EventArgs e)
        {

        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCEP.Text))
                MessageBox.Show("O Campo CEP esta vazio.","Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                CorreiosApi correiosApi = new CorreiosApi();
                var retorno = correiosApi.consultaCEP(txtCEP.Text);

                if (retorno is null)
                {
                    MessageBox.Show("CEP Não encontrado.", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                txtBairro.Text = retorno.bairro;
                txtCidade.Text = retorno.cidade;
                txtEndereco.Text = retorno.end;
                txtEstado.Text = retorno.uf;
            }
        }

        private void brnSair_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Sair da Aplicação?", "Saindo...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Application.Exit();
        }
    }
}
