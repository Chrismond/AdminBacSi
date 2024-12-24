using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageBloodTypes.Models
{
    public class NhomMauModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> CanGiveTo { get; set; }
        public List<int> CanReceiveFrom { get; set; }
    }
}