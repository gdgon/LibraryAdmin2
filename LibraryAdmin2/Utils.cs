//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Web;

//namespace LibraryAdmin2
//{
//    public static class Utils
//    {
//        public string SendPost(string url, string data = "")
//        {

//            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

//            // set post headers
//            request.Method = "POST";
//            request.KeepAlive = true;
//            request.ContentLength = data.Length;
//            request.ContentType = "application/x-www-form-urlencoded";

//            // write the data to the request stream         
//            StreamWriter writer = new StreamWriter(request.GetRequestStream());
//            writer.Write(data);

//            // iirc this actually triggers the post
//            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
//        }
//    }
//}