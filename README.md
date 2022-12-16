# AuthenticationAndAuthorizationWithASPDotNetCore
This Solution contains different projects depicting the use of various Authentication and Authorization techniques in ASP.Net Core.
## AAWithAzureAD
    Steps :
         1. Install Nuget Package Microsoft.Identity.Web.
         2. Add in ConfigureServices in startup.

            // Code snippet
             services.AddMicrosoftIdentityWebApiAuthentication(Configuration);

        3. Configure Middleware: 

            app.UseAuthentication();

            // Configure middleware to authenticate user before accessing the request.
            app.Use(async (context, next) =>
            {
                if (!context.User.Identity?.IsAuthenticated ?? false)
                {
                    context.Response.StatusCode = 401;
                    context.Response.WriteAsync("Invalid User !");
                }
                else
                {
                    await next();
                }
            });

        4. Update appsettings.json :

            "AzureAd": {
              "Instance": "https://login.microsoftonline.com/",
              "ClientId": "",
              "TenantId": "",
              "Audience": ""
            }

        5. Create AppRegistration in Azure portal and fill the details in appsettings.json accordingly.

        6. Use link : https://docs.microsoft.com/en-us/azure/active-directory/develop/authentication-flows-app-scenarios to create appropriate request based on application type for authentication.

        Request format Used in the project :

         https://login.microsoftonline.com/2278cd6f-c0d2-4a85-8cb9-c36ba45e8bab/oauth2/authorize?client_id=
         &response_type=token
         &redirect_uri=
         &resource=
         &response_mode=fragment
         &state=12345
         &nonce=678910

        7. Put that request in browser to provide consent for the sign in using active directory. you will get a response url contain access token.
    
        Response : 
            https://localhost:44336/#access_token=eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6Imwzc1EtNTBjQ0g0eEJWWkxIVEd3blNSNzY4MCIsImtpZCI6Imwzc1EtNTBjQ0g0eEJWWkxIVEd3blNSNzY4MCJ9.eyJhdWQiOiJkYjM4YWFlYS0yMzY1LTQwMjgtYmQ5Ni02YjUzODhhNWEwYjIiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC8yMjc4Y2Q2Zi1jMGQyLTRhODUtOGNiOS1jMzZiYTQ1ZThiYWIvIiwiaWF0IjoxNjM2Mjk0OTU3LCJuYmYiOjE2MzYyOTQ5NTcsImV4cCI6MTYzNjMwMDA4MywiYWNyIjoiMSIsImFpbyI6IkFYUUFpLzhUQUFBQWVOQTdjMGd3cHRBTkkrWGd6OHBGYWRIelFUdVBTWWNHNWdnQ2N3K1BiYkluZWhrbFhwU3ZXNmdvVm4vR0J4VjFncGlJZk5jd1pncWNNdzNnNTFhMGJlSGpmY0dCeG1UdEpqSU1nU04vSXljRG84SFpzYmc5amo3U1VlWUNidzFINkhEMktCQitrUDkzWEc1TytQYSttZz09IiwiYW1yIjpbInB3ZCIsIm1mYSJdLCJhcHBpZCI6ImRiMzhhYWVhLTIzNjUtNDAyOC1iZDk2LTZiNTM4OGE1YTBiMiIsImFwcGlkYWNyIjoiMCIsImVtYWlsIjoiZGV2dmJyYXQxMTA3QGdtYWlsLmNvbSIsImZhbWlseV9uYW1lIjoiU2luZ2giLCJnaXZlbl9uYW1lIjoiRGV2YnJhdCIsImlkcCI6ImxpdmUuY29tIiwiaXBhZGRyIjoiMTE3LjI0OC4yNTMuMjUzIiwibmFtZSI6IkRldmJyYXQgU2luZ2giLCJvaWQiOiJkZWMzMDRhZC0xOGE5LTQ0M2MtYjA3NC05YjFhMmViYjNjMjciLCJyaCI6IjAuQVhFQWI4MTRJdExBaFVxTXVjTnJwRjZMcS1xcU9OdGxJeWhBdlpaclU0aWxvTEp4QUlFLiIsInNjcCI6IlVzZXIuUmVhZCIsInN1YiI6InE2RzU3d3dwcGxnOWZVLUR0VEJTbzNoZENXMHNKWlhjOHNuZHBZQ3BuOEkiLCJ0aWQiOiIyMjc4Y2Q2Zi1jMGQyLTRhODUtOGNiOS1jMzZiYTQ1ZThiYWIiLCJ1bmlxdWVfbmFtZSI6ImxpdmUuY29tI2RldnZicmF0MTEwN0BnbWFpbC5jb20iLCJ1dGkiOiJMWGhHWnU1ZXpFRzNIdV90VEVKd0FBIiwidmVyIjoiMS4wIn0.jDk9S6C_BmmSvVdZJ9htznjkCr2HFiueE9b-bkDy6PmfzvcWF8y6FXdxTbTUEd0myRjQP9_M1ho4RIME64HznDAmOop8JDB32sbQKvrkM8gSo377vEuLbPXIBGJBH1vV7k2OJynkcpcR8UfsB7nogvrDvHyJ7Dfwc44r6LliRyMQMD-nj_WFE_RMT3osN9YLU7l-y7HJNMLN2KlRYslA87fyvTBnL_eoaZ8TUHyAL2u_LTGtEgA462yHnVvpe5S7JPdpOPJ_sR7lKUYYw8pYQx-r_LpJ3zBJV4oZgevfXEAp2pPWD9JuqvHn1KXFWtBWmnz0p_fSeMmSuffJseYGag
            &token_type=Bearer
            &expires_in=4825
            &state=12345
            &Session_state=a9671316-1f6a-4a96-9f8a-3f19acbb1b6b

        8. Use access token as a bearer token type in authorization header to send the request to API.

        9. You will receive the data requested from API.

        10. Based on the users you want to provide access to the your application you can add as many users on the need basis in azure active directory and provide access to the application by adding user for the application registration in azure portal.

## AuthorizationWithJWT
## CookieBasedAuthentication
## IdentityServerBasedAuthentication
## Role and Policy Based Authentication

