using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EYAppleWatchPOC.Core.Services
{
    public interface IRestService
    {
        void MakeRequest<T>(string requestUrl, string verb, Action<T> successAction, Action<Exception> errorAction);
    }

    public class RestService
        : IRestService
    {
        private readonly IMvxJsonConverter _jsonConverter;



        public RestService()//IMvxJsonConverter jsonConverter)
        {
            //_jsonConverter = jsonConverter;
        }

        public void MakeRequest<T>(string requestUrl, string verb, Action<T> successAction, Action<Exception> errorAction)
        {
            var request = (HttpWebRequest)WebRequest.Create(requestUrl);
            request.Method = verb;
            request.Accept = "application/json";

            MakeRequest(
               request,
               (response) =>
               {
                   if (successAction != null)
                   {
                       T toReturn;
                       try
                       {
                           toReturn = Deserialize<T>(response);
                       }
                       catch (Exception ex)
                       {
                           errorAction(ex);
                           return;
                       }

                       successAction(toReturn);
                   }
               },
               (error) =>
               {
                   if (errorAction != null)
                   {
                       errorAction(error);
                   }
               }
            );
        }



        private void MakeRequest(HttpWebRequest request, Action<string> successAction, Action<Exception> errorAction)
        {
            request.BeginGetResponse(token =>
            {
                try
                {

                    using (var response = request.EndGetResponse(token))
                    {
                        using (var stream = response.GetResponseStream())
                        {
                            using (var reader = new StreamReader(stream))
                            {
                                response.Dispose();
                                request = null;
                                successAction(reader.ReadToEnd());
                            }
                        }
                    }
                }
                catch (WebException ex)
                {
                    Mvx.Error("ERROR: '{0}' when making {1} request to {2}", ex.Message, request.Method, request.RequestUri.AbsoluteUri);
                    errorAction(ex);
                }
            }, request);
        }

        public byte[] FileToSend;

        public  void PostRequest(string url, WebHeaderCollection headers, Action<string> successAction, Action<Exception> errorAction)
        {
			try
			{
			HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			myHttpWebRequest.Method = "POST";
			myHttpWebRequest.Headers = headers;
			myHttpWebRequest.ContentType = "application//x-www-form-urlencoded";

			// myHttpWebRequest.BeginGetRequestStream(new AsyncCallback(GetRequestStreamCallback), myHttpWebRequest);

			myHttpWebRequest.BeginGetRequestStream(token =>
				{                   


					// End the operation
					Stream postStream = myHttpWebRequest.EndGetRequestStream(token);


					// Convert the string into a byte array. 
					byte[] byteArray = System.Text.Encoding.UTF8.GetBytes ("post data");

					// Write to the request stream.
					postStream.Write(byteArray, 0, byteArray.Length);
					// postStream.Close();

					// Start the asynchronous operation to get the response
					IAsyncResult result = (IAsyncResult)myHttpWebRequest.BeginGetResponse(new AsyncCallback(delegate(IAsyncResult tempResult)
						{
							HttpWebResponse webResponse = (HttpWebResponse)myHttpWebRequest.EndGetResponse(tempResult);
							Stream responseStream = webResponse.GetResponseStream();

							using (StreamReader reader = new StreamReader(responseStream, Encoding.UTF8))
							{

								successAction(reader.ReadToEnd());

							}
						}), null);
				}, myHttpWebRequest);
		}
			catch(Exception ex)
            {
				
            }
        }

        private void GetRequestStreamCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;


            // End the operation
            Stream postStream = request.EndGetRequestStream(asynchronousResult);


            // Convert the string into a byte array. 
            byte[] byteArray = FileToSend;

            // Write to the request stream.
            postStream.Write(byteArray, 0, FileToSend.Length);
            // postStream.Close();

            // Start the asynchronous operation to get the response
            IAsyncResult result = (IAsyncResult)request.BeginGetResponse(new AsyncCallback(delegate(IAsyncResult tempResult)
            {
                HttpWebResponse webResponse = (HttpWebResponse)request.EndGetResponse(tempResult);
                Stream responseStream = webResponse.GetResponseStream();

                using (StreamReader reader = new StreamReader(responseStream, Encoding.UTF8))
                {
                    string response = reader.ReadToEnd();

                }
            }), null);
        }

        private T Deserialize<T>(string responseBody)
        {
            var toReturn = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseBody);
            return toReturn;
        }
    }

}
