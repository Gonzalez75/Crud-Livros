using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ParaCasa1
{
    public partial class CadLivrosUIL : Form
    {
        Livro umlivro = new Livro();
        public CadLivrosUIL()
        {
            InitializeComponent();
        }

        private void CadLivrosUIL_Load(object sender, EventArgs e)
        {
            LivroDAL.conecta();
            if (Erro.getErro())
            {
                MessageBox.Show("Banco de Dados não localizado - contate o suporte");
                Application.Exit();
            }
            else
                LivroDAL.populaDR();
        }

        private void CadLivrosUIL_FormClosed(object sender, FormClosedEventArgs e)
        {
            LivroDAL.desconecta();
        }

        private void mostra()
        {
            textBox1.Text = umlivro.getCodigo();
            textBox2.Text = umlivro.getTitulo();
            textBox3.Text = umlivro.getAutor();
            textBox4.Text = umlivro.getEditora();
            textBox5.Text = umlivro.getAno();
        }
         private void button1_Click_1(object sender, EventArgs e)
        {
            umlivro = LivroDAL.getPrimeiro();
            mostra();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            umlivro = LivroDAL.getAnterior();
            mostra();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            umlivro = LivroDAL.getProximo();
            mostra();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            umlivro = LivroDAL.getUltimo();
            mostra();
        }

    }
}
