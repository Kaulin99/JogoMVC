using JogoMVC.Models;
using JogoWeb.Models;
using System.Data;
using System.Data.SqlClient;

namespace JogoMVC.DAO
{
    public class CategoriasDAO 
    {
        public List<CategoriasViewModel> ListaCategoria()
        {
            List<CategoriasViewModel> lista = new List<CategoriasViewModel>();

            DataTable tabela = HelperDAO.ExecutaProcSelect("spListagemCategorias", null);

            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaCategoria(registro));
            return lista;
        }

        private CategoriasViewModel MontaCategoria(DataRow registro)
        {
            CategoriasViewModel c = new CategoriasViewModel()
            {
                id = Convert.ToInt32(registro["id"]),
                descricao = registro["descricao"].ToString()
            };
            return c;
        }

        public CategoriasViewModel Consulta(int id)
        {

            var p = new SqlParameter[]
            {
                new SqlParameter("id", id),
                new SqlParameter ("Tabela","Categorias")
            };

            DataTable tabela = HelperDAO.ExecutaProcSelect("spConsulta", p);

            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaCategoria(tabela.Rows[0]);
        }
    }
}
