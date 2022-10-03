using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Biblioteca.Models;

namespace Biblioteca.Controllers
{
    public class Autenticacao
    {
        public static void CheckLogin(Controller controller)
        {
            if (string.IsNullOrEmpty(controller.HttpContext.Session.GetString("user")))
            {
                controller.Request.HttpContext.Response.Redirect("/Home/Login");
            }
        }
        public static void CheckAdmin(Controller controller)
        {
            if (controller.HttpContext.Session.GetString("user") != "admin")
            {
                controller.Request.HttpContext.Response.Redirect("/Home/Login");
            }
        }
        public static bool verificaLoginSenha(string login, string senha, Controller controller)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                senha = Criptografia.TextoCriptografado(senha);

                IQueryable<Usuario> usuario = bc.Usuarios.Where(u => u.Login == login && u.Senha == senha);
                List<Usuario> usuari = usuario.ToList();
                if (usuari.Count != 0)
                {

                    // HttpContext.Session.SetInt32("idUsuario", usuario.Id);
                    controller.HttpContext.Session.SetString("user", usuari[0].Login);
                    controller.HttpContext.Session.SetString("Nome", usuari[0].Nome);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}