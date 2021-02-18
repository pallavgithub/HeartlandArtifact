using HeartlandArtifact.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeartlandArtifact.Services.Contracts
{
	public interface IFacebookManager
	{
		void Login(Action<FacebookUser, string> onLoginComplete);
		void Logout();
	}
}
