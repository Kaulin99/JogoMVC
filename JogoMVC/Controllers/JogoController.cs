using JogoMVC.DAO;
using JogoWeb.Models;
using Microsoft.AspNetCore.Mvc;
using JogoMVC.Models;

namespace JogoMVC.Controllers
{
    public class JogoController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                JogoDAO dao = new JogoDAO();
                var lista = dao.Listagem();
                return View("Index", lista);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel(ex.ToString()));
            }
        }

        public IActionResult Create()
        {
            try
            {
                JogoViewModel jogo = new JogoViewModel();
                jogo.data_aquisicao = DateTime.Now;
                return View("Form", jogo);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel(ex.ToString()));
            }
        }

        public IActionResult Salvar(JogoViewModel jogo)
        {
            try
            {
                JogoDAO dao = new JogoDAO();
                if(dao.Consulta(jogo.id)==null)
                    dao.Inserir(jogo);
                else
                    dao.Alterar(jogo);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel(ex.ToString()));
            }
        }

        public IActionResult Edit(int id)
        {
            try
            {
                JogoDAO dao = new JogoDAO();
                JogoViewModel jogo = dao.Consulta(id);
                if (jogo == null)
                    return RedirectToAction("index");
                else
                    return View("Form", jogo);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel(ex.ToString()));
            }
        }

    }
}
