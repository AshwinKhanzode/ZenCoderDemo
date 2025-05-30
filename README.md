"# ZenCoderDemo" 
* Add App_Data for local ASP.NET database.

When running locally, ASP.NET Identity expects to create the LocalDb databas
e under an App_Data folder.
Ensure the App_Data directory exists to avoid SQL errors.

## Google reCAPTCHA

Login now requires completing a Google reCAPTCHA check. Configure the following
keys in `Web.config` or via transformation per environment:

```
<add key="RecaptchaSiteKey" value="YOUR_SITE_KEY" />
<add key="RecaptchaSecretKey" value="YOUR_SECRET_KEY" />
```

For the debug environment the keys can be set in `Web.Debug.config` using the
`appSettings` section.
