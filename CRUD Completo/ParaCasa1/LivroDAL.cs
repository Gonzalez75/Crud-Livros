using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

namespace ParaCasa1
{
    class LivroDAL
    {
        private static String strConexao = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BDLivros.mdb";
        private static OleDbConnection conn = new OleDbConnection(strConexao);
        private static OleDbCommand strSQL;
        private static OleDbDataReader result;

        public static void conecta()
        {
            try
            {
                conn.Open();
            }
            catch(Exception)
            {
                Erro.setMsg("Problemas ao se conectar ao Banco de Dados");
            }
            
        }

        public static void desconecta()
        {
            conn.Close();
        }

        public static void inseriUmLivro(Livro umlivro)
        {
            String aux = "insert into TabLivro(codigo,titulo,autor,editora,ano) values (@codigo,@titulo,@autor,@editora,@ano)";

            strSQL = new OleDbCommand(aux, conn);
            strSQL.Parameters.Add("@codigo", OleDbType.VarChar).Value = umlivro.getCodigo();
            strSQL.Parameters.Add("@titulo", OleDbType.VarChar).Value = umlivro.getTitulo();
            strSQL.Parameters.Add("@autor", OleDbType.VarChar).Value = umlivro.getAutor();
            strSQL.Parameters.Add("@editora", OleDbType.VarChar).Value = umlivro.getEditora();
            strSQL.Parameters.Add("@ano", OleDbType.VarChar).Value = umlivro.getAno();
            Erro.setErro(false);
            try
            {
                strSQL.ExecuteNonQuery();
            }
            catch(Exception)
            {
                Erro.setMsg("Chave Duplicada!");
            }
        }

        public static void excluiUmLivro(Livro umlivro)
        {
            String aux = "delete from TabLivro where codigo = @codigo";

            strSQL = new OleDbCommand(aux, conn);
            strSQL.Parameters.Add("@codigo", OleDbType.VarChar).Value = umlivro.getCodigo();
            strSQL.ExecuteNonQuery();
        }
        public static void atualizaUmLivro(Livro umlivro)
        {
            String aux = "update TabLivro set titulo=@titulo, autor=@autor, editora=@editora, ano=@ano where codigo =@codigo";

            strSQL = new OleDbCommand(aux, conn);
            strSQL.Parameters.Add("@titulo", OleDbType.VarChar).Value = umlivro.getTitulo();
            strSQL.Parameters.Add("@autor", OleDbType.VarChar).Value = umlivro.getAutor();
            strSQL.Parameters.Add("@editora", OleDbType.VarChar).Value = umlivro.getEditora();
            strSQL.Parameters.Add("@ano", OleDbType.VarChar).Value = umlivro.getAno();
            strSQL.Parameters.Add("@codigo", OleDbType.VarChar).Value = umlivro.getCodigo();
            strSQL.ExecuteNonQuery();
        }

        public static void consultaUmLivro(Livro umlivro)
        {
            String aux = "select * from TabLivro where codigo = @codigo";

            strSQL = new OleDbCommand(aux, conn);
            strSQL.Parameters.Add("@codigo", OleDbType.VarChar).Value = umlivro.getCodigo();
            result = strSQL.ExecuteReader();
            Erro.setErro(false);
            if (result.Read())
            {
                umlivro.setTitulo(result.GetString(1));
                umlivro.setAutor(result.GetString(2));
                umlivro.setEditora(result.GetString(3));
                umlivro.setAno(result.GetString(4));
            }
            else
                Erro.setMsg("Livro não cadastrado.");
        }


    }
}
