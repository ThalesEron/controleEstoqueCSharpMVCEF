using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ControleEstoque.Models;
using Business;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;

namespace ControleEstoque.Controllers
{

    public class HomeController : Controller
    {


        private readonly ControleContexto _context;

        public HomeController(ControleContexto context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cadastro()
        {

            return View();
        }

        public IActionResult Editar()
        {

        

            IEnumerable<Produto> produtoQuery =
             from prod in _context.Produtos
             select prod;

            

        ViewBag.Listar = produtoQuery;

            return View();
        }


        public async Task<IActionResult> Listar(string searchString)
        {
            var produto = from p in _context.Produtos
                          select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                produto = produto.Where(s => s.Nome.Contains(searchString));
            }

            ViewBag.Listar = produto;

            return View();
        }


        // GET: Produto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

          

            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }


            return View(produto);
        }


        public async Task<IActionResult> Excluir(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

         

            var produto = await _context.Produtos.FindAsync(id);



            _context.Historicos.Add(new Historico() { Nome = produto.Nome, Quantidade = produto.Quantidade, Funcao = "Deletado", Data = DateTime.Now });
            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            ViewBag.Message = "Produto excluido com sucesso.";

            return View();
        }


        [HttpPost]
        public IActionResult Cadastro(String nome, int quantidade)
        {



            var produt = _context.Produtos
                       .FirstOrDefault(b => b.Nome == nome);





            if (produt?.Id > 0)
            {

                ViewBag.Message = "Produto já registrado.";

            }
            else
            {
                _context.Produtos.Add(new Produto() { Nome = nome, Quantidade = quantidade });
                _context.SaveChanges();
                ViewBag.Message = "Produto cadastro com sucesso.";
            }



            return View();
        }

        [HttpPost]
        public IActionResult Editar(string nome, int quantidade, string funcao)
        {


              


                var produt = _context.Produtos
                           .FirstOrDefault(b => b.Nome == nome);


                if (produt?.Id > 0)
                {
                    

                    var produtoEdit = (from p in _context.Produtos
                                       where p.Nome == nome
                                       select p).SingleOrDefault();


                if (funcao == "Acrescentado")
                {

                    produtoEdit.Quantidade += quantidade;


                }
                else if (funcao == "Retirado")
                {

                    int reduzir = produtoEdit.Quantidade - quantidade;

                    if (reduzir < 0)
                    {
                        produtoEdit.Quantidade = 0;
                    }
                    else
                    {
                        produtoEdit.Quantidade -= quantidade;
                    }


                }
                else if (funcao == "Deletado")
                {
                    _context.Produtos.Remove(produt);
                   
                }
                else
                {
                    produtoEdit.Quantidade = quantidade;

                }


                if (funcao == "Deletado")
                {
                    ViewBag.Message = "Produto deletado com sucesso.";
                } else
                {
                    ViewBag.Message = "Produto editado com sucesso. Novo valor: " + produtoEdit.Quantidade;
                }



                _context.Historicos.Add(new Historico() { Nome = produtoEdit.Nome, Quantidade = quantidade, Funcao = funcao, Data = DateTime.Now });
                _context.SaveChanges();

             

                }
                else
                {

                    ViewBag.Message = "O produto nao existe no banco de dados.";
                }

            IEnumerable<Produto> produtoQuery =
                from prod in _context.Produtos
                select prod;


            ViewBag.Listar = produtoQuery;




            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
