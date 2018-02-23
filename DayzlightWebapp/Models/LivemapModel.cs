using DayzlightCommon.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DayzlightWebapp.Models
{
    public class LivemapModel
    {
        // Response from server fields
        public ServerRestartEntity ServerLastRestartInfo { set; get; }
        public ServerRestartEntity ServerCurRestartInfo { set; get; }
        public ServerRestartEntity ServerNextRestartInfo { set; get; }
        public TimepointEntity[] Timepoints { set; get; }

        // Request to server fields
        public String PostAction { set; get; }
        public String PostData { set; get; }
    }
}