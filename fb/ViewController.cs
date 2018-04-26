using System;
using Xamarin.Auth;
using UIKit;
using System.Json;
using Foundation;


namespace fb
{
    public partial class ViewController : UIViewController
    {
        


        partial void UIButton391_TouchUpInside(UIButton sender)
        {
            var auth = new OAuth2Authenticator(
                clientId: "211037466157644",
                scope: "",
                authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth"),
                redirectUrl: new Uri("http://www.facebook.com/connect/login_success.html")

            
            );

            auth.Completed += Auth_Completed;
            var ui = auth.GetUI();
            PresentViewController(ui, true, null);
        }


        private async void Auth_Completed(object sender, AuthenticatorCompletedEventArgs e){

            if (e.IsAuthenticated)
            {

                var request = new OAuth2Request(
                    "GET",
                    new Uri("https://graph.facebook.com/me?fields=name.picture,cover,birthday"),
                    null,
                    e.Account
                );

                var fbResponse = await request.GetResponseAsync();

                var fbUser = JsonValue.Parse(fbResponse.GetResponseText());

                name.Text = fbUser["name"];
                id.Text = fbUser["id"];

            }

            DismissViewController(true, null);
        }


        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}
