using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageBloodTypes.Models
{
    public class YeuCauModel
    {
        public int ID { get; set; }
        public string Ten { get; set; }
        public Nullable<int> SoLanHienMau { get; set; }
        public Nullable<int> SoLanNhanMau { get; set; }
        public Nullable<bool> Hide { get; set; }

    }
}