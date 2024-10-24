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
                ViewBag.Operacao = "I";

                JogoViewModel jogo = new JogoViewModel();
                jogo.data_aquisicao = DateTime.Now;

                JogoDAO dao = new JogoDAO();
                jogo.id = dao.ProximoId();

                return View("Form", jogo);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel(ex.ToString()));
            }
        }

        public IActionResult Salvar(JogoViewModel jogo, string Operacao)
        {
            try
            {
                ValidaDados(jogo, Operacao);
                if (ModelState.IsValid == false)
                {
                    ViewBag.Operacao = Operacao;
                    return View("Form", jogo);
                }
                else
                {
                    JogoDAO dao = new JogoDAO();
                    if (Operacao == "I")
                        dao.Inserir(jogo);
                    else
                        dao.Alterar(jogo);

                    return RedirectToAction("Index");

                }
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
                ViewBag.Operacao = "A";

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

        public IActionResult Delete(int id)
        {
            try
            {
                JogoDAO dao = new JogoDAO();
                dao.Excluir(id);
                return RedirectToAction("index");

            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel(ex.ToString()));
            }
        }

        private void ValidaDados(JogoViewModel jogo, string Operacao)
        {
            ModelState.Clear(); //Evita aparecer erros em inglês
            JogoDAO dAO = new JogoDAO();

            if (jogo.id <= 0)
                ModelState.AddModelError("id", "ID inválido!");
            else
            {
                if (Operacao == "I" && dAO.Consulta(jogo.id) != null)
                    ModelState.AddModelError("id", "Código já está em uso.");
                if (Operacao == "A" && dAO.Consulta(jogo.id) == null) ;
                ModelState.AddModelError("id", "Código não existe!");
            }

            if (string.IsNullOrEmpty(jogo.descricao))
                ModelState.AddModelError("descricao", "Preencha o nome do jogo.");

            if (jogo.valor_locacao <= 0)
                ModelState.AddModelError("valor_locacao", "Valor não aceito");

            if (jogo.categoriaID <= 0)
                ModelState.AddModelError("categoriaID", "Informe o código da categoria");

            if (jogo.data_aquisicao > DateTime.Now)
                ModelState.AddModelError("data_aquisicao", "Data ínvalida meu querido/a viajante do tempo");
        }
    }
}
