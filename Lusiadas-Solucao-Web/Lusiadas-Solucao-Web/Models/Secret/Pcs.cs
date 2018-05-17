using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LusiadasSolucaoWeb.Models.Secret
{
    public class Pcs
    {

        private string ip;
        private DateTime data;
        private string browser;
        private string url;

        public Pcs()
        {
            ip = "";
            data = DateTime.Now;
            browser = "";
            url = "";

        }

        public Pcs(string _ip,string _browser,string _url)
        {
            ip = _ip;
            data = DateTime.Now;
            browser = _browser;
            url = _url;

        }

        public string getBrowser()
        {

            return browser;

        }

        public string getIp()
        {

            return ip;

        }

        public string getUrl()
        {

            return url;

        }

        public DateTime getData()
        {

            return data;

        }

        public void setBrowser(string _browser)
        {

            browser = _browser;

        }

        public void setIp(string _ip)
        {

            ip = _ip;

        }

        public void setData(DateTime _data)
        {

            data = _data;

        }

        public void setUrl(String _url)
        {

            url = _url;

        }




    }
}