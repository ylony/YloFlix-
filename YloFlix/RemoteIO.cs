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

        public RemoteIO(string remoteUrl)
        {
            this.RemoteUrl = remoteUrl;
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
    }
}
