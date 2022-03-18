using Microsoft.AspNetCore.Mvc;
using SeriesPersonajesClienteAPB.Models;
using SeriesPersonajesClienteAPB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeriesPersonajesClienteAPB.Controllers {
    public class ServidorController : Controller {
        private ServiceApiSeriesPersonajes service;
        public ServidorController(ServiceApiSeriesPersonajes service) {
            this.service = service;
        }
        public async Task<IActionResult> MostrarSeries() {
            return View(await this.service.GetSeriesAsync());
        }
        [HttpGet]
        public async Task<IActionResult> ModificarSerie(int id) {
            return View(await this.service.FindSerieAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> ModificarSerie(Serie serie) {
            await this.service.ModificarSerieAsync(serie.IdSerie, serie.Nombre, serie.Imagen, serie.Puntuacion, serie.Año);
            return RedirectToAction("DetallesSerie", new { id = serie.IdSerie });
        }
        public async Task<IActionResult> DetallesSerie(int id) {
            return View(await this.service.FindSerieAsync(id));
        }
        public async Task<IActionResult> VerPersonajesSerie(int idserie) {
            Serie serie = await this.service.FindSerieAsync(idserie);
            ViewData["SERIE"] = serie.Nombre;
            return View(await this.service.GetPersonajesBySerieAsync(idserie));
        }
        public async Task<IActionResult> EliminarPersonaje(int id) {
            Personaje personaje = await this.service.FindPersonajeAsync(id);
            await this.service.DeletePersonajeAsync(id);
            return RedirectToAction("VerPersonajesSerie", new { idserie = personaje.IdSerie });
        }
        [HttpGet]
        public async Task<IActionResult> InsertarPersonaje() {
            ViewData["SERIES"] = await this.service.GetSeriesAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> InsertarPersonaje(Personaje personaje) {
            await this.service.CreatePersonajeAsync(personaje.Nombre, personaje.Imagen, personaje.IdSerie);
            return RedirectToAction("VerPersonajesSerie", new { idserie = personaje.IdSerie });
        }
        [HttpGet]
        public async Task<IActionResult> CambiarSerie(int id) {
            ViewData["SERIES"] = await this.service.GetSeriesAsync();
            return View(await this.service.FindPersonajeAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> CambiarSerie(int idpersonaje, int idserie) {
            await this.service.ModificarPersonajeSerieAsync(idpersonaje, idserie);
            return RedirectToAction("VerPersonajesSerie", new { idserie = idserie });
        }
    }
}
