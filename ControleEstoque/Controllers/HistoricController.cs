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

        private readonly ControleContexto _context;

        public HistoricController(ControleContexto context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Historico()
        {
   

            IEnumerable<Historico> historicoQuery =
            from hist in _context.Historicos
            select hist;


            ViewBag.ListarHist = historicoQuery;

            return View();
        }


    }
}
