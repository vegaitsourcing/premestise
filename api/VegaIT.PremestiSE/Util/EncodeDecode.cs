using HashidsNet;

namespace Util
{
    public class EncodeDecode
    {
        private static string Salt = "codeforacause3";
        private static int HashLength = 10;
        public static string Encode(int id)
        {
            var hashids = new Hashids(Salt, HashLength);
            var NewId = hashids.Encode(id);
            return NewId;
        }

        public static int Decode(string id)
        {
            var hashids = new Hashids(Salt, HashLength);
            var NewId = hashids.Decode(id);
            return NewId[0];
        }
    }
}
