using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace RedirecterApi.Helpers
{
    public static class StatusCodePage
    {
        private static string BasePage(string title, string message, int statusCode) => $"""
            <!DOCTYPE html>
            <html lang="en">
            <head
                <meta charset="utf-8" /><meta http-equiv="X-UA-Compatible" content="IE=edge" /><meta name="viewport" content="width=device-width, initial-scale=1" />
                <title>{title}</title>
                <style></style>    
            </head>
            <body>
                <div class="header">
                    <h1>{title}</h1>
                    <p>Status Code: {statusCode}</p>
                    <p class="message">{message}</p>
                </div>
            </body>
            </html>
            """;

        public static ContentResult NotFound404()
        {
            const int statusCode = 404;
            return new ContentResult
            {
                Content = BasePage("Resource not found", "We are sorry, but the requested resource could not be found", statusCode),
                ContentType = "text/html",
                StatusCode = statusCode
            };
        }

        public static ContentResult InternalServerError500()
        {
            const int statusCode = 500;
            return new ContentResult
            {
                Content = BasePage("Internal Server Error", "we are currently experiencing server issues. Please come back later. Sorry for the inconvenience.", statusCode),
                ContentType = "text/html",
                StatusCode = statusCode
            };
        }

        public static ContentResult TemporaryRedirect302(string redirectUrl)
        {
            const int statusCode = 302;
            return new ContentResult
            {
                Content = BasePage("Temporary redirect", $"If you were not redirected please click here <a href=\"{redirectUrl}\">{redirectUrl}</a>", statusCode),
                ContentType = "text/html",
                StatusCode = statusCode
            };
        }
    }
}
