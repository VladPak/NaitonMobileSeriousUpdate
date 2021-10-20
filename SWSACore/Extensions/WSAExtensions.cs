using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace SimpleWSA.Extensions
{
  public static class WSAExtensions
  {
    public static WebRequest InitializeWebRequest(this WebRequest webRequest, CompressionType outgoingCompressionType, byte[] postData, WebProxy webProxy)
    {
      webRequest.ContentType = outgoingCompressionType.SetWebRequestContentType();
      webRequest.Method = HttpMethod.POST.ToString();
      webRequest.ContentLength = postData.Length;
      webRequest.Proxy = webProxy;

      using (Stream postStream = webRequest.GetRequestStream())
      {
        postStream.Write(postData, 0, postData.Length);
        postStream.Close();
      }

      return webRequest;
    }

    public static string SparseString(this string input)
    {
      char[] validXmlChars = input.Where(ch => XmlConvert.IsXmlChar(ch)).ToArray();

      return new string(validXmlChars);
    }

       
    private static string SetWebRequestContentType(this CompressionType compressionType)
    {
      return compressionType == CompressionType.NONE ? "text/xml" : "application/octet-stream";
    }
  }
}
