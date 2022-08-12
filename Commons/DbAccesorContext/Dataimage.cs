using System;
using System.Collections.Generic;

#nullable disable

namespace Commons.DbAccesorContext
{
    public partial class Dataimage
    {
        public long? Iid { get; set; }
        public byte[] Image { get; set; }
        public string Name { get; set; }
        public string Filetype { get; set; }
        public string Filecontenttype { get; set; }
    }
}
