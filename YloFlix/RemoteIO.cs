using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace YloFlix
{
    public class RemoteIO
    {
        private string RemoteUrl { get; set; }

        private CookieContainer cookieContainer { get; set; }

        public RemoteIO(string remoteUrl)
        {
            this.RemoteUrl = remoteUrl;
            this.cookieContainer = new CookieContainer();
        }

        public string Cache()
        {
            using (WebClient client = new WebClient())
            {
                string tmpFile = Path.GetTempFileName();
                client.DownloadFile(this.RemoteUrl, tmpFile);
                return tmpFile;
            }
        }

        public void Login()
        {
            Uri url = new Uri("http://www.addic7ed.com/dologin.php");
            HttpWebRequest request = null;
            request = (HttpWebRequest)WebRequest.Create(url);
            request.CookieContainer = cookieContainer;
            request.Method = "GET";
            HttpStatusCode responseStatus;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                responseStatus = response.StatusCode;
                url = request.Address;
            }

            if (responseStatus == HttpStatusCode.OK)
            {
                UriBuilder urlBuilder = new UriBuilder(url);

                request = (HttpWebRequest)WebRequest.Create(urlBuilder.ToString());
                request.Referer = url.ToString();
                request.CookieContainer = cookieContainer;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:62.0) Gecko/20100101 Firefox/62.0";

                using (Stream requestStream = request.GetRequestStream())
                using (StreamWriter requestWriter = new StreamWriter(requestStream, Encoding.ASCII))
                {
                    string postData = "username=" + Config.AddictedLogin + "&password=" + Config.AddictedPassword + "&submit=Log+in";
                    requestWriter.Write(postData);
                }

                string responseContent = null;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                using (StreamReader responseReader = new StreamReader(responseStream))
                {
                    responseContent = responseReader.ReadToEnd();
                }

                Console.WriteLine(responseContent);
            }
            else
            {
                Console.WriteLine("Client was unable to connect!");
            }
        }

    }
}
