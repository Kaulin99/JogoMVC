using JogoMVC.DAO;
using JogoWeb.Models;
using Microsoft.AspNetCore.Mvc;


namespace JogoMVC.Controllers
{
    public class JogoController : Controller
    {
        public IActionResult Index()
        {
            JogoDAO dao = new JogoDAO();
            List<JogoViewModel> lista = dao.Listagem();
            return View("Index", lista);
        }

        public IActionResult Create()
        {
            JogoViewModel jogo = new JogoViewModel();
            jogo.data_aquisicao = DateTime.Now;
            return View("Form", jogo);
        }

        public IActionResult Salvar(JogoViewModel jogo)
        {
            JogoDAO dao = new JogoDAO();
            dao.Inserir(jogo);
            return RedirectToAction("Index");
        }
    }
}
