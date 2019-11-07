using System;
using HashidsNet;

namespace Util
{
    public class HashId
    {
        private static int _hashLength = 10;
        private static string _salt = "codeforacause3";
        private static Hashids hashid = new Hashids(_salt, _hashLength);

        public static string Encode(int id)
        {
            try
            {
                var newId = hashid.Encode(id);
                return newId;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static int Decode(string id)
        {
            try
            {
                var newId = hashid.Decode(id);
                return newId[0];
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
