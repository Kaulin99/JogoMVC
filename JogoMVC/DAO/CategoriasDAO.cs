using JogoMVC.Models;
using System.Data;

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
    }
}
