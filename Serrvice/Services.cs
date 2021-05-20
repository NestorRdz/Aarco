using Aarco.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Aarco.Serrvice
{
    public class Services
    {
        private string _URL = "https://apitestcotizamatico.azurewebsites.net/api/catalogos";
        private string _Method = "POST";

        private string GetServices(string body)
        {
            string responseService = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_URL);
                request.Method = _Method;
                request.ContentType = "application/json";
                request.Accept = "application/json";

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(body);
                    streamWriter.Flush();
                    streamWriter.Close();
                }


                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return null;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            responseService = objReader.ReadToEnd();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al consultar servico externo: ==> {ex.Message}");
            }
            return responseService;
        }

        public Catalogo GetCatalogo(RequestCatalogo requestCat)
        {
            Catalogo catalogo;
            string gResponse = null;
            try
            {
                string request = JsonConvert.SerializeObject(requestCat);
                gResponse = GetServices(request);

                catalogo = !string.IsNullOrEmpty(gResponse) ? JsonConvert.DeserializeObject<Catalogo>(gResponse) : null;
                
            }
            catch (Exception e)
            {
                catalogo = null;
                Console.WriteLine($"Error al consultar servico GetCatalogo externo: ==> {e.Message}");
            }
            return catalogo;
        }
    }
}
