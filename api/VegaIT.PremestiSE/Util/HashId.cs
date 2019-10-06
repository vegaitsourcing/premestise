using System;
using HashidsNet;
using Util.Exceptions;

namespace Util
{
    public class HashId
    {
        private static string Salt = "codeforacause3";
        private static int HashLength = 10;
        private static Hashids hashid = new Hashids(Salt, HashLength);


        public static string Encode(int id)
        {
            try
            {
                var newId = hashid.Encode(id);
                return newId;
            }
            catch
            {
                throw new HashIdException();
            }
        }

        public static int Decode(string id)
        {
            try
            {
                var newId = hashid.Decode(id);
                return newId[0];
            }
            catch
            {
                throw new HashIdException();
            }
        }
    }
}
