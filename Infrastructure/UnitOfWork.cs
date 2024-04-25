﻿using Core.Repositories;
using Core.UnitOfWork;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public  IPostRepository PostRepository { get; }

        public IMediaFileRepository MediaFileRepository { get; }

        public UnitOfWork(AppDbContext context,
            IPostRepository postRepository,
            IMediaFileRepository mediaFileRepository
            )
        {
            _context = context;
            PostRepository = postRepository;
            MediaFileRepository = mediaFileRepository;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void BeginTransaction()
        {
            _context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _context.Database.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            _context.Database.RollbackTransaction();
        }
    }
}
