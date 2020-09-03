using System;
using System.Collections.Generic;

namespace GridManagement.domain.Models
{
    public partial class Clients
    {
        public Clients()
        {
            ClientBilling = new HashSet<ClientBilling>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }

        public virtual ICollection<ClientBilling> ClientBilling { get; set; }
    }
}
