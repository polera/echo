using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Nancy;

namespace echo
{
    public class MainModule : NancyModule
    {

        public MainModule() : base("/")
        {
            var payload = new Dictionary<string, object>();

            Get["/"] = x =>
            {
                // Add payload items to our Dictionary
                payload.Add("url", this.Request.Url.Path);
                payload.Add("protocol", this.Request.Url.Scheme);
                payload.Add("query", this.Request.Url.Query);
                payload.Add("headers", Request.Headers);

                return Response.AsJson(payload);
                
            };

            Post["/"] = x =>
            {
                // Reset Request stream position to 0 so we can read it all
                this.Request.Body.Position = 0;
                var reader = new StreamReader(this.Request.Body);
                var requestData = reader.ReadToEnd();

                // Add payload items to our Dictionary
                payload.Add("url", this.Request.Url.Path);
                payload.Add("protocol", this.Request.Url.Scheme);
                payload.Add("body",requestData);
                payload.Add("headers",Request.Headers);
                
                return Response.AsJson(payload);
            };

        }
    }
}