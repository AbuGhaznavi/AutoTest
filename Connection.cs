using System;
using System.Text;
using System.Net.Http;
using System.Net;
using System.IO;

namespace AutoTest
{
    class Connection
    {
        private static readonly HttpClient client = new HttpClient();
        CookieContainer container = new CookieContainer();
        String ip = "192.168.1.3";
        public int idx = 1;

        //When a new connection is created set the validation to accept all certificates because Accedian uses bad certificates
        public Connection()
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback += new System.Net.Security.RemoteCertificateValidationCallback(ValidateCertificate);
        }

        //Accept all certificates
        static bool ValidateCertificate(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors errors)
        {
            return true;
        }

        //If there is a cookie saved then it is connected.
        public bool connected()
        {
            return container.Count >= 1;
        }

        //Login the the switch at "address" with the "username" and "password"
        public string login(String address, String user, String pass)
        {
            ip = address;
            // Create a request using a URL that can receive a post. 
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://" + ip + "/login");
            // Set the Method property of the request to POST.
            request.Method = "POST";
            request.Timeout = 5000;

            container = new CookieContainer();

            request.CookieContainer = container;

            // Create POST data and convert it to a byte array.  Modify this line accordingly
            string postData = String.Format("loginname={0}&password={1}&ref=", user, pass);

            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded";
            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;
            // Get the request stream.
            try
            {
                Stream dataStream = request.GetRequestStream();
                // Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length);
                // Close the Stream object.
                dataStream.Close();
                // Get the response.
                using (WebResponse response = request.GetResponse())
                {
                    // Get the stream containing content returned by the server.
                    dataStream = response.GetResponseStream();
                    // Open the stream using a StreamReader for easy access.
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content.
                    string responseFromServer = reader.ReadToEnd();

                    // Clean up the streams.
                    reader.Close();
                    dataStream.Close();
                    response.Close();

                    return responseFromServer;
                }
            }
            catch (WebException e)
            {
                return e.Status + "\n" + e.Message;
            }
        }

        //Get the list of parts to show the user
        public string getList()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://" + ip + "/page1_3.asp");
            request.CookieContainer = container;
            request.Timeout = 5000;
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    Stream dataStream = response.GetResponseStream();
                    // Open the stream using a StreamReader for easy access.
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content.
                    String responseFromServer = reader.ReadToEnd();

                    // Clean up the streams.
                    reader.Close();
                    dataStream.Close();
                    response.Close();

                    return responseFromServer;
                }
            }
            catch (WebException e)
            {
                return e.Status + "\n" + e.Message;
            }
        }

        //Get the html for the first port
        public string getPort1()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://" + ip + "/portsfp?idx=" + idx.ToString());
            request.CookieContainer = container;
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            String responseFromServer = reader.ReadToEnd();

            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }

        //Get the html for the second port
        public string getPort2()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://" + ip + "/portsfp?idx=" + (idx + 1).ToString());
            request.CookieContainer = container;
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            String responseFromServer = reader.ReadToEnd();

            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }

        //Get the html for the third port
        public string getPort3()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://" + ip + "/portsfp?idx=" + (idx + 2).ToString());
            request.CookieContainer = container;
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            String responseFromServer = reader.ReadToEnd();

            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }

        //Get the html for the fourth port
        public string getPort4()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://" + ip + "/portsfp?idx=" + (idx + 3).ToString());
            request.CookieContainer = container;
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            String responseFromServer = reader.ReadToEnd();

            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }

        public string getPort5()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://" + ip + "/portsfp?idx=" + (idx + 4).ToString());
            request.CookieContainer = container;
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            String responseFromServer = reader.ReadToEnd();

            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }

        public string getPort6()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://" + ip + "/portsfp?idx=" + (idx + 5).ToString());
            request.CookieContainer = container;
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            String responseFromServer = reader.ReadToEnd();

            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }

        public string getPort7()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://" + ip + "/portsfp?idx=" + (idx + 6).ToString());
            request.CookieContainer = container;
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            String responseFromServer = reader.ReadToEnd();

            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }

        public string getPort8()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://" + ip + "/portsfp?idx=" + (idx + 7).ToString());
            request.CookieContainer = container;
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            String responseFromServer = reader.ReadToEnd();

            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }

        public string getPort9()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://" + ip + "/portsfp?idx=" + (idx + 8).ToString());
            request.CookieContainer = container;
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            String responseFromServer = reader.ReadToEnd();

            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }

        public string getPort10()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://" + ip + "/portsfp?idx=" + (idx + 9).ToString());
            request.CookieContainer = container;
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            String responseFromServer = reader.ReadToEnd();

            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }

        public string getPort11()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://" + ip + "/portsfp?idx=" + (idx + 10).ToString());
            request.CookieContainer = container;
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            String responseFromServer = reader.ReadToEnd();

            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }

        public string getPort12()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://" + ip + "/portsfp?idx=" + (idx + 11).ToString());
            request.CookieContainer = container;
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            String responseFromServer = reader.ReadToEnd();

            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }
    }
}
