using Q2.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Q2.Hub
{
    public class MovieHub : Microsoft.AspNetCore.SignalR.Hub
    {
        private readonly ApplicationDbContext _context;

        public MovieHub(ApplicationDbContext appplicationDbContext)
        {
            _context = appplicationDbContext;
        }

        public async Task DeleteMovieAsync(int movieId)
        {
            var movie = await _context.Movies
                .Where(m => m.Id == movieId)
                .Include(m => m.Stars)
                .Include(m => m.Genres)
                .FirstOrDefaultAsync();

            if (movie != null)
            {
                foreach (var star in movie.Stars)
                {
                    movie.Stars.Remove(star);
                }

                foreach (var genre in movie.Genres)
                {
                    movie.Genres.Remove(genre);
                }

                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
            }

            await Clients.All.SendAsync("DeletedMovieId", movieId);
        }
    }
}
