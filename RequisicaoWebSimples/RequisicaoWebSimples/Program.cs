using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
            }
        }
    }
}
