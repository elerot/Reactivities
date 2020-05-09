using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Applicaiton.Errors;
using MediatR;
using Persistence;

namespace Applicaiton.Activities
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                this._context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.FindAsync(request.Id);

                if (activity == null)
                    throw new RestException(HttpStatusCode.NotFound, new { activity = "Not Found" });

                _context.Remove(activity);

                var succes = await _context.SaveChangesAsync() > 0;
                if (succes) return Unit.Value;
                throw new Exception("Problem saving changes");
            }
        }
    }
}