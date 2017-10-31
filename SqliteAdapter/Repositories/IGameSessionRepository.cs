﻿using System.Collections.Immutable;
using System.Threading.Tasks;
using Domain;

namespace SqliteAdapter.Repositories
{
    public interface IGameSessionRepository
    {
        IImmutableList<GameSession> GetSessions();
        Task Add(GameSession gameSession);
        Task AddPoliceOfficer(PoliceOfficer policeOfficer, GameSession gameSession);
        Task AddMrX(MrX mrX, GameSession gameSession);
    }
}