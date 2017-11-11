﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Domain.Validation;
using Domain.ValueTypes;
using Domain.ValueTypes.Ids;

namespace Domain
{
    public class MrX : Player
    {
        public static MrX NullValue { get; } = new MrX(new MrXId(new Guid().ToString()), "NaN", new Collection<Move>(), new Collection<Move>());

        public ICollection<Move> OpenMoves { get; }
        public IEnumerable<VehicelType> UsedVehicles => MoveHistory.Select(move => move.Type);

        public MrXId MrXId { get; }
        public static event Action MrxDeleted;
        public static event Action<Move, MrX> MrxMoved;

        public MrX(MrXId mrXId, string name, ICollection<Move> moveHistory, ICollection<Move> openMoves) : base(name)
        {
            MrXId = mrXId;
            OpenMoves = openMoves;
            MoveHistory = moveHistory;
        }

        public MrX(string name) : base(name)
        {
            MrXId = new MrXId(Guid.NewGuid().ToString());
            OpenMoves = new Collection<Move>();
            MoveHistory = new Collection<Move>();
        }

        public DomainValidationResult Delete()
        {
            MrxDeleted?.Invoke();
            return DomainValidationResult.OkResult();
        }

        public override DomainValidationResult Move(Station station, VehicelType vehicelType)
        {
            var move = new Move(station, vehicelType);
            MoveHistory.Add(move);
            CurrentStationHidden = station;
            if (MoveHistory.Count % 5 == 0)
            {
                OpenMoves.Add(move);
                LastKnownStation = station;
            }

            MrxMoved?.Invoke(move, this);
            return DomainValidationResult.OkResult();
        }

        public Station LastKnownStation { get; private set; } = Station.NullStation;

        public Station CurrentStationHidden { get; private set; } = Station.NullStation;
    }
}