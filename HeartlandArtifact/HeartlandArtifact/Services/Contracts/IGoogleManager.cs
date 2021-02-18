using HeartlandArtifact.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeartlandArtifact.Services.Contracts
{
	public interface IGoogleManager
	{
		void Login(Action<GoogleUser, string> OnLoginComplete);

		void Logout();
	}
}
