using System;

namespace FacebookBatchRequestLib
{
    public enum RequestMethod
    {
        GET = 1,
        POST = 2
    }

    class FacebookUrl
    {
        private string _Method;
        private string _Url;

        public FacebookUrl(RequestMethod Method, string Url)
        {
            _Method = TranslateRequestMethod((short)Method);
            _Url = Url;
        }

        private string TranslateRequestMethod(short RequestMethod)
        {
            string RetRequestMethod = "";

              switch(RequestMethod)       
              {         
                 case 1:   
                    RetRequestMethod = "get";
                    break;                  
                 case 2:            
                    RetRequestMethod = "post";
                    break;                 
                 default:            
                    RetRequestMethod = "get";
                    break;
               }

              return RetRequestMethod;
        }

        public string Method
        {
            get { return _Method; }
        }

        public string Url
        {
            get { return _Url; }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is FacebookUrl))
            {
                return false;
            }
            FacebookUrl FacebookUrlObj = (FacebookUrl)obj;
            return ((FacebookUrlObj.Method == this.Method) && (FacebookUrlObj.Url == this.Url));
        }
    }
}
