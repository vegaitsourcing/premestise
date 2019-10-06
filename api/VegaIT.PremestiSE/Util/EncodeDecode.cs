using System;
using HashidsNet;

namespace Util
{
    public class EncodeDecode
    {
        private static string Salt = "codeforacause3";
        private static int HashLength = 10;
        private static Hashids hashid = new Hashids(Salt, HashLength);


        public static string Encode(int id)
        {

            var newId = hashid.Encode(id);
            return newId;
        }

        public static int Decode(string id)
        {
            Console.WriteLine("ID : " + id);
            var newId = hashid.Decode(id);
            Console.WriteLine("AFTER DECODE: " + newId);
            Console.WriteLine("newId[0]: " + newId[0]);
            return newId[0];
        }
    }
}
