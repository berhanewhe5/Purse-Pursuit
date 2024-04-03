// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("FA6wq1TNHDNCdNDS700ci9KH5YuSGXfwJaGYPJlTIuKjNQThi/DQ8NN3JzgR28SzobV2ctSA14aupYaDamzWnaNRquyJzlTzsqeWJRJocU63JRCWyimfKoFIOvSFEg8AJID+2jH0PcJS1RjDNoU4zkLqtYlyXwXWekMF4PRMlmKmA8cQ8L6A23KYiiUZ/TFUyx/78BTdITe41DbFG+DhEC7ScbpT6rio9/I3B5VpPDauY1wtWNWf0nFbK3HntI9XRRPCfCdnO+M+kZbw3qyNvtPfjXodvxAqRuROHWjr5eraaOvg6Gjr6+pDQT8DqdjxlvboOweB/LnWH8gPnfF2MXihFVXaaOvI2ufs48Bsomwd5+vr6+/q6W9O15HmyG8At+jp6+rr");
        private static int[] order = new int[] { 10,12,11,3,13,9,11,11,9,11,11,12,13,13,14 };
        private static int key = 234;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
