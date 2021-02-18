﻿using System;

using Foundation;
using Google.SignIn;
using HeartlandArtifact.Services.Contracts;
using UIKit;

namespace HeartlandArtifact.iOS
{
	public class GoogleManager : NSObject, IGoogleManager, ISignInDelegate
	{
		private Action<Models.GoogleUser, string> _onLoginComplete;
		private UIViewController _viewController { get; set; }

		public GoogleManager()
		{
			//SignIn.SharedInstance.UIDelegate = this;
			//SignIn.SharedInstance.Delegate = this;
		}

		public void Login(Action<Models.GoogleUser, string> OnLoginComplete)
		{
			 //moved this here from Configure
			
			_onLoginComplete = OnLoginComplete;

			var window = UIApplication.SharedApplication.KeyWindow;
			var vc = window.RootViewController;
			while (vc.PresentedViewController != null)
			{
				vc = vc.PresentedViewController;
			}

			_viewController = vc;
			SignIn.SharedInstance.PresentingViewController = _viewController;
			SignIn.SharedInstance.SignedIn += (sender, e) =>
			{
				DidSignIn(null, e.User, e.Error);
			};

			SignIn.SharedInstance.SignInUser();
		}

		public void Logout()
		{
			SignIn.SharedInstance.SignOutUser();
		}

		public void DidSignIn(SignIn signIn, Google.SignIn.GoogleUser user, NSError error)
		{

			if (user != null && error == null)
				_onLoginComplete?.Invoke(new Models.GoogleUser()
				{
					Name = user.Profile.Name,
					Email = user.Profile.Email,
					Picture = user.Profile.HasImage ? new Uri(user.Profile.GetImageUrl(500).ToString()) : new Uri(string.Empty)
				}, string.Empty);
			else
				_onLoginComplete?.Invoke(null, error.LocalizedDescription);
		}

		[Export("signIn:didDisconnectWithUser:withError:")]
		public void DidDisconnect(SignIn signIn, GoogleUser user, NSError error)
		{
			// Perform any operations when the user disconnects from app here.
		}

		[Export("signInWillDispatch:error:")]
		public void WillDispatch(SignIn signIn, NSError error)
		{
			//myActivityIndicator.StopAnimating();
		}

		[Export("signIn:presentViewController:")]
		public void PresentViewController(SignIn signIn, UIViewController viewController)
		{
			_viewController?.PresentViewController(viewController, true, null);
		}

		[Export("signIn:dismissViewController:")]
		public void DismissViewController(SignIn signIn, UIViewController viewController)
		{
			_viewController?.DismissViewController(true, null);
		}
	}
}