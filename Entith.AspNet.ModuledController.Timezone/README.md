This is a simple library to get the timezone offset from the user's browser.

To add the JavaScript block that populates the cookie use:

	@Html.TimeZoneCookieBlock()

If you want to force a reload for first time users so ensure the cookie is populated you can use:+1:

	@Html.TimeZoneCookieBlock(true)


To get the server side part working, you'll need to register `TimezoneControllerModule` with your
dependency injector as an `IControllerModule` so that it gets instantiated and passed to the `ControllerModuleManager`.

In the controller you can use the extension method `GetTimeZoneOffset()` to get the offset. In the view you can use
`Html.GetTimeZoneOffset`.
