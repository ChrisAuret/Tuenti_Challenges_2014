using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge_02_F1TrackMaker
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //var trackData = @"------\-/-/-\-----#-------\--/----------------\--\----\---/---";
            //var trackData = @"#----\-----/-----\-----/";
            const string trackData = @"#------/------\--\/-------\/--/------\----/-------/-\--------/---------------------------------------------\-------\--\/-------\/--/------\-----/\\------/-\-----\-/-----/-\------//\--/-------/----\/--/-\--\/--/-\--\/--/\----/-------/--\\\\\\\\\\\\/-----/-\------/----\\\\\\/---\----";

            var trackMaker = new TrackMaker();
            var track = trackMaker.MakeTrack(trackData);

            Console.WriteLine(track);
            Console.Read();
        }
    }
}
