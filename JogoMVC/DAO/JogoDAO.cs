using JogoWeb.Models;
using Microsoft.AspNetCore.Http.Json;
using System.Data;
using System.Data.SqlClient;

namespace JogoMVC.DAO
{
    public class JogoDAO

    {
        public void Inserir(JogoViewModel jogo)

        {
            string sql =
            "insert into jogos(id, descricao, valor_locacao, data_aquisicao, categoriaID)"
           +
            "values ( @id, @descricao, @valor_locacao, @data_aquisicao, @categoriaID )"
           ;
            HelperDAO.ExecutaSQL(sql, CriaParametros(jogo));

        }
        public void Alterar(JogoViewModel jogo)

        {
            string sql =
            "update jogos set descricao = @descricao, " +
            "valor_locacao = @valor_locacao, " +
            "categoriaID = @categoriaID," +
            "data_aquisicao = @data_aquisicao where id = @id"
       ;
            HelperDAO.ExecutaSQL(sql, CriaParametros(jogo));

        }
        private SqlParameter[] CriaParametros(JogoViewModel jogo)

        {
            SqlParameter[] parametros = new SqlParameter[5];
            parametros[0] = new SqlParameter("id", jogo.id);
            parametros[1] = new SqlParameter("descricao", jogo.descricao);
            if (jogo.valor_locacao == null)
                parametros[2] = new SqlParameter("valor_locacao", DBNull.Value);
            else
                parametros[2] = new SqlParameter("valor_locacao", jogo.valor_locacao);
            parametros[3] = new SqlParameter("categoriaID", jogo.categoriaID);
            parametros[4] = new SqlParameter("data_aquisicao", jogo.data_aquisicao);
            return parametros;

        }
        public void Excluir(int id)

        {
            string sql = "delete jogos where id =" + id;
            HelperDAO.ExecutaSQL(sql, null);
        }

        private JogoViewModel MontaAluno(DataRow registro)

        {
            JogoViewModel a = new JogoViewModel();
            a.id = Convert.ToInt32(registro["id"]);
            a.descricao = registro["descricao"].ToString();
            a.categoriaID = Convert.ToInt32(registro["categoriaID"]);
            a.data_aquisicao = Convert.ToDateTime(registro["data_aquisicao"]);
            if (registro["valor_locacao"] != DBNull.Value)
                a.valor_locacao = Convert.ToDouble(registro["valor_locacao"]);
            return a;
        }

        public JogoViewModel Consulta(int id)
        {
            string sql = "select * from jogos where id = " + id;

            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);

            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaAluno(tabela.Rows[0]);
        }
        public List<JogoViewModel> Listagem()
        {
            List<JogoViewModel> lista = new List<JogoViewModel>();
            string sql = "select * from jogos order by descricao";

            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaAluno(registro));
            return lista;
        }
    }
}
