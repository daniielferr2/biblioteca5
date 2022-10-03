using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Cadastro()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.CheckAdmin(this);
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(Usuario u)
        {
            UsuarioService usuarioService = new UsuarioService();

            if(u.Id == 0)
            {
                usuarioService.Inserir(u);
            }
            else
            {
                usuarioService.Atualizar(u);
            }

            return RedirectToAction("Listagem");
        }

        public IActionResult Listagem()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.CheckAdmin(this);
            UsuarioService usuarioService = new UsuarioService();
            return View(usuarioService.ListarTodos());
        }

        public IActionResult Edicao(int id)
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.CheckAdmin(this);
            UsuarioService us = new UsuarioService();
            Usuario u = us.ObterPorId(id);
            return View(u);
        }
        public IActionResult Excluir(int Id)
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.CheckAdmin(this);
            UsuarioService us = new UsuarioService();
            Usuario u = us.ObterPorId(Id);
            us.Exclui(u);
            return RedirectToAction("Listagem");
        }
    }
}