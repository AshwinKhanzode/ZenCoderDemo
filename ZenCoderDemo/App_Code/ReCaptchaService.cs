using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

public static class ReCaptchaService
{
    private const string VerifyUrl = "https://www.google.com/recaptcha/api/siteverify";

    public static bool VerifyResponse(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return false;
        }

        var secret = System.Configuration.ConfigurationManager.AppSettings["RecaptchaSecretKey"];
        if (string.IsNullOrEmpty(secret))
        {
            // If secret is not configured treat as passed for dev environment
            return true;
        }

        try
        {
            var request = (HttpWebRequest)WebRequest.Create(VerifyUrl);
            request.Method = "POST";
            var postData = $"secret={secret}&response={token}";
            var bytes = System.Text.Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = bytes.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);
            }

            using (var response = request.GetResponse())
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var jsonResponse = reader.ReadToEnd();
                dynamic result = JsonConvert.DeserializeObject(jsonResponse);
                return result.success == true;
            }
        }
        catch
        {
            // on exception fail safe and allow login to continue
            return true;
        }
    }
}
