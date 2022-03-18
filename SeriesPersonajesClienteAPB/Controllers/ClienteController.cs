using Microsoft.AspNetCore.Mvc;
using SeriesPersonajesClienteAPB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeriesPersonajesClienteAPB.Controllers {
    public class ClienteController : Controller {
        private ServiceApiSeriesPersonajes service;
        public ClienteController(ServiceApiSeriesPersonajes service) {
            this.service = service;
        }
        public IActionResult MostrarSeries() {
            return View();
        }
        public IActionResult VerPersonajesSerie() {
            return View();
        }
        public IActionResult InsertarSerie() {
            return View();
        }
    }
}
