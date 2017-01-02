namespace Framework.DBC
{
    public class DbHeader
    {
        public string Signature { get; set; }
        public uint RecordCount { get; set; }
        public uint FieldCount { get; set; }
        public uint RecordSize { get; set; }
        public uint StringBlockSize { get; set; }

        public bool IsValidDbcFile => Signature == "WDBC";
        public bool IsValidDb2File => Signature == "WDB2";
    }
}
