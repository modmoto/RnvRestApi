﻿using System.Collections.Generic;
using RnvRestApi.Domain.ValueTypes.Ids;

namespace RnvRestApi.Domain
{
    public class MrX : Player
    {
        public MrXId MrXId { get; }
        public IEnumerable<Station> showedLocations { get; }
        public IEnumerable<VehicelType> usedVehicles { get; }

        public MrX(Tickets tickets) : base(tickets)
        {
        }
    }
}