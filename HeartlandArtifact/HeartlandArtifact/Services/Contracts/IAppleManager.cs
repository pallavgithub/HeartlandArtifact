using System;
using System.Threading.Tasks;
using HeartlandArtifact.Helpers;
using HeartlandArtifact.Models;
namespace HeartlandArtifact.Services.Contracts
{
    public interface IAppleManager
    {
        bool IsAvailable { get; }
        Task<AppleSignInCredentialState> GetCredentialStateAsync(string userId);
        Task<AppleAccount> SignInAsync();
    }
}
