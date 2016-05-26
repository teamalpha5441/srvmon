namespace srvmon.Text
{
    public sealed class Letter
    {
        public readonly byte Width;
        public readonly byte Height;
        public readonly bool[] Data;

        public Letter(byte Width, byte Height, bool[] Data)
        {
            this.Width = Width;
            this.Height = Height;
            this.Data = Data;
        }

        public static Letter GenerateDefaultLetter(byte Width, byte Height)
        {
            bool[] data = new bool[Width * Height];
            for (ushort i = 0; i < data.Length; i++)
                data[i] = true;
            return new Letter(Width, Height, data);
        }
    }
}
