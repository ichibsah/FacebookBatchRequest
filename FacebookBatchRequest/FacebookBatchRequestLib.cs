using System;
using System.Web;
using System.Collections.Generic;
using System.Collections;
using System.Net;
using System.Text;

namespace FacebookBatchRequestLib
{
    public class FacebookBatchRequest
    {
        private string _AccessToken;
        private List<FacebookUrl> _AllUrls = new List<FacebookUrl>();

        public FacebookBatchRequest(string AccessToken)
        {
            _AccessToken = AccessToken;
        }

        public bool AddBatchUrl(RequestMethod RequestMethod, string Url)
        {
            if (string.IsNullOrEmpty(Url))
                return false;

            FacebookUrl FBUrlObj = new FacebookUrl(RequestMethod, Url);
            if (_AllUrls.Contains(FBUrlObj) == true)
            {
                return false;
            }

            _AllUrls.Add(FBUrlObj);

            return true;
        }

        public List<string> Send()
        {
            // chop urls into batches of 50 each
            int BatchSize = 50;
            List<string> RetJSONs = new List<string>();
            string RetJSON = "";

            while (_AllUrls.Count >= BatchSize)
            {
                RetJSON = SendBatch(_AccessToken, _AllUrls.GetRange(0, BatchSize));
                _AllUrls.RemoveRange(0, BatchSize);

                if(string.IsNullOrEmpty(RetJSON) == false)
                {
                    RetJSONs.Add(RetJSON);
                }

                RetJSON = "";
            }

            RetJSON = SendBatch(_AccessToken, _AllUrls.GetRange(0, _AllUrls.Count));

            if (string.IsNullOrEmpty(RetJSON) == false)
            {
                RetJSONs.Add(RetJSON);
            }

            return RetJSONs;
        }

        private string SendBatch(string AccessToken, List<FacebookUrl> FacebookUrls)
        {
            string RetJSON = "";

            if (FacebookUrls.Count > 0)
            {
                string JSON_URL = "";
                WebClient FBClient = new WebClient();
                FBClient.Headers.Add("Content-Type", "application/json");
                FBClient.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";

                foreach (FacebookUrl FBUrlObj in FacebookUrls)
                {
                    JSON_URL += "{\"method\":\"" + FBUrlObj.Method + "\",\"relative_url\":\"" + FBUrlObj.Url + "\"},";
                }
                JSON_URL.Trim(',');
                JSON_URL = "[" + JSON_URL + "]";

                string RequestParam = "access_token=" + AccessToken + "&batch=" + HttpUtility.UrlEncode(JSON_URL);

                byte[] RequestParamBytes = Encoding.UTF8.GetBytes(RequestParam);

                try
                {
                    byte[] RetJSONBytes = FBClient.UploadData("https://graph.facebook.com/", "POST", RequestParamBytes);
                    RetJSON = Encoding.UTF8.GetString(RetJSONBytes);
                }
                catch (Exception)
                {
                }
            }

            return RetJSON;
        }
    }
}