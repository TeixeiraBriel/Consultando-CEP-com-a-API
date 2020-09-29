using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace RequisicaoWebSimples
{
    class Program
    {
        public static void EnviaRequisicaoPOST()
        {
            string dadosPOST = "title=macoratti";
            dadosPOST = dadosPOST + "&body=teste de envio de post";
            dadosPOST = dadosPOST + "&userId=1";

            var dados = Encoding.UTF8.GetBytes(dadosPOST);

            var requisicaoWeb = WebRequest.CreateHttp("http://jsonplaceholder.typicode.com/posts");

            requisicaoWeb.Method = "POST";
            requisicaoWeb.ContentType = "application/x-www-form-urlencoded";
            requisicaoWeb.ContentLength = dados.Length;
            requisicaoWeb.UserAgent = "RequisicaoWebDemo";

            using (var stream = requisicaoWeb.GetRequestStream())
            {
                stream.Write(dados, 0, dados.Length);
                stream.Close();
            }

            using (var resposta = requisicaoWeb.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                object objResponse = reader.ReadToEnd();
                var post = JsonConvert.DeserializeObject<Post>(objResponse.ToString());
                Console.WriteLine(post.Id + " " + post.title + " " + post.body);
                streamDados.Close();
                resposta.Close();
            }
        }


        public static void LerRequisicaoPOST()
        {
            int id = 1;
            var requisicaoWeb = WebRequest.CreateHttp("http://jsonplaceholder.typicode.com/posts/" + id);
            requisicaoWeb.Method = "GET";
            requisicaoWeb.UserAgent = "RequisicaoWebDemo";

            using (var resposta = requisicaoWeb.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                object objResponse = reader.ReadToEnd();

                var post = JsonConvert.DeserializeObject<Post>(objResponse.ToString());

                Console.WriteLine(post.Id);
                Console.WriteLine(post.title);
                Console.WriteLine(post.body);
                Console.WriteLine("---------");
                Console.WriteLine(objResponse.ToString());


                streamDados.Close();
                resposta.Close();
            }
        }

        public static void LerRequisicaoPOSTComLink(string link)
        {
            var requisicaoWeb = WebRequest.CreateHttp(link);
            requisicaoWeb.Method = "GET";
            requisicaoWeb.UserAgent = "RequisicaoWebDemo";

            using (var resposta = requisicaoWeb.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                object objResponse = reader.ReadToEnd();

                Console.WriteLine(objResponse.ToString());
                streamDados.Close();
                resposta.Close();
            }
        }
        

        public static void LogandoWowBrasil()
        {
            var client = new RestClient("http://www.wow-brasil.com/index.php?n=account/login&action=login");

            client.Timeout = -1;

            var request = new RestRequest(Method.POST);

            request.AddHeader("Accept", " text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            //request.AddHeader("Host", " www.wow-brasil.com");
            request.AddHeader("Origin", " http://www.wow-brasil.com");
            request.AddHeader("Referer", " http://www.wow-brasil.com/index.php?n=account/login");

            client.UserAgent = " Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.121 Safari/537.36";

            request.AddHeader("Cookie", "PHPSESSID=223143f3f8ed480e204cbe391f2367c4");

            request.AddParameter("login", " xxbielgtxx");
            request.AddParameter("pass", " gatinha123");

            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
        }

        static void Main(string[] args)
        {
            int i = 10;
            Console.WriteLine("0 - Sair || 1 - Ler Requisicão || 2 - Enviar Requisição || 3 - Ler Requisicão com Link");
            while (i != 0)
            {
                i = int.Parse(Console.ReadLine());

                if (i == 1)
                {
                    LerRequisicaoPOST();
                }
                else if (i == 2)
                {
                    EnviaRequisicaoPOST();
                }
                else if (i == 3)
                {
                    LerRequisicaoPOSTComLink("http://viacep.com.br/ws/30660120/json/");
                }
                else if (i == 4)
                {
                    //Fazer Testes com HTMLAgilityPack
                    LerRequisicaoPOSTComLink("http://warcraftlivros.blogspot.com/2016/06/warcraft-ordem-cronologica-livros-hqs.html");
                }
                else if (i == 5)
                {
                    LogandoWowBrasil();
                }
            }
        }
    }
}
