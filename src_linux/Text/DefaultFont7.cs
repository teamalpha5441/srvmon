using System.Collections.Generic;

namespace srvmon.Text
{
    public class DefaultFont7 : Font
    {
        private static Dictionary<char, Letter> _Letters;

        static DefaultFont7()
        {
            _Letters = new Dictionary<char, Letter>(64);
            _Letters.Add('0', new Letter(4, 7, new bool[]
            {
                false, true, true, false,
                true, false, false, true,
                true, false, true, true,
                true, true, false, true,
                true, false, false, true,
                true, false, false, true,
                false, true, true, false
            }));
            _Letters.Add('1', new Letter(3, 7, new bool[]
            {
                false, true, false,
                true, true, false,
                false, true, false,
                false, true, false,
                false, true, false,
                false, true, false,
                true, true, true
            }));
            _Letters.Add('2', new Letter(4, 7, new bool[]
            {
                false, true, true, false,
                true, false, false, true,
                false, false, false, true,
                false, true, true, false,
                true, false, false, false,
                true, false, false, false,
                true, true, true, true
            }));
            _Letters.Add('3', new Letter(4, 7, new bool[]
            {
                true, true, true, false,
                false, false, false, true,
                false, false, false, true,
                false, true, true, false,
                false, false, false, true,
                false, false, false, true,
                true, true, true, false,
            }));
            _Letters.Add('4', new Letter(4, 7, new bool[]
            {
                false, false, true, false,
                false, true, true, false,
                true, false, true, false,
                true, false, true, false,
                true, true, true, true,
                false, false, true, false,
                false, false, true, false
            }));
            _Letters.Add('5', new Letter(4, 7, new bool[]
            {
                true, true, true, true,
                true, false, false, false,
                true, false, false, false,
                true, true, true, false,
                false, false, false, true,
                false, false, false, true,
                true, true, true, false
            }));
            _Letters.Add('6', new Letter(4, 7, new bool[]
            {
                false, true, true, true,
                true, false, false, false,
                true, false, false, false,
                true, true, true, false,
                true, false, false, true,
                true, false, false, true,
                false, true, true, false
            }));
            _Letters.Add('7', new Letter(4, 7, new bool[]
            {
                true, true, true, true,
                false, false, false, true,
                false, false, true, false,
                false, false, true, false,
                false, true, false, false,
                false, true, false, false,
                false, true, false, false
            }));
            _Letters.Add('8', new Letter(4, 7, new bool[]
            {
                false, true, true, false,
                true, false, false, true,
                true, false, false, true,
                false, true, true, false,
                true, false, false, true,
                true, false, false, true,
                false, true, true, false
            }));
            _Letters.Add('9', new Letter(4, 7, new bool[]
            {
                false, true, true, false,
                true, false, false, true,
                true, false, false, true,
                false, true, true, true,
                false, false, false, true,
                false, false, false, true,
                true, true, true, false
            }));
            _Letters.Add('A', new Letter(4, 7, new bool[]
            {
                false, true, true, false,
                true, false, false, true,
                true, false, false, true,
                true, false, false, true,
                true, true, true, true,
                true, false, false, true,
                true, false, false, true
            }));
            _Letters.Add('B', new Letter(4, 7, new bool[]
            {
                true, true, true, false,
                true, false, false, true,
                true, false, false, true,
                true, true, true, false,
                true, false, false, true,
                true, false, false, true,
                true, true, true, false
            }));
            _Letters.Add('C', new Letter(4, 7, new bool[]
            {
                false, true, true, true,
                true, false, false, false,
                true, false, false, false,
                true, false, false, false,
                true, false, false, false,
                true, false, false, false,
                false, true, true, true
            }));
            _Letters.Add('D', new Letter(5, 7, new bool[]
            {
                true, true, true, true, false,
                false, true, false, false, true,
                false, true, false, false, true,
                false, true, false, false, true,
                false, true, false, false, true,
                false, true, false, false, true,
                true, true, true, true, false
            }));
            _Letters.Add('E', new Letter(4, 7, new bool[]
            {
                true, true, true, true,
                true, false, false, false,
                true, false, false, false,
                true, true, true, false,
                true, false, false, false,
                true, false, false, false,
                true, true, true, true
            }));
            _Letters.Add('F', new Letter(4, 7, new bool[]
            {
                true, true, true, true,
                true, false, false, false,
                true, false, false, false,
                true, true, true, false,
                true, false, false, false,
                true, false, false, false,
                true, false, false, false
            }));
            _Letters.Add('G', new Letter(4, 7, new bool[]
            {
                false, true, true, true,
                true, false, false, false,
                true, false, false, false,
                true, false, true, true,
                true, false, false, true,
                true, false, false, true,
                false, true, true, true
            }));
            _Letters.Add('H', new Letter(4, 7, new bool[]
            {
                true, false, false, true,
                true, false, false, true,
                true, false, false, true,
                true, true, true, true,
                true, false, false, true,
                true, false, false, true,
                true, false, false, true
            }));
            _Letters.Add('I', new Letter(3, 7, new bool[]
            {
                true, true, true,
                false, true, false,
                false, true, false,
                false, true, false,
                false, true, false,
                false, true, false,
                true, true, true,
            }));
            _Letters.Add('J', new Letter(4, 7, new bool[]
            {
                true, true, true, true,
                false, false, false, true,
                false, false, false, true,
                false, false, false, true,
                false, false, false, true,
                true, false, false, true,
                false, true, true, false
            }));
            _Letters.Add('K', new Letter(4, 7, new bool[]
            {
                true, false, false, true,
                true, false, false, true,
                true, false, true, false,
                true, true, false, false,
                true, false, true, false,
                true, false, false, true,
                true, false, false, true
            }));
            _Letters.Add('L', new Letter(3, 7, new bool[]
            {
                true, false, false,
                true, false, false,
                true, false, false,
                true, false, false,
                true, false, false,
                true, false, false,
                true, true, true
            }));
            _Letters.Add('M', new Letter(5, 7, new bool[]
            {
                true, false, false, false, true,
                true, true, false, true, true,
                true, true, false, true, true,
                true, false, true, false, true,
                true, false, true, false, true,
                true, false, false, false, true,
                true, false, false, false, true
            }));
            _Letters.Add('N', new Letter(4, 7, new bool[]
            {
                true, false, false, true,
                true, true, false, true,
                true, true, false, true,
                true, false, true, true,
                true, false, true, true,
                true, false, false, true,
                true, false, false, true
            }));
            _Letters.Add('O', new Letter(4, 7, new bool[]
            {
                false, true, true, false,
                true, false, false, true,
                true, false, false, true,
                true, false, false, true,
                true, false, false, true,
                true, false, false, true,
                false, true, true, false
            }));
            _Letters.Add('P', new Letter(4, 7, new bool[]
            {
                true, true, true, false,
                true, false, false, true,
                true, false, false, true,
                true, true, true, false,
                true, false, false, false,
                true, false, false, false,
                true, false, false, false
            }));
            _Letters.Add('Q', new Letter(5, 7, new bool[]
            {
                false, true, true, true, false,
                true, false, false, false, true,
                true, false, false, false, true,
                true, false, false, false, true,
                true, false, true, false, true,
                true, false, false, true, false,
                false, true, true, false, true
            }));
            _Letters.Add('R', new Letter(4, 7, new bool[]
            {
                true, true, true, false,
                true, false, false, true,
                true, false, false, true,
                true, true, true, false,
                true, false, false, true,
                true, false, false, true,
                true, false, false, true
            }));
            _Letters.Add('S', new Letter(4, 7, new bool[]
            {
                false, true, true, false,
                true, false, false, true,
                true, false, false, false,
                false, true, true, false,
                false, false, false, true,
                true, false, false, true,
                false, true, true, false
            }));
            _Letters.Add('T', new Letter(5, 7, new bool[]
            {
                true, true, true, true, true,
                false, false, true, false, false,
                false, false, true, false, false,
                false, false, true, false, false,
                false, false, true, false, false,
                false, false, true, false, false,
                false, false, true, false, false
            }));
            _Letters.Add('U', new Letter(4, 7, new bool[]
            {
                true, false, false, true,
                true, false, false, true,
                true, false, false, true,
                true, false, false, true,
                true, false, false, true,
                true, false, false, true,
                false, true, true, false
            }));
            _Letters.Add('V', new Letter(3, 7, new bool[]
            {
                true, false, true,
                true, false, true,
                true, false, true,
                true, false, true,
                true, false, true,
                false, true, false,
                false, true, false
            }));
            _Letters.Add('W', new Letter(5, 7, new bool[]
            {
                true, false, false, false, true,
                true, false, false, false, true,
                true, false, true, false, true,
                true, false, true, false, true,
                true, true, false, true, true,
                true, true, false, true, true,
                true, false, false, false, true
            }));
            _Letters.Add('X', new Letter(5, 7, new bool[]
            {
                true, false, false, false, true,
                true, false, false, false, true,
                false, true, false, true, false,
                false, false, true, false, false,
                false, true, false, true, false,
                true, false, false, false, true,
                true, false, false, false, true
            }));
            _Letters.Add('Y', new Letter(5, 7, new bool[]
            {
                true, false, false, false, true,
                true, false, false, false, true,
                false, true, false, true, false,
                false, false, true, false, false,
                false, false, true, false, false,
                false, false, true, false, false,
                false, false, true, false, false
            }));
            _Letters.Add('Z', new Letter(4, 7, new bool[]
            {
                true, true, true, true,
                false, false, false, true,
                false, false, true, false,
                false, true, false, false,
                true, false, false, false,
                true, false, false, false,
                true, true, true, true
            }));
            _Letters.Add('a', new Letter(4, 5, new bool[]
            {
                true, true, true, false,
                false, false, false, true,
                false, true, true, true,
                true, false, false, true,
                false, true, true, true
            }));
            _Letters.Add('b', new Letter(4, 7, new bool[]
            {
                true, false, false, false,
                true, false, false, false,
                true, true, true, false,
                true, false, false, true,
                true, false, false, true,
                true, false, false, true,
                true, true, true, false
            }));
            _Letters.Add('c', new Letter(4, 5, new bool[]
            {
                false, true, true, true,
                true, false, false, false,
                true, false, false, false,
                true, false, false, false,
                false, true, true, true
            }));
            _Letters.Add('d', new Letter(4, 7, new bool[]
            {
                false, false, false, true,
                false, false, false, true,
                false, true, true, true,
                true, false, false, true,
                true, false, false, true,
                true, false, false, true,
                false, true, true, true
            }));
            _Letters.Add('e', new Letter(4, 5, new bool[]
            {
                false, true, true, false,
                true, false, false, true,
                true, true, true, true,
                true, false, false, false,
                false, true, true, true
            }));
            _Letters.Add('f', new Letter(3, 7, new bool[]
            {
                false, true, true,
                true, false, false,
                true, false, false,
                true, true, false,
                true, false, false,
                true, false, false,
                true, false, false
            }));
            // g
            _Letters.Add('h', new Letter(3, 7, new bool[]
            {
                true, false, false,
                true, false, false,
                true, true, false,
                true, false, true,
                true, false, true,
                true, false, true,
                true, false, true
            }));
            _Letters.Add('i', new Letter(1, 6, new bool[] { true, false, true, true, true, true }));
            // j
            // k
            _Letters.Add('l', new Letter(2, 7, new bool[]
            {
                true, false,
                true, false,
                true, false,
                true, false,
                true, false,
                true, false,
                false, true
            }));
            _Letters.Add('m', new Letter(5, 5, new bool[]
            {
                true, true, true, true, false,
                true, false, true, false, true,
                true, false, true, false, true,
                true, false, true, false, true,
                true, false, true, false, true
            }));
            _Letters.Add('n', new Letter(4, 5, new bool[]
            {
                true, true, true, false,
                true, false, false, true,
                true, false, false, true,
                true, false, false, true,
                true, false, false, true
            }));
            _Letters.Add('o', new Letter(4, 5, new bool[]
            {
                false, true, true, false,
                true, false, false, true,
                true, false, false, true,
                true, false, false, true,
                false, true, true, false
            }));
            // p
            // q
            // r
            _Letters.Add('s', new Letter(3, 5, new bool[]
            {
                false, true, true,
                true, false, false,
                false, true, false,
                false, false, true,
                true, true, false
            }));
            _Letters.Add('t', new Letter(3, 7, new bool[]
            {
                false, true, false,
                false, true, false,
                true, true, true,
                false, true, false,
                false, true, false,
                false, true, false,
                false, false, true
            }));
            _Letters.Add('u', new Letter(3, 5, new bool[]
            {
                true, false, true,
                true, false, true,
                true, false, true,
                true, false, true,
                false, true, true
            }));
            // v
            // w
            // x
            // y
            // z
            _Letters.Add('/', new Letter(3, 6, new bool[]
            {
                false, false, true,
                false, false, true,
                false, true, false,
                false, true, false,
                true, false, false,
                true, false, false
            }));
            _Letters.Add('%', new Letter(3, 6, new bool[]
            {
                true, false, true,
                false, false, true,
                false, true, false,
                false, true, false,
                true, false, false,
                true, false, true
            }));
            _Letters.Add('.', new Letter(1, 1, new bool[] { true }));
            _Letters.Add(':', new Letter(1, 6, new bool[] { true, false, false, false, true, false }));
            _Letters.Add('-', new Letter(4, 4, new bool[]
            {
                true, true, true, true,
                false, false, false, false,
                false, false, false, false,
                false, false, false, false
            }));
            _Letters.Add(' ', new Letter(2, 1, new bool[] { false, false }));
            _Letters.Add('\u25CC', new Letter(5, 6, new bool[]
            {
                false, true, false, true, false,
                false, false, false, false, false,
                true, false, false, false, true,
                false, false, false, false, false,
                false, true, false, true, false,
                false, false, false, false, false
            }));
            _Letters.Add('\u25CB', new Letter(5, 6, new bool[]
            {
                false, true, true, true, false,
                true, false, false, false, true,
                true, false, false, false, true,
                true, false, false, false, true,
                false, true, true, true, false,
                false, false, false, false, false
            }));
            _Letters.Add('\u25CF', new Letter(5, 6, new bool[]
            {
                false, true, true, true, false,
                true, true, true, true, true,
                true, true, true, true, true,
                true, true, true, true, true,
                false, true, true, true, false,
                false, false, false, false, false
            }));
        }

        private static readonly DefaultFont7 _Instance = new DefaultFont7();
        public static DefaultFont7 Instance { get { return _Instance; } }

        private DefaultFont7() { }

        public override byte MaxHeight { get { return 7; } }

        protected override Dictionary<char, Letter> Letters { get { return _Letters; } }
    }
}
