using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Models
{
    public class EmprestimoService
    {
        public void Inserir(Emprestimo e)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                bc.Emprestimos.Add(e);
                bc.SaveChanges();
            }
        }

        public void Atualizar(Emprestimo e)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                Emprestimo emprestimo = bc.Emprestimos.Find(e.Id);
                emprestimo.NomeUsuario = e.NomeUsuario;
                emprestimo.Telefone = e.Telefone;
                emprestimo.LivroId = e.LivroId;
                emprestimo.DataEmprestimo = e.DataEmprestimo;
                emprestimo.DataDevolucao = e.DataDevolucao;

                bc.SaveChanges();
            }
        }

        public ICollection<Emprestimo> ListarTodos(int page, int size, FiltrosEmprestimos filtro = null)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                int pular = (page - 1) * size;
                IQueryable<Emprestimo> query;

                if (filtro != null)
                {
                    switch (filtro.TipoFiltro)
                    {
                        case "Usuario":
                            query = bc.Emprestimos.Where(e => e.NomeUsuario.Contains(filtro.Filtro));

                            break;

                        case "Livro":

                            List<Livro> LivrosFiltrados = bc.Livros.Where(e => e.Titulo.Contains(filtro.Filtro)).ToList();

                            List<int> LivrosIds = new List<int>();
                            for (int i = 0; i < LivrosFiltrados.Count; i++)
                            { LivrosIds.Add(LivrosFiltrados[i].Id); }
                            query = bc.Emprestimos.Where(e => LivrosIds.Contains(e.LivroId));
                            break;

                        default:
                            query = bc.Emprestimos;
                            break;
                    }
                }
                else
                {
                    query = bc.Emprestimos;
                }

                List<Emprestimo> listaConsulta = query.OrderByDescending(e => e.DataDevolucao).Skip(pular).Take(size).ToList();

                for (int i = 0; i < listaConsulta.Count; i++)
                {
                    listaConsulta[i].Livro = bc.Livros.Find(listaConsulta[i].LivroId);
                }
                return listaConsulta;
            }
        }

        public Emprestimo ObterPorId(int id)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Emprestimos.Find(id);
            }
        }
        public int CountEmprestimos()
        {
            using (var context = new BibliotecaContext())
            {
                return context.Emprestimos.Count();
            }
        }
    }
}