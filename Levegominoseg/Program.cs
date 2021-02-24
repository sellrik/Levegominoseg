using System;

namespace Levegominoseg
{
    class Program
    {
        static void Main(string[] args)
        {
            var downloader = new Downloader();
            var data = downloader.Download();
        }
    }
}
