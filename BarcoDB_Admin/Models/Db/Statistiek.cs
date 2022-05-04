using System;
using System.Collections.Generic;

namespace BarcoDB_Admin.Models.Db
{
    public partial class Statistiek
    {
        public int Id { get; set; }
        public string? JrNr { get; set; }
        public short? Jaar { get; set; }
        public byte? Maand { get; set; }
        public byte? LessEqual5 { get; set; }
        public byte? More5 { get; set; }
        public byte? Som { get; set; }
        public byte? Aantal { get; set; }
    }
}
