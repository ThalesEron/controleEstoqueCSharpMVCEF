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

        public IActionResult Listar()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ControleContexto>();
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-PJMFVJI\\SQLEXPRESS;Database=cms2;Trusted_Connection=True;");

            var context = new ControleContexto(optionsBuilder.Options);

            //         var produtoExist = context.Produtos
            //.FromSqlRaw("SELECT * FROM dbo.Produto WHERE Nome= 'Biscoito'").FirstOrDefault();


            //         Debug.WriteLine("Testando {0}", produtoExist.Quantidade);

            //context.Produtos.Find();

            
           


           var produt = context.Produtos
                       .FirstOrDefault(b => b.Nome == "Biscoito");

            Debug.WriteLine("Testando {0}", produt.Quantidade);


            IEnumerable<Produto> produtoQuery =
    from prod in context.Produtos
    select prod;


            ViewBag.Listar = produtoQuery;

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

            } else
            {
                context.Produtos.Add(new Produto() { Nome = nome, Quantidade = quantidade });
                context.SaveChanges();
                ViewBag.Message = "Produto cadastro com sucesso.";
            }

               

            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
