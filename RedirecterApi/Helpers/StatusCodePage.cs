using Microsoft.AspNetCore.Mvc;
using System.Drawing;
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
            """
            +
            """
            <style>
            body {
                font-family: Arial, sans-serif;
                background-color: #f0f0f0;
                margin: 0;
                padding: 0;
            }

            .header {
                background-color: #1877f2; /* Facebook blue color */
                color: #fff;
                text-align: center;
                padding: 20px;
            }

            h1 {
                font-size: 36px;
                font-weight: bold;
                font-family: 'Helvetica', sans-serif; /* Adjust font as needed */
            }

            p {
                font-size: 18px;
                font-family: 'Helvetica', sans-serif; /* Adjust font as needed */
            }

            .message {
                font-style: italic;
            }
            </style>
            """
            +  
            $"""
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

        public static ContentResult BadRequest400()
        {
            const int statusCode = 400;
            return new ContentResult
            {
                Content = BasePage("Bad Request", "The made request was invalid.", statusCode),
                ContentType = "text/html",
                StatusCode = statusCode
            };
        }

        public static ContentResult NotFound404()
        {
            const int statusCode = 404;
            return new ContentResult
            {
                Content = BasePage("Resource not found", "We are sorry, but the requested resource could not be found.", statusCode),
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
                Content = BasePage("Temporary redirect", $"If you were not redirected please click here <a href=\"{redirectUrl}\">{redirectUrl}</a>.", statusCode),
                ContentType = "text/html",
                StatusCode = statusCode
            };
        }
    }
}
