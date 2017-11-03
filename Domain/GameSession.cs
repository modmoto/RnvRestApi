﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Domain.Validation;
using Domain.ValueTypes.Ids;

namespace Domain
{
    public class GameSession
    {
        public static event Action<GameSession> GameSessionCreated;
        public static event Action<MrX, GameSession> MrxAdded;
        public static event Action<PoliceOfficer, GameSession> PoliceOfficerAdded;
        public static event Action<MrX> MrXDeleted;
        public static event Action<PoliceOfficer> PoliceOfficerDeleted;

        public static GameSession Create(string name, out DomainValidationResult result)
        {
            var session = new GameSession(name, new GameSessionId(Guid.NewGuid().ToString()));
            GameSessionCreated?.Invoke(session);
            result = DomainValidationResult.OkResult();
            return session;
        }

        private GameSession(string name, GameSessionId id)
        {
            Name = name;
            GameSessionId = id;
            PoliceOfficers = new Collection<PoliceOfficer>();
            StartTime = DateTimeOffset.Now;
            MrX = MrX.NullValue;
        }

        private void OnMrxDeleted()
        {
            var mrxTemp = MrX;
            MrX = MrX.NullValue;
            MrXDeleted?.Invoke(mrxTemp);
        }

        public GameSession(string name, GameSessionId id, DateTimeOffset startTime, MrX mrX, ICollection<PoliceOfficer> policeOfficers)
        {
            Name = name;
            GameSessionId = id;
            StartTime = startTime;
            MrX = mrX;
            PoliceOfficers = policeOfficers;

            MrX.MrxDeleted += OnMrxDeleted;
            PoliceOfficer.PoliceOfficerDeleted += PoliceOfficerOnPoliceOfficerDeleted;
        }

        private void PoliceOfficerOnPoliceOfficerDeleted(PoliceOfficer policeOfficer)
        {
            PoliceOfficers.Remove(policeOfficer);
            PoliceOfficerDeleted?.Invoke(policeOfficer);
        }

        public MrX AddNewMrX(string mrXName, out DomainValidationResult validationResult)
        {
            var mrX = new MrX(mrXName);
            if (MrX != MrX.NullValue)
            {
                validationResult =
                    new DomainValidationResult("Game Session can only have one MrX, delete the old one first");
                return MrX;
            }
            MrX = mrX;
            MrxAdded?.Invoke(mrX, this);
            validationResult = DomainValidationResult.OkResult();
            return mrX;
        }

        public PoliceOfficer AddNewOfficer(string officerName, out DomainValidationResult validationResult)
        {
            var officer = new PoliceOfficer(officerName);
            PoliceOfficers.Add(officer);
            PoliceOfficerAdded?.Invoke(officer, this);
            validationResult = DomainValidationResult.OkResult();
            return officer;
        }

        public GameSessionId GameSessionId { get; }
        public string Name { get;  }
        public DateTimeOffset StartTime { get; }
        public MrX MrX { get; private set; }
        public ICollection<PoliceOfficer> PoliceOfficers { get; }

        public PoliceOfficer GetPoliceOfficer(PoliceOfficerId policeOfficerId, out DomainValidationResult validationResult)
        {
            var policeOfficer = PoliceOfficers.SingleOrDefault(officer => officer.PoliceOfficerId == policeOfficerId);
            validationResult = policeOfficer == null ? new DomainValidationResult("Police Officer not found") : DomainValidationResult.OkResult();
            return policeOfficer;
        }
    }
}