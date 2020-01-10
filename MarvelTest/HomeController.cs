using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Web.Mvc;
using MarvelTest.Model;
using System.Collections.Generic;

namespace MarvelTest.Source
{
    public class HomeController : Controller
    {
        public dynamic Index(int request)
        {


            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                string ts = DateTime.Now.Ticks.ToString();
                string publicKey = "e03c763129548d360f04c21e913dee0b";
                string hash = GerarHash(ts, publicKey,
                   "321571b0e166adfd22001ea67bc60162af30fffd");

                if (request == 1)
                {
                    Personagem personagem;
                    List<Personagem> personagens = new List<Personagem>();

                    HttpResponseMessage response = client.GetAsync(
                    "http://gateway.marvel.com/v1/public/" +
                    $"comics/43501/characters?ts={ts}&apikey={publicKey}&hash={hash}&").Result;

                    response.EnsureSuccessStatusCode();
                    string conteudo =
                        response.Content.ReadAsStringAsync().Result;

                    dynamic resultado = JsonConvert.DeserializeObject(conteudo);

                    foreach (var item in resultado.data.results)
                    {
                        personagem = new Personagem();
                        personagem.Nome = item.name;
                        personagem.Descricao = item.description != "" ? item.description : "This char don't have a description";
                        personagem.UrlImagem = item.thumbnail.path + "." +
                            item.thumbnail.extension;
                        personagem.UrlWiki = item.urls[1].url;
                        personagens.Add(personagem);
                    }
                    return personagens;
                }
                else
                {
                    Comic comic = new Comic();

                    HttpResponseMessage response = client.GetAsync(
                    "http://gateway.marvel.com/v1/public/" +
                    $"comics/43501?ts={ts}&apikey={publicKey}&hash={hash}&").Result;

                    response.EnsureSuccessStatusCode();
                    string conteudo =
                        response.Content.ReadAsStringAsync().Result;

                    dynamic resultado = JsonConvert.DeserializeObject(conteudo);

                    comic.Titulo = resultado.data.results[0].title;
                    comic.Descricao = resultado.data.results[0].description;
                    comic.UrlImagem = resultado.data.results[0].images[0].path + "." + resultado.data.results[0].images[0].extension;
                    comic.CopyRights = resultado.copyright;
                    comic.Attribution = resultado.attributionText;
                    return comic;

                }
            }

        }

        private string GerarHash(
            string ts, string publicKey, string privateKey)
        {
            byte[] bytes =
                Encoding.UTF8.GetBytes(ts + privateKey + publicKey);
            var gerador = MD5.Create();
            byte[] bytesHash = gerador.ComputeHash(bytes);
            return BitConverter.ToString(bytesHash)
                .ToLower().Replace("-", String.Empty);
        }
    }
}