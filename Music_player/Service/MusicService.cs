using Music_player.Models;
using System.Collections.Generic;
using System.IO;

namespace Music_player.Service

{
    public class MusicService : IMusicService
    {
        public List<FileDetails> GetListMusic()
        {
            List<FileDetails> Audio = new List<FileDetails>();
            DirectoryInfo d = new DirectoryInfo(@"wwwroot/Upload/"); //Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.mp3");//Getting Text files
            int id = 0;
            foreach (var item in Files)
            {
                Audio.Add(
                    new FileDetails
                    {
                        Id = id++,
                        Name = item.Name,
                        Path = @"wwwroot/Upload/" + item.Name
                    }
                    );
            }

            return Audio;
        }
    }
}
