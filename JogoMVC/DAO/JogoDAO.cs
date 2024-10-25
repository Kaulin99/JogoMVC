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
             HelperDAO.ExecutaProc("spIncluiJogos", CriaParametros(jogo));
        }
        public void Alterar(JogoViewModel jogo)

        {
             HelperDAO.ExecutaProc("spAlteraJogos", CriaParametros(jogo));
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
            var p = new SqlParameter[]
            {
                new SqlParameter("id", id)
            };

            HelperDAO.ExecutaProc("spDelete", p);
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

            var p = new SqlParameter[]
            {
                new SqlParameter ("id", id)
            };

            DataTable tabela = HelperDAO.ExecutaProcSelect("spConsulta", p);

            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaAluno(tabela.Rows[0]);
        }
        
        public List<JogoViewModel> Listagem()
        {
            List<JogoViewModel> lista = new List<JogoViewModel>();
            var p = new SqlParameter[]
           {
                new SqlParameter ("Tabela","jogos")
           };

            DataTable tabela = HelperDAO.ExecutaProcSelect("spLista", p);

            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaAluno(registro));
            return lista;
        }

        public int ProximoId()
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("tabela", "jogos")
            };

            DataTable tabela = HelperDAO.ExecutaProcSelect("spProximoId", p);
            return Convert.ToInt32(tabela.Rows[0]["MAIOR"]);
        }


    }
}
