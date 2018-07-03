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

        private CookieContainer CookieContainer { get; set; }

        public RemoteIO(string remoteUrl)
        {
            this.RemoteUrl = remoteUrl;
            this.CookieContainer = new CookieContainer();
            this.Login();
        }

        public string Cache()
        {
            return this.Download(this.RemoteUrl);
        }

        public string Download(string downloadLink)
        {
            string tmpFile = Path.GetTempFileName();
            Uri url = new Uri(downloadLink);
            HttpWebRequest request = null;
            request = (HttpWebRequest)WebRequest.Create(url);
            request.CookieContainer = CookieContainer;
            request.Method = "GET";
            HttpStatusCode responseStatus;
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    responseStatus = response.StatusCode;
                    url = request.Address;
                    if (responseStatus == HttpStatusCode.OK)
                    {
                        string responseContent = null;
                        using (Stream responseStream = response.GetResponseStream())
                        using (StreamReader responseReader = new StreamReader(responseStream))
                        {
                            responseContent = responseReader.ReadToEnd();
                            using (StreamWriter tmpWriter = new StreamWriter(File.Open(tmpFile, FileMode.Append)))
                            {
                                tmpWriter.WriteLine(responseContent);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Utils.Log(e.Message);
                return null;
            }
            return tmpFile;
        }

        public void Login()
        {
            Uri url = new Uri("http://www.addic7ed.com/dologin.php");
            HttpWebRequest request = null;
            request = (HttpWebRequest)WebRequest.Create(url);
            request.CookieContainer = CookieContainer;
            request.Method = "GET";
            HttpStatusCode responseStatus;
            try
            {
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
                    request.CookieContainer = CookieContainer;
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
            catch (Exception e)
            {
                Utils.Log(e.Message);
            }
        }
    }
}
