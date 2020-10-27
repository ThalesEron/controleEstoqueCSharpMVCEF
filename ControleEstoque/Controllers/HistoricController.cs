using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControleEstoque.Controllers
{
    public class HistoricController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Historico()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ControleContexto>();
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-PJMFVJI\\SQLEXPRESS;Database=cms2;Trusted_Connection=True;");

            var context = new ControleContexto(optionsBuilder.Options);



            IEnumerable<Historico> historicoQuery =
    from hist in context.Historicos
    select hist;


            ViewBag.ListarHist = historicoQuery;

            return View();
        }


    }
}
