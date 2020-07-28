using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace TesteElano.Util
{
    public class Common
    {
        public string GetExternalService(string url)
        {
            var result = "";
            string resultLatLong = "";

            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            var requisicaoWeb = WebRequest.CreateHttp(url);
            requisicaoWeb.Method = "GET";
            requisicaoWeb.UserAgent = "RequisicaoWebDemo";

            using (var resposta = requisicaoWeb.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                object objResponse = reader.ReadToEnd();
                var post = JsonConvert.DeserializeObject<dynamic>(objResponse.ToString());
                
                result = post.message.ToString();

                streamDados.Close();
                resposta.Close();
            }

            return string.Join(",", result);
        }
    }
}
