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



        [HttpPost]
        public IActionResult Cadastro(String nome, int quantidade)
        {

            var optionsBuilder = new DbContextOptionsBuilder<ControleContexto>();
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-PJMFVJI\\SQLEXPRESS;Database=cms2;Trusted_Connection=True;");


                var context = new ControleContexto(optionsBuilder.Options);
                context.Produtos.Add(new Produto() {Nome = nome, Quantidade = quantidade });
                context.SaveChanges();
  
            //Debug.WriteLine("Testando {0} {1}", nome, quantidade);



            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
