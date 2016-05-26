using System.Collections.Generic;

namespace srvmon.Text
{
    public class DefaultFont14 : Font
    {
        private static Dictionary<char, Letter> _Letters;

        static DefaultFont14()
        {
            _Letters = new Dictionary<char, Letter>(64);
            // nwlosp[0-9a-f] for NetworkInterfaceScreen
            // runig for SystemdStatusScreen
            _Letters.Add(' ', new Letter(3, 1, new bool[] { false, false, false }));
        }

        private static readonly DefaultFont14 _Instance = new DefaultFont14();
        public static DefaultFont14 Instance { get { return _Instance; } }

        private DefaultFont14() { }

        public override byte MaxHeight { get { return 7; } }

        protected override Dictionary<char, Letter> Letters { get { return _Letters; } }
    }
}
