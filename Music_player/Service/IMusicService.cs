using Music_player.Models;
using System.Collections.Generic;

namespace Music_player.Service
{
    public interface IMusicService
    {
        public List<FileDetails> GetListMusic();
    }
}