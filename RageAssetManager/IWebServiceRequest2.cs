namespace AssetPackage
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Request Settings.
    /// </summary>
    public class RequestSetttings
    {
        /// <summary>
        /// The method.
        /// </summary>
        public string method;

        /// <summary>
        /// URI of the document.
        /// </summary>
        public Uri uri;

        /// <summary>
        /// The request headers.
        /// </summary>
        public Dictionary<String, String> requestHeaders;

        /// <summary>
        /// The body.
        /// </summary>
        public String body;

        /// <summary>
        /// The allowed responses.
        /// </summary>
        public List<int> allowedResponsCodes;

        /// <summary>
        /// Initializes a new instance of the AssetPackage.requestParameters
        /// class.
        /// </summary>
        public RequestSetttings()
        {
            method = "GET";
            requestHeaders = new Dictionary<String, String>();
            body = String.Empty;
            allowedResponsCodes = new List<int>();
            allowedResponsCodes.Add(200);
        }
    }

    /// <summary>
    /// Response results.
    /// </summary>
    public class RequestResponse : RequestSetttings
    {
        /// <summary>
        /// The response code.
        /// </summary>
        public int responseCode;

        /// <summary>
        /// Message describing the respons.
        /// </summary>
        public string responsMessage;

        /// <summary>
        /// The response headers.
        /// </summary>
        public Dictionary<String, String> responseHeaders;

        /// <summary>
        /// Initializes a new instance of the AssetPackage.RequestResponse class.
        /// </summary>
        public RequestResponse() : base()
        {
            responseCode = 0;
            responsMessage = String.Empty;

            responseHeaders = new Dictionary<String, String>();
        }

        /// <summary>
        /// Initializes a new instance of the AssetPackage.RequestResponse class.
        /// </summary>
        ///
        /// <remarks>
        /// The body is not copied as it will contain thee response body instead.
        /// </remarks>
        ///
        /// <param name="settings"> Options for controlling the operation. </param>
        public RequestResponse(RequestSetttings settings) : this()
        {
            method = settings.method;
            requestHeaders = settings.requestHeaders;
            uri = settings.uri;
            body = String.Empty;

            allowedResponsCodes = settings.allowedResponsCodes;
        }

        /// <summary>
        /// Gets a value indicating whether result is allowed.
        /// </summary>
        ///
        /// <value>
        /// true if result allowed, false if not.
        /// </value>
        public bool ResultAllowed
        {
            get
            {
                return allowedResponsCodes.Contains(responseCode);
            }
        }
    }

    /// <summary>
    /// Interface for web service request.
    /// </summary>
    ///
    /// <remarks>
    /// Implemented on a Bridge.
    /// </remarks>
    public interface IWebServiceRequest2
    {
        /// <summary>
        /// Web service request.
        /// </summary>
        ///
        /// <param name="requestSettings">  Options for controlling the operation. </param>
        ///
        /// <returns>
        /// A RequestResponse.
        /// </returns>
        void WebServiceRequest(RequestSetttings requestSettings, out RequestResponse requestResponse);
    }
}
