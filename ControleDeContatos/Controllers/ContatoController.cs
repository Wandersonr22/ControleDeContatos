using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers
{
    public class ContatoController : Controller
    {
        private readonly IContatoRepositorio _contatoRepositorio;

        public ContatoController(IContatoRepositorio contatoRepositorio)
        {
            _contatoRepositorio = contatoRepositorio;
        }

        public IActionResult Index()
        {
            var contatos = _contatoRepositorio.BuscarTodos();
            return View(contatos);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int Id)
        {
            var contato = _contatoRepositorio.ListarPorId(Id);
            return View(contato);
        }

        public IActionResult ApagarConfirmacao(int Id)
        {
            var contato = _contatoRepositorio.ListarPorId(Id);
            return View(contato);
        }

        public IActionResult Apagar(int Id)
        {
            try
            {
                var apagado = _contatoRepositorio.Apagar(Id);
                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Contato apagado com sucesso!";
                }
                else
                {
                    TempData["MensagemErro"] = "Ops, não foi possivel apagar seu contato!";
                }
                return RedirectToAction("Index");
            }
            catch (System.Exception e)
            {
                TempData["MensagemErro"] = $"Não foi possivel apagar este contato, Tente novamento, Detalhe erro: {e.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Criar(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _contatoRepositorio.Adicionar(contato);
                    TempData["MensagemSucesso"] = "Contato cadastro com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(contato);
            }
            catch (System.Exception e)
            {
                TempData["MensagemErro"] = $"ps, Não conseguimos cadastrar o seu contato, tente novamente, Detalhe do erro: {e.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Alterar(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _contatoRepositorio.Atualizar(contato);
                    TempData["MensagemSucesso"] = "Contato alterado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View("Editar", contato);
            }
            catch (System.Exception e)
            {
                TempData["MensagemErro"] = $"ps, Não conseguimos atualizar o seu contato, tente novamente, Detalhe do erro: {e.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
