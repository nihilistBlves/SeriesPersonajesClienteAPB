using Newtonsoft.Json;
using SeriesPersonajesClienteAPB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SeriesPersonajesClienteAPB.Services {
    public class ServiceApiSeriesPersonajes {
        private Uri UriApi;
        private MediaTypeWithQualityHeaderValue Header;
        public ServiceApiSeriesPersonajes(string url) {
            this.UriApi = new Uri(url);
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
        }
        public async Task<T> CallApiAsync<T>(string request) {
            using (HttpClient client = new HttpClient()) {
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode) {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else {
                    return default(T);
                }
            }
        }
        public async Task<List<Serie>> GetSeriesAsync() {
            string request = "/api/series";
            List<Serie> series = await this.CallApiAsync<List<Serie>>(request);
            return series;
        }
        public async Task<Serie> FindSerieAsync(int id) {
            string request = "/api/series/" + id;
            Serie serie = await this.CallApiAsync<Serie>(request);
            return serie;
        }
        public async Task<Personaje> FindPersonajeAsync(int id) {
            string request = "/api/personajes/" + id;
            Personaje personaje = await this.CallApiAsync<Personaje>(request);
            return personaje;
        }
        public async Task DeleteSerieAsync(int id) {
            string request = "/api/series/" + id;
            using (HttpClient client = new HttpClient()) {
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response =
                    await client.DeleteAsync(request);
            }
        }
        public async Task<List<Personaje>> GetPersonajesBySerieAsync(int idserie) {
            string request = "/api/Series/PersonajesSerie/" + idserie;
            List<Personaje> personajes = await this.CallApiAsync<List<Personaje>>(request);
            return personajes;
        }
        public async Task CreatePersonajeAsync(string nombre, string imagen, int idserie) {
            using (HttpClient client = new HttpClient()) {
                string request = "/api/personajes";
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Personaje personaje = new Personaje();
                personaje.IdPersonaje = 1;
                personaje.Nombre = nombre;
                personaje.Imagen = imagen;
                personaje.IdSerie = idserie;
                string json = JsonConvert.SerializeObject(personaje);
                StringContent content = new StringContent
                    (json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(request, content);
            }
        }

        public async Task ModificarSerieAsync(int idserie, string nombre, string imagen, double puntuacion, int año) {
            using (HttpClient client = new HttpClient()) {
                string request = "/api/series";
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Serie serie = new Serie();
                serie.IdSerie = idserie;
                serie.Nombre = nombre;
                serie.Imagen = imagen;
                serie.Puntuacion = puntuacion;
                serie.Año = año;
                string json = JsonConvert.SerializeObject(serie);
                StringContent content = new StringContent
                    (json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PutAsync(request, content);
            }
        }
        public async Task DeletePersonajeAsync(int id) {
            string request = "/api/personajes/" + id;
            using (HttpClient client = new HttpClient()) {
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response =
                    await client.DeleteAsync(request);
            }
        }
        public async Task ModificarPersonajeSerieAsync(int idpersonaje, int idserie) {
            using (HttpClient client = new HttpClient()) {
                string request = "/api/Personajes/" + idpersonaje + "/" + idserie;
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                string json = JsonConvert.SerializeObject(new object());
                StringContent content = new StringContent
                    (json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PutAsync(request, content);
            }
        }

    }
}
