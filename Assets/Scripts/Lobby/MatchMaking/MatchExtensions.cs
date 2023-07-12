using System;
using System.Security.Cryptography;
using System.Text;

namespace Lobby.MatchMaking
{
    public static class MatchExtensions
    {
        public static Guid toGuid(this string id)
        {
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();

            byte[] inputBytes = Encoding.Default.GetBytes(id);
            byte[] hashBytes = provider.ComputeHash(inputBytes);

            return new Guid(hashBytes);
        }
    }
}