using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Contracts;

namespace MessageServer
{
    public static class UserRepository
    {
        private static readonly ConcurrentDictionary<int,User> _repo = new ConcurrentDictionary<int, User>();

        public static int Insert(User u)
        {
            int hash = u.Callback.GetHashCode();
            _repo.TryAdd(hash, u);
            u.Id = hash;
            return hash;
        }

        public static void Remove(int id)
        {
            User u;
            _repo.TryRemove(id, out u);
        }

        public static IEnumerable<User> GetAllMatchingThemeUsers(User user)
        {
            return _repo.Values.Where(u => !u.Id.Equals(user.Id) && u.Theme.Equals(user.Theme));
        }

        public static User GetByService(ICService service)
        {
            User u;
            _repo.TryGetValue(service.GetHashCode(),out u);
            return u;
        }
    }
}
