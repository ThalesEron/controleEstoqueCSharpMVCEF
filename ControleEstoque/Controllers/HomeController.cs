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

namespace ControleEstoque.Controllers
{

    public class HomeController : Controller
    {



        private readonly ILogger<HomeController> _logger;



        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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

            var optionsBuilder = new DbContextOptionsBuilder<ControleContexto>();
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-PJMFVJI\\SQLEXPRESS;Database=cms2;Trusted_Connection=True;");
            var context = new ControleContexto(optionsBuilder.Options);

            IEnumerable<Produto> produtoQuery =
   from prod in context.Produtos
   select prod;


            ViewBag.Listar = produtoQuery;

            return View();
        }


        public IActionResult Listar()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ControleContexto>();
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-PJMFVJI\\SQLEXPRESS;Database=cms2;Trusted_Connection=True;");

            var context = new ControleContexto(optionsBuilder.Options);



            IEnumerable<Produto> produtoQuery =
    from prod in context.Produtos
    select prod;


            ViewBag.Listar = produtoQuery;

            return View();
        }


        // GET: Produto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var optionsBuilder = new DbContextOptionsBuilder<ControleContexto>();
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-PJMFVJI\\SQLEXPRESS;Database=cms2;Trusted_Connection=True;");

            var context = new ControleContexto(optionsBuilder.Options);

            var produto = await context.Produtos.FindAsync(id);
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

            var optionsBuilder = new DbContextOptionsBuilder<ControleContexto>();
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-PJMFVJI\\SQLEXPRESS;Database=cms2;Trusted_Connection=True;");

            var context = new ControleContexto(optionsBuilder.Options);

            var produto = await context.Produtos.FindAsync(id);

            
           
            context.Historicos.Add(new Historico() { Nome = produto.Nome, Quantidade = produto.Quantidade, Funcao = "Deletado", Data = DateTime.Now });
            context.Produtos.Remove(produto);
            context.SaveChanges();

            ViewBag.Message = "Produto excluido com sucesso.";

            return View();
        }


        [HttpPost]
        public IActionResult Cadastro(String nome, int quantidade)
        {

            var optionsBuilder = new DbContextOptionsBuilder<ControleContexto>();
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-PJMFVJI\\SQLEXPRESS;Database=cms2;Trusted_Connection=True;");

            var context = new ControleContexto(optionsBuilder.Options);


            var produt = context.Produtos
                       .FirstOrDefault(b => b.Nome == nome);





            if (produt?.Id > 0)
            {

                ViewBag.Message = "Produto já registrado.";

            }
            else
            {
                context.Produtos.Add(new Produto() { Nome = nome, Quantidade = quantidade });
                context.SaveChanges();
                ViewBag.Message = "Produto cadastro com sucesso.";
            }



            return View();
        }

        [HttpPost]
        public IActionResult Editar(string nome, int quantidade, string funcao)
        {


                var optionsBuilder = new DbContextOptionsBuilder<ControleContexto>();
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-PJMFVJI\\SQLEXPRESS;Database=cms2;Trusted_Connection=True;");

                var context = new ControleContexto(optionsBuilder.Options);


                var produt = context.Produtos
                           .FirstOrDefault(b => b.Nome == nome);


                if (produt?.Id > 0)
                {
                    

                    var produtoEdit = (from p in context.Produtos
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
                    context.Produtos.Remove(produt);
                   
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
                   


                context.Historicos.Add(new Historico() { Nome = produtoEdit.Nome, Quantidade = quantidade, Funcao = funcao, Data = DateTime.Now });
                context.SaveChanges();

             

                }
                else
                {

                    ViewBag.Message = "O produto nao existe no banco de dados.";
                }

            IEnumerable<Produto> produtoQuery =
                from prod in context.Produtos
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
