namespace AssetPackage
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interface for web service response.
    /// </summary>
    ///
    /// <remarks>
    /// Implemented by assets requesting result notification of a
    /// IWebServiceRequest.
    /// </remarks>
    public interface IWebServiceResponse
    {
        /// <summary>
        /// Called when a WebRequest results in an Error.
        /// </summary>
        ///
        /// <param name="url"> URL of the document. </param>
        /// <param name="msg"> The error message. </param>
        void Error(string url, string msg);

        /// <summary>
        /// Called after a Successfull WebRequest (no Exceptions).
        /// </summary>
        ///
        /// <param name="url">     URL of the document. </param>
        /// <param name="code">    The code. </param>
        /// <param name="headers"> The headers. </param>
        /// <param name="body">    The body. </param>
        void Success(string url, int code, Dictionary<string, string> headers, string body);
    }

    /// <summary>
    /// Interface for web service request.
    /// </summary>
    ///
    /// <remarks>
    /// Implemented on a Bridge.
    /// </remarks>
    public interface IWebServiceRequest
    {

#warning Add Tag or Data parameter to this call so we can identify it in IWebServiceResponse?

        /// <summary>
        /// Web service request.
        /// </summary>
        ///
        /// <param name="method">      The method. </param>
        /// <param name="uri">         URI of the document. </param>
        /// <param name="headers">     The headers. </param>
        /// <param name="body">        The body. </param>
        /// <param name="response">    The response. </param>
        void WebServiceRequest(
            string method,
            Uri uri,
            Dictionary<string, string> headers,
            string body,
            IWebServiceResponse response
            );
    }
}
