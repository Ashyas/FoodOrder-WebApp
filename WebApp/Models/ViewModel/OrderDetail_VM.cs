using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
    public class OrderDetail_VM
    {
        public OrderHeader orderHeader { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
    }
}
