using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dentist2.Models
{
    public class ViewHome
    {
        public About About { get; set; }
        public List<slide> Slide { get; set; }
        public List<icon> icon { get; set; } 
        public List<Service> service { get; set; }
        public List<range> range { get; set; }
        public List<news> news { get; set; }
        public List<team> team { get; set; }
        public List <gallery> gallery { get; set; }
        public helpdesk helpdesk { get; set; }
        public List <subscribe> subscribe { get; set;}
    }
}